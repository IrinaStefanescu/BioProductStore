using System;
using System.Linq;
using System.Threading.Tasks;
using BioProductStore.Data;
using BioProductStore.DataAccess;
using BioProductStore.Models;
using BioProductStore.Repositories;
using BioProductStore.Utilities.JWTUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BioProductStore.Utilities
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IGenericRepository<User> _userRepository;
            
        public JWTMiddleware(
            RequestDelegate next,
            IOptions<AppSettings> appSettings
        )
        {
            _next = next;
            _appSettings = appSettings.Value;
            
            var connectionstring = "Server=DESKTOP-RPGRQME\\MSSQLSERVER2;Database=BioProductsStore2;Trusted_Connection=True;MultipleActiveResultSets=true";

            var optionsBuilder = new DbContextOptionsBuilder<BioProductStoreContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            
            _userRepository = new GenericRepository<User>(new BioProductStoreContext(optionsBuilder.Options));
        }

        public async Task Invoke(HttpContext httpContext, IGenericRepository<User> userRepository, IJWTUtils jwtUtils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(); //luam din Autorization acel bearer token
            var userId = jwtUtils.ValidateJWTToken(token);

            if (userId != Guid.Empty)
            {
                var user = _userRepository.FindById(userId) as User;
                httpContext.Items["User"] = user;
            }

            await _next(httpContext);
        }
    }
    
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTMiddleware>();
        }
    }

}
