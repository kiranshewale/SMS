using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMSCore.Models;
using SMSCore.Services.AccountsService;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SMSCore.Controllers
{
    public class AccountController: Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model,
        string returnUrl)
        {

            if (ModelState.IsValid)
            {

                bool isUservalid = false;
                var user = await _accountService.ValidateUserLoginAsync(model.UserName, model.Password);

                if (user != null)
                    isUservalid = true;

                if (isUservalid)
                {
                    //var numberOfLogins = user.NumLogin + 1;
                    //var lastLogin = DateTime.UtcNow;

                    //update member last login date and number of login attempts
                    //user.NumLogin = numberOfLogins;
                    // user.Lastlogin = lastLogin;
                    // await _accountService.UpdateMemberPortAsync(user);

                    var fullName = user.FirstName + " " + user.LastName;
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, fullName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var props = new AuthenticationProperties();
                    props.IsPersistent = true;

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }

            }

            return RedirectToAction("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
