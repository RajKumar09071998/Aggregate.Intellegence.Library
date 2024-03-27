using Aggregate.Intellegence.Library.Web.Service.Models;
using System.Threading.Tasks;

namespace Aggregate.Intellegence.Library.Web.Service.Interfaces
{
    public interface IAuthenticateService
    {
        Task<bool> RegisterUser(RegisterUser regisetUser);
        Task<AuthenticationResponce> AuthenticateUser(UserLoginVM login);
        Task<ApplicationUser> GenarateUserClaims(AuthenticationResponce authenticationResponce);
        Task<List<User>> FetchAllUsers();
    }
}
