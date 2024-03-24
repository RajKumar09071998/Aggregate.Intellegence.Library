using Aggregate.Intellegence.Library.Web.Service.Interfaces;
using Aggregate.Intellegence.Library.Web.Service.Models;
using Aggregate.Intellegence.Library.Web.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aggregate.Intellegence.Library.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpPost]
        [Route("InsertOrUpdateRole")]
        public async Task<IActionResult> InsertOrUpdateRole(Role role)
        {

            try
            {
                var response = await roleService.InsertOrUpdateRole(role);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("{roleId:long}")]
        public async Task<IActionResult> GetRole(long roleId)
        {
            try
            {
                var response = await roleService.GetRole(roleId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        [HttpDelete]
        [Route("{roleId:long}")]
        public async Task<IActionResult> DeleteBook(long roleId)
        {
            try
            {
                var response = await roleService.DeleteRole(roleId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("FetchAllRoles")]
        public async Task<IActionResult> FetchAllRoles()
        {
            try
            {
                var response = await roleService.FetchAllRoles();
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
