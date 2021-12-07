using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly BioProductStoreContext _context;

        public UserRepository(BioProductStoreContext context) : base(context)
        {
            _context = context;
        }

        // public User GetById(Guid id)
        // {
        //     return _table.FirstOrDefault(u => u.Id.Equals(id));
        // }

        public IQueryable<RespondUserDTO> GetAllUsers()
        {
            return _table.Select(x => new RespondUserDTO
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Username = x.Username,
            });
        }

        public IQueryable<RespondUserDTO> GetAllUsersByName(string name)
        {
            return _table.Where(x => x.FirstName.Equals(name) || x.LastName.Equals(name))
                        .Select(x => new RespondUserDTO
                        {
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            Username = x.Username,
                        });
        }

        public IQueryable<RespondUserDTO> GetAllUsersByEmail(string email)
        {
            return _table.Where(x => x.Email.Equals(email))
                        .Select(x => new RespondUserDTO
                        {
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            Username = x.Username,
                        });
        }
    }
}
