using Aggregate.Intellegence.Library.Web.UI.Model;

namespace Aggregate.Intellegence.Library.Web.UI.Interface
{
    public interface IRoleService
    {
        Task<bool> InsertOrUpdateRole(Role role);
        Task<bool> DeleteRole(long roleId);
        Task<Role> GetRole(long roleId);
        Task<List<Role>> FetchAllRoles();
    }
}
