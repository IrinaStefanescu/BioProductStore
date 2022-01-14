using BioProductStore.Repositories.UserRepository;
using BioProductStore.Utilities.JWTUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Utilities
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JWTMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext httpContext, IUserRepository userRepository, IJWTUtils jwtUtils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(); //luam din Autorization acel bearer token
            var userId = jwtUtils.ValidateJWTToken(token);

            if (userId != Guid.Empty)
            {
                httpContext.Items["User"] = userRepository.FindById(userId);
            }

            await _next(httpContext);
        }
    }
}
