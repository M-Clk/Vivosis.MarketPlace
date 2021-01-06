using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        public IEnumerable<SystemUser> GetAllUsers()
        {
            return _accountDbContext.Users;
        }
        public IEnumerable<SystemUser> GetCustomerUsers()
        {
            return _userManager.GetUsersInRoleAsync("Customer").Result;
        }
        public IEnumerable<SystemUser> GetAdminUsers()
        {
            return _userManager.GetUsersInRoleAsync("Admin").Result;
        }
        public bool Login(string userName, string password, bool rememberMe)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            if(user == null)
                return false;
            _signInManager.SignOutAsync().Wait();
            var result = _signInManager.PasswordSignInAsync(user, password, rememberMe, true).Result;
            if(result.Succeeded && _userManager.IsInRoleAsync(user, "Customer").Result)
            {
                var connectionString = $"Server={user.Server}; Database={user.DbName}; Uid={user.DbUserName}; Pwd={user.DbPassword};";
                CookieOptions option = new CookieOptions { Expires = DateTime.Now.AddDays(1), IsEssential = true };
                _httpContextAccessor.HttpContext.Response.Cookies.Append("VivosisConnectionString", connectionString, option);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("IsCustomer", "y", option);
            }
            else
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("IsCustomer");
            return result.Succeeded;
        }
        public void Logout() => _signInManager.SignOutAsync().Wait();
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
                dbContext.Database.Migrate();
                user.Settings = new UserSettings();
            }
            var result = _userManager.CreateAsync(user, pass).Result;
            if(result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, role).Wait();
                SeedStores(dbContext);
            }
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
            if(user.UserName == _httpContextAccessor.HttpContext.User.Identity.Name)
                return null;
            var isCustomer = _userManager.IsInRoleAsync(user, "Customer").Result;
            var result = _userManager.DeleteAsync(user).Result;
            if(result.Succeeded && isCustomer)
            {
                var options = new DbContextOptionsBuilder<MarketPlaceDbContext>();
                var connectionString = string.Format(_configuration.GetConnectionString("DynamicLocalDatabase"), $"db_{user.UserName.ToLower()}");
                options.UseMySql(connectionString);
                var dbContext = new MarketPlaceDbContext(options.Options);
                dbContext.Database.EnsureDeleted();
            }
            return result;
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
        private void SeedStores(MarketPlaceDbContext dbContext)
        {
            var stores = new List<Store>
            {
                new Store
                {
                    store_id = 1,
                    name = "N11",
                    image = "n11.png",
                    ssl = "n11_default_ssl",
                    url = "https://www.n11.com"
                },
                new Store
                {
                    store_id = 2,
                    name = "Trendyol",
                    image = "trendyol.png",
                    ssl = "trendyol_default_ssl",
                    url = "https://www.trendyol.com"
                },
                new Store
                {
                    store_id = 3,
                    name = "Hepsiburada",
                    image = "hepsiburada.png",
                    ssl = "hepsiburada_default_ssl",
                    url = "https://www.hepsiburada.com"
                },
                new Store
                {
                    store_id = 4,
                    name = "Gittigidiyor",
                    image = "gittigidiyor.png",
                    ssl = "gittigidiyor_default_ssl",
                    url = "https://www.gittigidiyor.com"
                },
                new Store
                {
                    store_id = 5,
                    name = "Cicek Sepeti",
                    image = "ciceksepeti.png",
                    ssl = "ciceksepeti_default_ssl",
                    url = "https://www.ciceksepeti.com"
                },
            };
            dbContext.Stores.AddRange(stores);
            dbContext.SaveChanges();
        }
    }
}
