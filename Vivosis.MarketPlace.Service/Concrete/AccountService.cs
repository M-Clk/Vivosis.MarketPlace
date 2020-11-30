using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;

namespace Vivosis.MarketPlace.Service.Concrete
{
    public class AccountService :IAccountService
    {
        MarketPlaceDbContext _dbContext;
        UserManager<SystemUser> _userManager;
        SignInManager<SystemUser> _signInManager;
        public AccountService(MarketPlaceDbContext dbContext, UserManager<SystemUser> userManager, SignInManager<SystemUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public bool Login(string userName, string password)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            if(user == null)
                return false;
            var result = _signInManager.PasswordSignInAsync(user, password, true, true).Result;
            return result.Succeeded;
        }
        public IdentityResult AddUser(SystemUser user)
        {
            if(!CheckDbConnection(user.Server, user.DbName, user.DbUserName, user.DbPassword))
                throw new InvalidOperationException("Hedef veritabanina baglanilamadi. Lutfen bilgilerinizi kontorl edin. Veritabaninizin uzaktan erisilebilir olduguna emin olun.");
            var pass = user.PasswordHash;
            user.PasswordHash = null;
            return _userManager.CreateAsync(user, pass).Result;
        }
        public IdentityResult UpdateUser(SystemUser user)
        {
            if(!CheckDbConnection(user.Server, user.DbName, user.DbUserName, user.DbPassword))
                throw new InvalidOperationException("Hedef veritabanina baglanilamadi. Lutfen bilgilerinizi kontorl edin. Veritabaninizin uzaktan erisilebilir olduguna emin olun.");
            return _userManager.UpdateAsync(user).Result;
        }
        public IdentityResult DeleteUser(int userId)
        {
            var user = _dbContext.Users.Find(userId);
            return _userManager.DeleteAsync(user).Result;
        }
        private bool CheckDbConnection(string server, string dbName, string dbUserName, string dbPassword)
        {
            var newConnectionString = $"Server={server}; Database={dbName}; Uid={dbUserName}; Pwd={dbPassword};";
            var connection = new MySqlConnection(newConnectionString);
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
