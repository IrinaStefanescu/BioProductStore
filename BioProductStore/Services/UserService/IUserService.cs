using BioProductStore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services
{
    public interface IUserService
    {
        UserResponseDTO GetUserByUserId(Guid Id);
        public List<UserResponseDTO> GetAllUsers();
        public List<UserResponseDTO> GetAllUsersByName(string name);

  

        void CreateUser(RegisterUserDTO entity);
        void CreateAdmin(RegisterUserDTO entity);

        void DeleteUserById(Guid id);
        void UpdateUser(RegisterUserDTO user, Guid id);

    }
}
