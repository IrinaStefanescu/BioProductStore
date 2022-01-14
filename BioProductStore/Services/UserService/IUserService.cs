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
        public IQueryable<UserResponseDTO> GetAllUsers();
        public IQueryable<UserResponseDTO> GetAllUsersByName(string name);

        public IQueryable<UserResponseDTO> GetAllUsersByEmail(string email);

        void CreateUser(RegisterUserDTO entity);
        void CreateAdmin(RegisterUserDTO entity);

        void DeleteUserById(Guid id);
        void UpdateUser(RegisterUserDTO user, Guid id);

        UserResponseTokenDTO Authentificate(LoginUserDTO model);
    }
}
