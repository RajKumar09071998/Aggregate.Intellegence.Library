using Aggregate.Intellegence.Library.Web.Service.AppDBContext;
using Aggregate.Intellegence.Library.Web.Service.Interfaces;
using Aggregate.Intellegence.Library.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Aggregate.Intellegence.Library.Web.Service.Repository
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDBContext dBContext;
        public RoleService(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<bool> DeleteRole(long roleId)
        {
            var role = await dBContext.roles.FindAsync(roleId);
            if (role != null)
            {
                dBContext.roles.Remove(role);
            }
            var response = await dBContext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<List<Role>> FetchAllRoles()
        {
            return await dBContext.roles.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<Role> GetRole(long roleId)
        {
            var role = await dBContext.roles.FindAsync(roleId);
            if (role != null)
            {
                return role;
            }
            return null;
        }

        public async Task<bool> InsertOrUpdateRole(Role role)
        {
            if (role != null)
            {
                if (role.Id > 0)
                {
                    var dbRole = await dBContext.roles.FindAsync(role.Id);
                    if (dbRole != null)
                    {
                        dbRole.Id = role.Id;
                        dbRole.Name = role.Name;
                        dbRole.Code = role.Code;
                        dbRole.ModifiedOn = DateTime.Now;
                        dbRole.ModifiedBy = role.ModifiedBy;
                        dbRole.IsActive = role.IsActive;
                    }
                }
                else if (await dBContext.roles.AnyAsync(x => x.Name.ToLower().Trim() == role.Name.ToLower().Trim()))
                {
                    var dbRole = await dBContext.roles.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == role.Name.ToLower().Trim());
                    if (dbRole != null)
                    {
                        dbRole.Id = role.Id;
                        dbRole.Name = role.Name;
                        dbRole.Code = role.Code;
                        dbRole.ModifiedOn = DateTime.Now;
                        dbRole.ModifiedBy = role.ModifiedBy;
                        dbRole.IsActive = role.IsActive;
                    }
                }
                else
                {
                    await dBContext.roles.AddAsync(role);

                }
                var response = await dBContext.SaveChangesAsync();
                return response == 1 ? true : false;
            }
            return false;
        }
    }
}
