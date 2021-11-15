using System;
using System.Threading.Tasks;
using FamilyWebAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class UserSqliteService : IUserService
    {
        private UserContext _userContext;

        public UserSqliteService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            var first = await _userContext.Users.FirstAsync(user => user.UserName.Equals(userName));
            if (first == null) {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password)) {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}