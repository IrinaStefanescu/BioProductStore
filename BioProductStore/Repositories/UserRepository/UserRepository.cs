using BioProductStore.Data;
using BioProductStore.DTOs;
using BioProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        /*public IQueryable<RespondUserDTO> GetAllUsers()
        {
          
            return _table.Select(x => new RespondUserDTO
            {
                FirstName = x.FirstName,
               LastName = x.LastName,
                Email = x.Email,
                Username = x.Username,
            });
        }*/
        public List<User> GetAllUsers()
        {
            return new List<User>(_context.Users.AsNoTracking().ToList());
        }

        /*public IQueryable<RespondUserDTO> GetAllUsersByName(string name)
        {
            return _table.Where(x => x.FirstName.Equals(name) || x.LastName.Equals(name))
                        .Select(x => new RespondUserDTO
                        {
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            Username = x.Username,
                        });
        }*/
        public List<User> GetAllUsersByName(string name)
        {
            return new List<User>(_context.Users.Where(u => u.FirstName.Equals(name)
            || u.LastName.Equals(name)));
        }

       /* public IQueryable<RespondUserDTO> GetAllUsersByEmail(string email)
        {
            return _table.Where(x => x.Email.Equals(email))
                        .Select(x => new RespondUserDTO
                        {
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            Username = x.Username,
                        });
        }*/
       public User GetByUsername(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Username.Equals(name));
        }

        public User GetByEmail(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Email.Equals(name));
        }
    }
}
