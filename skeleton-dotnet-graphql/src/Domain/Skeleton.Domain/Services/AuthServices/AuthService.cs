using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Skeleton.Domain.Enum;
using Skeleton.Domain.Models.Users;
using Skeleton.Domain.Options;
using Skeleton.Domain.Services.AuthServices.Abstractions;
using Skeleton.Domain.Services.Core;

namespace Skeleton.Domain.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly AuthOptions _authOptions;
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthService> _logger;

        public AuthService(AuthOptions authOptions, IUserService userService, ILogger<AuthService> logger, IPasswordHasher<User> passwordHasher)
        {

            _authOptions = authOptions;
            _userService = userService;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<(AuthError, string Token)> LoginAsync(User userAuth)
        {
            using (_logger.BeginScope("User login to get an access token."))
            {
                _logger.LogDebug("Received user informations: {@user}", userAuth);

                if (string.IsNullOrEmpty(userAuth.Login))
                {
                    _logger.LogWarning("Received user has not username.");
                    return (AuthError.Forbidden, null);
                }

                _logger.LogInformation($"User name: {userAuth.Login}");

                User user = await _userService.GetAsync(u => u.Login == userAuth.Login.Trim());
                if (user == null)
                {
                    _logger.LogWarning("Received user cannot be find as an enabled user.");
                    return (AuthError.Forbidden, null);
                }

                _logger.LogDebug("Found user informations: {@user}", user);

                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, userAuth.Password.Trim());

                if (result == PasswordVerificationResult.Failed)
                {
                    return (AuthError.Forbidden, null);
                }
                string accessToken = CreateToken(user);

                return (AuthError.None, accessToken);
            }
        }
        
        public async Task<string> CreateResetToken(string mail)
        {
            User user = await _userService.GetAsync(u => u.Login == mail.Trim());

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authOptions.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim("type", "reset")
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authOptions.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddSeconds(_authOptions.TokenExpires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}