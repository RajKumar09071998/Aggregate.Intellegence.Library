using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aggregate.Intellegence.Library.Web.UI.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        private readonly INotyfService notyfService;
        public RoleController(IRoleService roleService,
                               INotyfService notyfService)
        {
            this.roleService = roleService;
            this.notyfService= notyfService;
        }
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> InsertOrUpdateRole([FromBody]Role role)
        {
            try
            {
                if (role != null)
                {
                    var response = await roleService.InsertOrUpdateRole(role);
                    if (response)
                    {
                        if (role.Id > 0 && role.IsActive==true)
                        {
                            notyfService.Success("Role Updated Successfully");
                        }
                        else if(role.Id > 0 && role.IsActive == false)
                        {
                            notyfService.Success("Role Deleted Successfully");
                        }
                        else
                        {
                            notyfService.Success("Role Inserted Successfully");
                            return Json(true);
                        }
                    }
                    else
                    {
                        notyfService.Warning("Something went wrong");
                        return Json(false);
                    }
                }
                else
                {
                    notyfService.Warning("Something went wrong");
                    return Json(false);
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Authorize]
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
        [Authorize]
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
