using Blazored.LocalStorage;
using Cumbo.Shared;
using Cumbo.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Cumbo.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(
            IHttpClientFactory httpClientFactory, 
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;

        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto request)
        {
            var client = _httpClientFactory.CreateClient("Base");

            var response = await client.PostAsJsonAsync("api/Auth/Login", request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<string>>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("authToken", result.Data);
                await _authenticationStateProvider.GetAuthenticationStateAsync();

                return result;
            }

            return result;
        }

        public async Task<ServiceResponse<string>> Register(UserRegistrationDto request)
        {
            var client = _httpClientFactory.CreateClient("Base");

            var response = await client.PostAsJsonAsync("api/Auth/Register", request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<string>>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("authToken", result.Data);
                await _authenticationStateProvider.GetAuthenticationStateAsync();

                return result;
            }

            return result;
        }

        public bool LoginValidation(UserLoginDto user, out string error)
        {
            StringBuilder errorsString = new StringBuilder();

            if (string.IsNullOrEmpty(user.Username))
                errorsString.AppendLine("Username field cannot be empty!");

            if (string.IsNullOrEmpty(user.Password))
                errorsString.AppendLine("Password field cannot be empty!");

            error = errorsString.ToString();

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;

        }

        public bool RegistrationValidation(UserRegistrationDto user, out string error)
        {
            StringBuilder errorsString = new StringBuilder();

            if (string.IsNullOrEmpty(user.Username))
                errorsString.AppendLine("Username field cannot be empty!");

            if (string.IsNullOrEmpty(user.Password))
                errorsString.AppendLine("Password field cannot be empty!");

            if (!CheckPasswordRequirements(user.Password))
                errorsString.AppendLine("Password needs to contain at least one upper case letter, one lowercase letter and one number.");

            if (!user.Password.Equals(user.ConfirmPassword))
                errorsString.AppendLine("Passwords do not match!");

            if (user.Password.Length < 8)
                errorsString.AppendLine("Password has to be at least 8 characters long");

            error = errorsString.ToString();

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;
        }

        private bool CheckPasswordRequirements(string password)
        {
            Regex uppercaseRegex = new Regex(@"[A-Z]");
            Regex lowercaseRegex = new Regex(@"[a-z]");
            Regex numberRegex = new Regex(@"[0-9]");

            bool hasUpperCase = uppercaseRegex.IsMatch(password);
            bool hasLowerCase = lowercaseRegex.IsMatch(password);
            bool hasNumber = numberRegex.IsMatch(password);

            if (hasUpperCase && hasLowerCase && hasNumber)
                return true;
            else
                return false;
        }
    }
}
