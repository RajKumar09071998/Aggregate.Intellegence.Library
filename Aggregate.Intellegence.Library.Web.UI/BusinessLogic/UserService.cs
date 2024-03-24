using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;

namespace Aggregate.Intellegence.Library.Web.UI.BusinessLogic
{
    public class UserService : IUserService
    {
        public Task<bool> DeleteUser(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> FetchAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> FetchUser(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
