using NetCoreInterface;
using NetCoreModels;
using System;
using System.Linq;

namespace NetCoreService
{ 
    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public UserModel Get(string username, string password)
        {
            return new UserList().Users
                .Where(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase))
                .Where(u => u.Password.Equals(password, StringComparison.InvariantCulture))
                .FirstOrDefault();
        }
    }
}
