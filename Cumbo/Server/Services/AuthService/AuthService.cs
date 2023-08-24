using Cumbo.Server.Configurations;
using Cumbo.Server.Data;
using Cumbo.Server.Models;
using Cumbo.Shared;
using Cumbo.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cumbo.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(
            AppDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Register(UserRegistrationDto userRegistrationDto)
        {
            var user_exist = await _context.Users.FirstOrDefaultAsync(x => x.Username.Equals(userRegistrationDto.Username));

            if (user_exist != null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            if (!userRegistrationDto.Password.Equals(userRegistrationDto.ConfirmPassword))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Inputted passwords doesn't match."
                };
            }

            CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var new_user = new User
            {
                Username = userRegistrationDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(new_user);
            await _context.SaveChangesAsync();

            bool is_created = _context.Users.Contains(new_user);

            if (is_created)
            {
                var token = GenerateJwtToken(new_user);

                return new ServiceResponse<string>
                {
                    Success = true,
                    Data = token,
                    Message = "User is registrated successfully."
                };
            }

            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Server error."
            };
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto userLogin)
        {
            var response = new ServiceResponse<string>();
            var existing_user = await _context.Users.FirstOrDefaultAsync(x => x.Username.Equals(userLogin.Username));

            if (existing_user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(userLogin.Password, existing_user.PasswordHash, existing_user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                var token = GenerateJwtToken(existing_user);

                response.Success = true;
                response.Message = "Login successful.";
                response.Data = token;
            }
            return response;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);

            }
        }

        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            //Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
