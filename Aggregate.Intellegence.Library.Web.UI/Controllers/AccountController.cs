using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using Aggregate.Intellegence.Library.Web.UI.Utility;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aggregate.Intellegence.Library.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthneticatioinService authneticatioinService;
        private readonly INotyfService notyfService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AccountController(IAuthneticatioinService authneticatioinService, INotyfService notyfService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.authneticatioinService = authneticatioinService;
            this.notyfService = notyfService;
            this.httpContextAccessor = httpContextAccessor;

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registerUser)
        {
            try
            {
                if (registerUser != null)
                {
                    var responce = await authneticatioinService.RegisterUser(registerUser);

                    notyfService.Success("Registration Successfull");

                    return Json(true);
                }

                notyfService.Error("Somethingwent wrong");

                return Json(false);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginVM userLogin)
        {
            try
            {
                var responce = await authneticatioinService.AuthenticateUser(userLogin);

                if (responce != null)
                {
                    if (!string.IsNullOrEmpty(responce.JwtToken))
                    {
                        httpContextAccessor.HttpContext.Session.SetString("AccessToken", responce.JwtToken);

                        var userClimes = await authneticatioinService.GenarateUserClaims(responce);

                        if (userClimes != null)
                        {
                            var claimsIdentity = UserPrincipal.GenarateUserPrincipal(userClimes);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                               new ClaimsPrincipal(claimsIdentity),
                                                               new AuthenticationProperties
                                                               {
                                                                   IsPersistent = true,
                                                                   ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                                                               });

                            return Json(new { appUser = userClimes, status = true });
                        }
                    }

                    notyfService.Error(responce.StatusMessage);
                }
                else
                {
                    notyfService.Error("Something went wrong");
                }

                return Json(responce);
            }
            catch (Exception ex)
            {
                notyfService.Error(ex.Message + ex.InnerException);

                throw ex;
            }

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account", null);
        }
    }
}
