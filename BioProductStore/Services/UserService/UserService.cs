using AutoMapper;
using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Repositories.UserRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Threading.Tasks;
using BioProductStore.Utilities.JWTUtils;
using BioProductStore.Utilities;
using Microsoft.Extensions.Options;

namespace BioProductStore.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public BioProductStoreContext _context;
        private IJWTUtils _ijwtUtils;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, BioProductStoreContext context, IJWTUtils ijwtUtils, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userRepository = userRepository;
            _context = context;
            _ijwtUtils = ijwtUtils;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public UserResponseDTO GetUserByUserId(Guid Id)
        {
            User user = _userRepository.FindById(Id);

            if (user == null)
                throw new Exception("User not found!");

            UserResponseDTO userResponseDto = _mapper.Map<UserResponseDTO>(user);

            return userResponseDto;
        }

        public void CreateUser(RegisterUserDTO user)
        {
            //check if username and email are unique
            if (_userRepository.GetByEmail(user.Email) != null
                || _userRepository.GetByUsername(user.Username) != null)
                throw new Exception("Email or username already exists");

            var userToCreate = _mapper.Map<User>(user);
            userToCreate.Role = Role.User;
            userToCreate.PasswordHash = BCryptNet.HashPassword(user.PasswordHash);
            userToCreate.DateCreated =  DateTime.Now;
            userToCreate.DateModified = DateTime.Now;

            _userRepository.Create(userToCreate);
            _userRepository.Save(); //without this line it does't save
        }

        public void CreateAdmin(RegisterUserDTO user)
        {

            // verific ca username ul si emailul sa fie unice(sa nu se regaseasca in baza de date)
            if (_userRepository.GetByEmail(user.Email) != null || _userRepository.GetByUsername(user.Username) != null)
                throw new Exception("Email or username already exists");

            var userToCreate = _mapper.Map<User>(user);

            userToCreate.Role = Role.Admin;
            userToCreate.PasswordHash = BCryptNet.HashPassword(user.PasswordHash);
            userToCreate.DateCreated = DateTime.Now;
            userToCreate.DateModified = DateTime.Now;

            _userRepository.Create(userToCreate);
            _userRepository.Save();
        }

        public List<UserResponseDTO> GetAllUsers()
        {
            List<User> usersList = _userRepository.GetAllUsers();

            if (usersList.Count == 0)
                throw new Exception("There are no users");

            List<UserResponseDTO> userResponseDto = _mapper.Map<List<UserResponseDTO>>(usersList);
            return userResponseDto;
        }

        public List<UserResponseDTO> GetAllUsersByName(string name)
        {
            List<User> usersList = _userRepository.GetAllUsersByName(name);
            if (usersList.Count == 0)
                throw new Exception("There are no users with this name");
            List<UserResponseDTO> userResponseDto = _mapper.Map<List<UserResponseDTO>>(usersList);
            return userResponseDto;
        }


        public void DeleteUserById(Guid id)
        {
            User user = _userRepository.FindById(id);

            if (user == null)
                throw new Exception("User not found");

            _userRepository.Delete(user);
            _userRepository.Save();
        }

        public void UpdateUser(RegisterUserDTO newUser, Guid id)
        {
            User userToUpdate = _userRepository.FindById(id);

            if (userToUpdate == null)
                throw new Exception("User not found");

            userToUpdate = _mapper.Map<RegisterUserDTO, User>(newUser, userToUpdate);

            userToUpdate.DateModified = DateTime.Now;
            userToUpdate.PasswordHash = BCryptNet.HashPassword(newUser.PasswordHash);

            try
            {
                _userRepository.Update(userToUpdate);
                _userRepository.Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }

            public UserResponseTokenDTO Authentificate(LoginUserDTO model) //asta e o metoda care verifica parolele (hash-ul cu parola noastra)
            {

                var user = _context.Users.FirstOrDefault(x => x.Username.Equals(model.Username));

                if (user == null || !BCryptNet.Verify(model.PasswordHash, user.PasswordHash))
                {
                    return null;
                }

                //generam jwt token
                var jwtToken = _ijwtUtils.GenerateJWTToken(user);
                return new UserResponseTokenDTO(user, jwtToken);
            }
        }
    
}
