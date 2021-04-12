using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Microsoft.EntityFrameworkCore;
using Core.Handler;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPost _postHandler;
        private readonly IUser _userHandler;

        public HomeController(ILogger<HomeController> logger, IPost postHandler, IUser userHandler)
        {
            _logger = logger;
            _postHandler = postHandler;
            _userHandler = userHandler;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var userId = int.Parse(HttpContext.User.Claims.First().Value);
            return View(await _postHandler.GetPosts(10, userId));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(LoginModel model)
        {
            return View(model ?? new LoginModel());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> OnLogin(LoginModel model)
        {
            var userSalt = await _userHandler.GetUserSalt(model.Username);
            var userId = await _userHandler.CheckUser(model.Username, CreateHash(model.Password, userSalt));
            if (userId == 0)
            {
                return View("Login", new LoginModel { ErrorMessage = "Username or password is invalid." });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return Redirect("/");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Login", new LoginModel());
        }

        [AllowAnonymous]
        public IActionResult SignUp(SignUpModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> OnSignUp(SignUpModel model)
        {
            if (String.IsNullOrEmpty(model.Username))
            {
                return View("SignUp", new SignUpModel { ErrorMessage = "Username cannot be empty." });
            }
            if (String.IsNullOrEmpty(model.Password) || String.IsNullOrEmpty(model.RepeatedPassword))
            {
                return View("SignUp", new SignUpModel { ErrorMessage = "Passwords cannot be empty." });
            }
            if (model.Password != model.RepeatedPassword)
            {
                return View("SignUp", new SignUpModel { ErrorMessage = "Passwords do not match." });
            }
            var exists = await _userHandler.CheckUsernameExists(model.Username);
            if (exists)
            {
                return View("SignUp", new SignUpModel { ErrorMessage = "This username is already registered." });
            }

            var salt = CreateSalt();
            await _userHandler.CreateUser(model.Username, salt, CreateHash(model.Password, salt));

            return View("Login", new LoginModel());
        }

        string CreateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        string CreateHash(string pw, string salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pw,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 100,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
