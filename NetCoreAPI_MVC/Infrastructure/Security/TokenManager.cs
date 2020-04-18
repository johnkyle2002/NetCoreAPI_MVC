using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCoreInterface;
using NetCoreModels;
using NetCoreService;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetCoreAPI.Infrastructure.Security
{
    public interface ITokenManager
    {
        string AuthenticateUser(string username, string password);
    }

    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public TokenManager(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        public string AuthenticateUser(string username, string password)
        {
            var model = _userService.Get(username, password);

            if (model != null)
            {
                return GenerateAuthJSONWebToken(model);
            }

            return string.Empty;
        }

        private string GenerateAuthJSONWebToken(UserModel userModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[] {
                new Claim("username", userModel.UserName),
                new Claim(ClaimTypes.Email, userModel.Email),
                new Claim("role", userModel.Role),
                new Claim(ClaimTypes.Name, userModel.Name)
            };

            var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
