using Aggregate.Intellegence.Library.Web.Service.Interfaces;
using Aggregate.Intellegence.Library.Web.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aggregate.Intellegence.Library.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticateService authenticateService;
        public AccountController(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUser registerUser)
        {

            try
            {
                var response = await authenticateService.RegisterUser(registerUser);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(UserLoginVM userLogin)
        {
            try
            {
                var responce = await authenticateService.AuthenticateUser(userLogin);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("GenarateUserClaims")]
        public async Task<IActionResult> GenarateUserClaims(AuthenticationResponce authentication)
        {
            try
            {
                var responce = await authenticateService.GenarateUserClaims(authentication);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
