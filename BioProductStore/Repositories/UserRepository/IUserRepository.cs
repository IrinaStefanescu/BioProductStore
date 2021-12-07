using BioProductStore.DTOs;
using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IQueryable<RespondUserDTO> GetAllUsers();
        IQueryable<RespondUserDTO> GetAllUsersByName(string name);
        IQueryable<RespondUserDTO> GetAllUsersByEmail(string email);
    }
}
