using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI.Infrastructure.Security;
using NetCoreModels.ViewModel;

namespace NetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;

        public LoginController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]LoginViewModel login)
        {
            IActionResult response = Unauthorized();
            var token = _tokenManager.AuthenticateUser(login.Username, login.Password);

            if (!string.IsNullOrWhiteSpace(token))
            {                
                response = Ok(new { token = token });
            } 
            return response;
        } 
    }
}