using Aggregate.Intellegence.Library.Web.Service.Models;

namespace Aggregate.Intellegence.Library.Web.Service.Interfaces
{
    public interface IRoleService
    {
        Task<bool> InsertOrUpdateRole(Role role);
        Task<bool> DeleteRole(long roleId);
        Task<Role> GetRole(long roleId);
        Task<List<Role>> FetchAllRoles();
        
    }
}
