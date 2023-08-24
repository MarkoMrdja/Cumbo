using Cumbo.Shared.DTOs;
using Cumbo.Shared;

namespace Cumbo.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(UserLoginDto request);
        Task<ServiceResponse<string>> Register(UserRegistrationDto request);
        bool LoginValidation(UserLoginDto user, out string error);
        bool RegistrationValidation(UserRegistrationDto user, out string error);
    }
}
