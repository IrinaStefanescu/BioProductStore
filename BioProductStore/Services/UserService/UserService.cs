using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public BioProductStoreContext _context;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public RegisterUserDTO GetUserByUserId(Guid Id)
        {
            User user = _userRepository.FindById(Id);
            RegisterUserDTO userDto = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                //Username = user.Username,
            };

            return userDto;
        }

        public void CreateUser(RegisterUserDTO user)
        {
            var userToCreate = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            _userRepository.Create(userToCreate);
            _userRepository.Save(); //without this line it does't save
        }

        public IQueryable<RespondUserDTO> GetAllUsers()
        {
            IQueryable<RespondUserDTO> usersList = _userRepository.GetAllUsers();
            return usersList;
        }

        public IQueryable<RespondUserDTO> GetAllUsersByName(string name)
        {
            IQueryable<RespondUserDTO> usersList = _userRepository.GetAllUsersByName(name);
            return usersList;
        }

        public IQueryable<RespondUserDTO> GetAllUsersByEmail(string email)
        {
            IQueryable<RespondUserDTO> usersList = _userRepository.GetAllUsersByEmail(email);
            return usersList;
        }

        public void DeleteUserById(Guid id)
        {
            User user = _userRepository.FindById(id);
            _userRepository.Delete(user);
            _userRepository.Save();
        }

        public void UpdateUser(RegisterUserDTO newUser, Guid id)
        {
            User user = _userRepository.FindById(id);

            if (newUser.FirstName != null)
                user.FirstName = newUser.FirstName;

            if (newUser.LastName != null)
                user.LastName = newUser.LastName;

            if (newUser.Email != null)
                user.Email = newUser.Email;

            if (newUser.Username != null)
                user.Username = newUser.Username;

            if (newUser.Password != null)
                user.Password = newUser.Password;

            user.DateModified = DateTime.Now;

            _userRepository.Save();
        }
    }
}
