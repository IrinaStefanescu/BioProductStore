using AutoMapper;
using BioProductStore.DTOs;
using BioProductStore.Services.AuthenticationService;
using BioProductStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BioProductStore.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(RegisterUserViewModel model)
        {
            var dto = _mapper.Map<RegisterUserDTO>(model);
            
            _authenticationService.RegisterUser(dto);

            return Ok();
        }
        
        [HttpPost("RegisterAdmin")]
        public IActionResult RegisterAdmin(RegisterAdminViewModel model)
        {
            var dto = _mapper.Map<RegisterUserDTO>(model);
            
            _authenticationService.RegisterAdmin(dto);

            return Ok();
        }
        
        [HttpPost("Login")]
        public IActionResult Login(LoginUserViewModel model)
        {
            var dto = _mapper.Map<LoginUserDTO>(model);
            
            var token = _authenticationService.Login(dto);

            return token is null ? new BadRequestResult() : Ok(token);
        }
    }
}