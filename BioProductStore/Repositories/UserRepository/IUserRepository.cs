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
        List<User> GetAllUsers();
        List<User> GetAllUsersByName(string name);
        User GetByUsername(string name);
        User GetByEmail(string name);
    }
}
