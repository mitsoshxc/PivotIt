using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PivotIt.Infrastructure.Services.Users;
using PivotIt.Web.Helpers;
using PivotIt.Web.Models.Account;

namespace PivotIt.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index), "Dashboard");
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginForm loginForm)
        {
            var user = await _userService.GetSiteUserByUserNameAsync(loginForm.Username).ConfigureAwait(false);

            if (user == null)
            {
                return View();
            }

            var salt = Convert.FromBase64String(user.PasswordSalt);
            var hashed = PasswordExtensions.GenerateHash(loginForm.Password, salt);

            if (hashed != user.PasswordHash)
            {
                return View();
            }

            var identityClaims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identityClaims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identityClaims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identityClaims.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identityClaims.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            identityClaims.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("o"), ClaimValueTypes.DateTime));
            //identityClaims.AddClaim(new Claim(ClaimTypes.Role, "User"));

            var claimsPrincipal = new ClaimsPrincipal(identityClaims);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true, // "Remember Me"
                    IssuedUtc = DateTimeOffset.UtcNow,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                }).ConfigureAwait(false);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterForm registerForm)
        {
            if (registerForm != null && ModelState.IsValid)
            {
                var salt = PasswordExtensions.GenerateSalt();

                var hashed = PasswordExtensions.GenerateHash(registerForm.Password, salt);

                var saltText = Convert.ToBase64String(salt);

                var siteUser = new Infrastructure.Entities.SiteUser
                {
                    UserName = registerForm.Username,
                    PasswordSalt = saltText,
                    PasswordHash = hashed,
                    Email = registerForm.Email,
                    FirstName = registerForm.FirstName,
                    LastName = registerForm.LastName,
                    RegistrationDate = DateTime.Now,
                    DateOfBirth = DateTime.Now
                };

                if (await _userService.TryAddUserAsync(siteUser).ConfigureAwait(false))
                {
                    return RedirectToAction(nameof(Index), "Home");
                }

                ModelState.AddModelError("UserNotAdded", $"Could not get you registered. Probably {registerForm.Username} or {registerForm.Email} already exist.");
            }

            return View(registerForm);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}