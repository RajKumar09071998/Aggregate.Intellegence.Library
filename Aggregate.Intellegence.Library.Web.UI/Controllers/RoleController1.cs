using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;

namespace Aggregate.Intellegence.Library.Web.UI.Controllers
{
    public class RoleController1 : Controller
    {
        private readonly IRoleService roleService;
        private readonly NotyfService notyfService;
        public RoleController1(IRoleService roleService,
                               NotyfService notyfService)
        {
            this.roleService = roleService;
            this.notyfService= notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> FetchAllRoles()
        {
            try
            {
                var response = await roleService.FetchAllRoles();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateRole(Role role)
        {
            try
            {
                if (role != null)
                {
                    var response = await roleService.InsertOrUpdateRole(role);
                    if (response)
                    {
                        if (role.Id > 0)
                        {
                            notyfService.Success("Role Updated Successfully");
                        }
                        notyfService.Success("Role Insertred Successfully");
                        return Json(true);
                    }
                    notyfService.Warning("Something went wrong");
                    return Json(false);
                }
                notyfService.Warning("Something went wrong");
                return Json(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetARole(long bookId)
        {
            try
            {
                var response = await roleService.GetRole(bookId);
                return response != null ? Json(true) : Json(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(long rolId)
        {
            try
            {
                var response = await roleService.DeleteRole(rolId);
                return response != null ? Json(true) : Json(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
