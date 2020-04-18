using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreModels
{
    public class UserList
    {

        public List<UserModel> Users;

       
        public UserList()
        {
            Initialize();
        }

        private void Initialize()
        {
           this.Users = new List<UserModel>
            {
                new UserModel{ Name = "Mark Diaz",  UserName = "user1", Email = "Email1@email.com", Password = "password1", Role = "admin"},
                new UserModel{ Name = "Jeffrey Soriano",  UserName = "user2", Email = "Email2@email.com", Password = "password2", Role = "Manager" }, 
                new UserModel{ Name = "Angel Locsin",  UserName = "user3", Email = "Email3@email.com", Password = "password3", Role = "Lead" },
                new UserModel{ Name = "Sunshine Cruz",  UserName = "user4", Email = "Email4@email.com", Password = "password4", Role = "" }
            };
        }

    }
}
