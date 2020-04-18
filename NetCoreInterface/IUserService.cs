using NetCoreModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreInterface
{
    public interface IUserService
    {
        UserModel Get(string username, string password);
    }
}
