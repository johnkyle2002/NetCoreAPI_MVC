using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreMVC.Commons.Helper;
using NetCoreMVC.Infrastructure.Security;
using NetCoreMVC.Models;
using NetCoreMVC.Models.ViewModel;
using Newtonsoft.Json;

namespace NetCoreMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClientHelper _clientHelper;
        private readonly ITokenManager _tokenManager;

        public HomeController(IClientHelper clientHelper, ITokenManager tokenManager)
        {
            _clientHelper = clientHelper;
            _tokenManager = tokenManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        class AuthenticationModel
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }


        
        public async Task<IActionResult> Privacy()
        {
            System.Threading.Thread.Sleep(100);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel usrLogin, string returnURL)
        {
            if (!ModelState.IsValid)
                return View(usrLogin);

            var respo = await _clientHelper.Authenticate("api/login", new { username = usrLogin.Username, password = usrLogin.Password });

            if (respo.IsSuccessStatusCode)
            {
                dynamic result = respo.Content.ReadAsStringAsync().Result;
                var token = (JsonConvert.DeserializeObject<AuthenticationModel>(result)).Token;
                await _tokenManager.Authenticate(token);
                if (string.IsNullOrWhiteSpace(returnURL))
                {
                    return RedirectToAction("Index");
                }
                return Redirect(returnURL);
            }
            ModelState.AddModelError("error", "Invalid username or password.");
            return View(usrLogin);
        }
    }
}
