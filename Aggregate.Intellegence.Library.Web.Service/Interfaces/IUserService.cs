using Aggregate.Intellegence.Library.Web.Service.Models;

namespace Aggregate.Intellegence.Library.Web.Service.Interfaces
{
    public interface IUserService
    {
        
        Task<bool> DeleteUser(long userId);
        Task<User> FetchUser(long userId);
        Task<List<User>> FetchAllUsers();
    }
}
