using Aggregate.Intellegence.Library.Web.UI.Model;

namespace Aggregate.Intellegence.Library.Web.UI.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterUser(User user);
        Task<bool> DeleteUser(long userId);
        Task<User> FetchUser(long userId);
        Task<List<User>> FetchAllUsers();
    }
}
