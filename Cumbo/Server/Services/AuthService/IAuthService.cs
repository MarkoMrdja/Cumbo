using Cumbo.Shared.DTOs;
using Cumbo.Shared;

namespace Cumbo.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(UserRegistrationDto userRegistrationDto);
        Task<ServiceResponse<string>> Login(UserLoginDto userLogin);
    }
}
