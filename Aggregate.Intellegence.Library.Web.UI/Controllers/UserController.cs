using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregate.Intellegence.Library.Web.UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly INotyfService notyfService;
        public UserController(IUserService userService,
                               INotyfService notyfService)
        {
            this.userService = userService;
            this.notyfService = notyfService;
        }
        
        [HttpGet]
        public async Task<IActionResult> FetchAllUsers()
        {
            try
            {
                var response = await userService.FetchAllUsers();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> FetchUser(long userId)
        {
            try
            {
                var response = await userService.FetchUser(userId);
                return response != null ? Json(true) : Json(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            try
            {
                var response = await userService.DeleteUser(userId);
                return response != null ? Json(true) : Json(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
