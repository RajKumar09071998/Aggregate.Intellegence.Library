using Aggregate.Intellegence.Library.Web.Service.Interfaces;
using Aggregate.Intellegence.Library.Web.Service.Models;
using Aggregate.Intellegence.Library.Web.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aggregate.Intellegence.Library.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        
        [HttpGet]
        [Route("{userId:long}")]
        public async Task<IActionResult> FetchUser(long userId)
        {
            try
            {
                var response = await userService.FetchUser(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        [HttpDelete]
        [Route("{userId:long}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            try
            {
                var response = await userService.DeleteUser(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("FetchAllUsers")]
        public async Task<IActionResult> FetchAllUsers()
        {
            try
            {
                var response = await userService.FetchAllUsers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
