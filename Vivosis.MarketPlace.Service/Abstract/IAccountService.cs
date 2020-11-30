using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Service.Abstract
{
    public interface IAccountService
    {
        bool Login(string userName, string password);
        IdentityResult AddUser(SystemUser user);
        IdentityResult UpdateUser(SystemUser user);
        IdentityResult DeleteUser(int userId);
    }
}
