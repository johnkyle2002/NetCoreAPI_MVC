using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMVC.Infrastructure.Security
{
    public interface ITokenManager
    {
        ClaimsPrincipal GetPrincipal(string token);
        string ValidateToken(string token);
        Task<bool> Authenticate(String jwtToken);
    }

    public class TokenManager : ITokenManager
    {
        #region Private Member
        
        private readonly IConfiguration _config;
        private readonly HttpContext _httpContext;

        #endregion

        #region Constructor

        public TokenManager(IConfiguration config, IHttpContextAccessor httpContext)
        {
            _config = config;
            _httpContext = httpContext.HttpContext;
        }

        #endregion

        #region Public Method

        public string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim usernameClaim = identity.FindFirst("username");
            username = usernameClaim.Value;
            return username;
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(_config["Jwt:Key"]);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Authenticate(String jwtToken)
        {
            try
            {
                // No use if token is empty
                if (string.IsNullOrWhiteSpace(jwtToken))
                    return false;
                 
               await LogoutAsync();

                // Setup handler for processing Jwt token
                var tokenHandler = new JwtSecurityTokenHandler();

                var settings = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
                };

                // Retrieve principal from Jwt token
                var principal = tokenHandler.ValidateToken(jwtToken, settings, out var validatedToken);

                // Cast needed for accessing claims property
                var identity = principal.Identity as ClaimsIdentity;

                // parse jwt token to get all claims
                var securityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

                // Search for missed claims, for example claim 'sub'
                var extraClaims = securityToken.Claims.Where(c => !identity.Claims.Any(x => x.Type == c.Type)).ToList();

                // Adding the original Jwt has 2 benefits:
                //  1) Authenticate REST service calls with orginal Jwt
                //  2) The original Jwt is available for renewing during sliding expiration
                extraClaims.Add(new Claim("jwt", jwtToken));

                // Merge claims
                identity.AddClaims(extraClaims);

                // Setup authenticaties 
                // ExpiresUtc is used in sliding expiration 
                var authenticationProperties = new AuthenticationProperties()
                {
                    //IssuedUtc = identity.Claims.First(c => c.Type == JwtRegisteredClaimNames.Iat)?.Value.ToInt64().ToUnixEpochDate(),
                    //ExpiresUtc = identity.Claims.First(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value.First()..ToInt64().ToUnixEpochDate(),
                    ExpiresUtc = DateTime.Now.AddMinutes(15),
                    IsPersistent = true

                };

                // The actual Login
                await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

                return identity.IsAuthenticated;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        #endregion
    }
}
