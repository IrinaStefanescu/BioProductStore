using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BioProductStore.DataAccess;
using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Utilities.JWTUtils;

namespace BioProductStore.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;
        private IJWTUtils _jwtUtils;

        public AuthenticationService(UnitOfWork uow, IMapper mapper, IJWTUtils utils)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtUtils = utils;
        }

        public void RegisterAdmin(RegisterUserDTO user)
        {
            Register(user, Role.Admin);
        }
        
        public void RegisterUser(RegisterUserDTO user)
        {
            Register(user, Role.User);
        }

        private void Register(RegisterUserDTO user, Role role)
        {
            var entity = _mapper.Map<User>(user);

            entity.Role = role;
            entity.DateModified = DateTime.Now;
            entity.PasswordHash = Convert.ToBase64String(
                MD5.Create().
                    ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
            
            _uow.User.Create(entity);
            _uow.SaveChanges();
        }
        
        public string Login(LoginUserDTO user)
        {
            var entity = _uow.User.FindBy(e => e.Username == user.Username)
                ?? throw new ArgumentException($"User {user.Username} not found", nameof(user));

            var hash = Convert.ToBase64String(
                MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password)));

            return entity.PasswordHash == hash ? _jwtUtils.GenerateJWTToken(entity) : null;

        }
    }
}