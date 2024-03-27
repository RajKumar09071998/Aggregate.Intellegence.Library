using Aggregate.Intellegence.Library.Web.UI.Model;

namespace Aggregate.Intellegence.Library.Web.UI.Interface
{
    public interface IAuthneticatioinService
    {
        public Task<bool> RegisterUser(RegisterUser registerUser);
        public Task<List<User>> FetchAllUsers();
        public Task<AuthenticationResponce> AuthenticateUser(UserLoginVM loginVM);

        public Task<MVCApplicationUser> GenarateUserClaims(AuthenticationResponce responce);
    }
}
