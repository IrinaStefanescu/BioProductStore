using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BioProductStore.Data;
using BioProductStore.DataAccess;
using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Utilities;
using BioProductStore.Utilities.JWTUtils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BioProductStore.Services
{
    public class UserService : IUserService
    {
        public UnitOfWork _uow;
        private IJWTUtils _ijwtUtils;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserService(UnitOfWork uow, BioProductStoreContext context, IJWTUtils ijwtUtils, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _uow = uow;
            _ijwtUtils = ijwtUtils;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public UserResponseDTO GetUserByUserId(Guid Id)
        {
            User user = _uow.User.FindById(Id);

            if (user == null)
                throw new Exception("User not found!");

            UserResponseDTO userResponseDto = _mapper.Map<UserResponseDTO>(user);

            return userResponseDto;
        }

        public void CreateUser(RegisterUserDTO user)
        {
            //check if username and email are unique
            if (_uow.User.FindBy(e => e.Email == user.Email) != null
                || _uow.User.FindBy(e => e.Username == user.Username) != null)
                throw new Exception("Email or username already exists");

            var userToCreate = _mapper.Map<User>(user);
            userToCreate.Role = Role.User;
            userToCreate.PasswordHash = BCryptNet.HashPassword(user.Password);
            userToCreate.DateCreated =  DateTime.Now;
            userToCreate.DateModified = DateTime.Now;

            _uow.User.Create(userToCreate);
            _uow.SaveChanges(); //without this line it does't save
        }

        public void CreateAdmin(RegisterUserDTO user)
        {

            // verific ca username ul si emailul sa fie unice(sa nu se regaseasca in baza de date)
            if (_uow.User.FindBy(e => e.Email == user.Email) != null
                || _uow.User.FindBy(e => e.Username == user.Username) != null)
                throw new Exception("Email or username already exists");

            var userToCreate = _mapper.Map<User>(user);

            userToCreate.Role = Role.Admin;
            userToCreate.PasswordHash = BCryptNet.HashPassword(user.Password);
            userToCreate.DateCreated = DateTime.Now;
            userToCreate.DateModified = DateTime.Now;

            _uow.User.Create(userToCreate);
            _uow.SaveChanges();
        }

        public List<UserResponseDTO> GetAllUsers()
        {
            var usersList =  _uow.User.GetAllAsQueryable();

            if (usersList.Count() == 0)
                throw new Exception("There are no users");

            List<UserResponseDTO> userResponseDto = _mapper.Map<List<UserResponseDTO>>(usersList);
            return userResponseDto;
        }

        public List<UserResponseDTO> GetAllUsersByName(string name)
        {
           var usersList =  _uow.User.GetAll(e => e.Username == name);
            if (usersList.Count() == 0)
                throw new Exception("There are no users with this name");
            List<UserResponseDTO> userResponseDto = _mapper.Map<List<UserResponseDTO>>(usersList);
            return userResponseDto;
        }
        
        public void DeleteUserById(Guid id)
        {
            User user = _uow.User.FindById(id);

            if (user == null)
                throw new Exception("User not found");

            _uow.User.Delete(user);
            _uow.SaveChanges();
        }

        public void UpdateUser(RegisterUserDTO newUser, Guid id)
        {
            User userToUpdate = _uow.User.FindById(id);

            if (userToUpdate == null)
                throw new Exception("User not found");

            userToUpdate = _mapper.Map<RegisterUserDTO, User>(newUser, userToUpdate);

            userToUpdate.DateModified = DateTime.Now;
            userToUpdate.PasswordHash = BCryptNet.HashPassword(newUser.Password);

            try
            {
                _uow.User.Update(userToUpdate);
                _uow.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
    
}
