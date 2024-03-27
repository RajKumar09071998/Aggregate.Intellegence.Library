using Aggregate.Intellegence.Library.Web.UI.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Aggregate.Intellegence.Library.Web.UI.Utility
{
    public class UserPrincipal
    {
        public static ClaimsPrincipal GenarateUserPrincipal(MVCApplicationUser user)
        {
            var roleName = user.RoleId.ToString();

            var claims = new List<Claim>
     {
        new Claim("Id", user.Id.ToString()),
        new Claim("Phone", user.Phone),
        new Claim("Email", user.Email),
        new Claim("FirstName", user.FirstName),
        new Claim("LastName", user.LastName),
       new Claim(ClaimTypes.Role, roleName)
     };
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            return principal;
        }
    }
}
