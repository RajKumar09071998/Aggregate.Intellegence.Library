using Aggregate.Intellegence.Library.Web.Service.AppDBContext;
using Aggregate.Intellegence.Library.Web.Service.Helpers;
using Aggregate.Intellegence.Library.Web.Service.Interfaces;
using Aggregate.Intellegence.Library.Web.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Aggregate.Intellegence.Library.Web.Service.Repository
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext dBContext;
        public UserService(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<bool> DeleteUser(long userId)
        {
            var user = await dBContext.users.FindAsync(userId);
            if (user != null)
            {
                dBContext.users.Remove(user);
            }
            var response = await dBContext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<List<User>> FetchAllUsers()
        {
            return await dBContext.users.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<User> FetchUser(long userId)
        {
            var user = await dBContext.users.FindAsync(userId);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> RegisterUser(RegisterUser registerUser)
        {
            if (registerUser.Id == 0 || registerUser.Id == null)
            {
                //create user 
                if (!string.IsNullOrEmpty(registerUser.Password))
                {
                    HashSalt hashSalt = HashSalt.GenerateSaltedHash(registerUser.Password);
                    User user = new User();

                    if (registerUser.Id.HasValue)
                        user.Id = registerUser.Id.Value;

                    user.FirstName = registerUser.FirstName;
                    user.LastName = registerUser.LastName;
                    user.RoleId = registerUser.RoleId;
                    user.Email = registerUser.Email;
                    user.Phone = registerUser.Phone;
                    user.PasswordHash = hashSalt.Hash;
                    user.PasswordSalt = hashSalt.Salt;
                    user.CreatedBy = -1;
                    user.CreatedOn = DateTimeOffset.Now;
                    user.ModifiedBy = -1;
                    user.ModifiedOn = DateTimeOffset.Now;
                    user.IsActive = true;
                    await dBContext.users.AddAsync(user);
                }
                else
                {
                    return false;
                }
            }
            else
            {

            }
            var responce = await dBContext.SaveChangesAsync();

            return responce == 1 ? true : false;
        }

    }
}
