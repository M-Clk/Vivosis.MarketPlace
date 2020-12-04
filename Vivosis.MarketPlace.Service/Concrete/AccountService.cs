extern alias MySqlConnectorAlias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        AccountDbContext _accountDbContext;
        UserManager<SystemUser> _userManager;
        SignInManager<SystemUser> _signInManager;
        IHttpContextAccessor _httpContextAccessor;
        IConfiguration _configuration;
        public AccountService(AccountDbContext accountDbContext, UserManager<SystemUser> userManager, SignInManager<SystemUser> signInManager, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _accountDbContext = accountDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public bool Login(string userName, string password, bool rememberMe)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            if(user == null)
                return false;
            _signInManager.SignOutAsync().Wait();
            var result = _signInManager.PasswordSignInAsync(user, password, rememberMe, true).Result;
            if(result.Succeeded)
            {
                var connectionString = $"Server={user.Server}; Database={user.DbName}; Uid={user.DbUserName}; Pwd={user.DbPassword};";
                CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(1), IsEssential = true };
                _httpContextAccessor.HttpContext.Response.Cookies.Append("VivosisConnectionString", connectionString, option);
            }
            return result.Succeeded;
        }
        public IdentityResult AddUser(SystemUser user, bool isAdmin = false)
        {
            if(!isAdmin && !CheckDbConnection(user.Server, user.DbName, user.DbUserName, user.DbPassword))
                throw new DBConcurrencyException("Hedef veritabanina baglanilamadi. Lutfen bilgilerinizi kontorl edin. Veritabaninizin uzaktan erisilebilir olduguna emin olun.");
            var pass = user.PasswordHash;
            MarketPlaceDbContext dbContext = null;
            user.PasswordHash = null;
            var role = isAdmin ? "Admin" : "Customer";
            if(!isAdmin)
            {
                var options = new DbContextOptionsBuilder<MarketPlaceDbContext>();
                var connectionString = string.Format(_configuration.GetConnectionString("DynamicLocalDatabase"), $"db_{user.UserName.ToLower()}");
                options.UseMySql(connectionString);
                dbContext = new MarketPlaceDbContext(options.Options);
                dbContext.Database.EnsureCreated();
            }
            var result = _userManager.CreateAsync(user, pass).Result;
            if(result.Succeeded)
                _userManager.AddToRoleAsync(user, role).Wait();
            else if(!isAdmin)
                dbContext?.Database.EnsureDeleted();
            return result;
        }
        public IdentityResult UpdateUser(SystemUser user, bool isAdmin)
        {
            if(!isAdmin && !CheckDbConnection(user.Server, user.DbName, user.DbUserName, user.DbPassword))
                throw new DBConcurrencyException("Hedef veritabanina baglanilamadi. Lutfen bilgilerinizi kontorl edin. Veritabaninizin uzaktan erisilebilir olduguna emin olun.");
            var result = _userManager.UpdateAsync(user).Result;
            var connectionString = $"Server={user.Server}; Database={user.DbName}; Uid={user.DbUserName}; Pwd={user.DbPassword};";
            CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(1), IsEssential = true };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("VivosisConnectionString", connectionString, option);
            return result;
        }
        public IdentityResult DeleteUser(int userId)
        {
            var user = _accountDbContext.Users.Find(userId);
            return _userManager.DeleteAsync(null).Result;
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
