using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface IAccountService
    {
        bool Login(string userName, string password, bool rememberMe);
        IdentityResult AddUser(SystemUser user, bool isAdmin = false);
        IdentityResult UpdateUser(SystemUser user, bool isAdmin = false);
        IdentityResult DeleteUser(int userId);
    }
}
