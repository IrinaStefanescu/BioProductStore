using BioProductStore.DTOs;

namespace BioProductStore.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        void RegisterAdmin(RegisterUserDTO user);
        void RegisterUser(RegisterUserDTO user);
        string Login(LoginUserDTO user);
    }
}