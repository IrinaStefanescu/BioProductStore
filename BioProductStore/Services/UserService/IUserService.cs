using BioProductStore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services
{
    public interface IUserService
    {
        RegisterUserDTO GetUserByUserId(Guid Id);
        public IQueryable<RespondUserDTO> GetAllUsers();
        public IQueryable<RespondUserDTO> GetAllUsersByName(string name);

        public IQueryable<RespondUserDTO> GetAllUsersByEmail(string email);

        void CreateUser(RegisterUserDTO entity);

        void DeleteUserById(Guid id);
        void UpdateUser(RegisterUserDTO user, Guid id);
    }
}
