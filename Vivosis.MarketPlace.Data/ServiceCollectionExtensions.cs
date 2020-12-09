using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Vivosis.MarketPlace.Data.Entities;

namespace Vivosis.MarketPlace.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SeedIdentity(this IServiceCollection services)
        {
            SeedRole(services.BuildServiceProvider().GetRequiredService<RoleManager<SystemRole>>());
            SeedUser(services.BuildServiceProvider().GetRequiredService<UserManager<SystemUser>>());
            return services;
        }
        public static IServiceCollection SeedStores(this IServiceCollection services)
        {
            var dbContext = services.BuildServiceProvider().GetRequiredService<AccountDbContext>();
            if(dbContext.Stores.Any())
                return services;
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
            return services;
        }
        public static IServiceCollection AddAccountDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AccountDbContext>(options => options.UseMySql(connectionString));
            services.BuildServiceProvider().GetRequiredService<AccountDbContext>().Database.Migrate();
            services.SeedStores().SeedIdentity();//Migration islemi sirasinda varolmayan db ye veri eklemeye calistigi icin hata veriyor
            //Update-Database demeden veritabanini kuruyor onu bir arastir bakam
            return services;
        }
        public static IServiceCollection AddMarketPlaceDbContext(this IServiceCollection services, string unformatedConnectionString)
        {
            services.AddDbContext<MarketPlaceDbContext>((serviceProvider, options) =>
            {
                var httpContext = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
                var httpRequest = httpContext?.Request;
                if(httpRequest == null)
                    return;
                if(string.IsNullOrEmpty(httpContext.User.Identity.Name))
                    return;
                var customerClaim = ((ClaimsIdentity)httpContext.User.Identity).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "Customer");
                if(customerClaim == null)
                    return;
                var connectionString = string.Format(unformatedConnectionString, $"db_{httpContext.User.Identity.Name}");
                options.UseMySql(connectionString);
            });
            services.AddDbContext<MarketPlaceDbContext>();
            return services;
        }
        private static void SeedUser(UserManager<SystemUser> userManager)
        {
            if(userManager.Users.Any())
                return;
            var defaultUser = new SystemUser
            {
                Id = 1,
                UserName = "admin",
                FullName = "Muhammed ÇELİK",
                PhoneNumber = "+90 534 818 31 26",
                Email = "celikmuhammed21@hotmail.com",
                ExpireTime = DateTime.MaxValue
            };
            userManager.CreateAsync(defaultUser, "123456").Wait();
            userManager.AddToRoleAsync(defaultUser, "Admin");
        }
        private static void SeedRole(RoleManager<SystemRole> roleManager)
        {
            if(roleManager.Roles.Any())
                return;
            var roles = new List<SystemRole>
            {
                new SystemRole
                {
                    Id = 1,
                    Name = "Admin"
                },
                new SystemRole
                {
                    Id = 2,
                    Name = "Customer"
                }
            };
            foreach(var role in roles)
                roleManager.CreateAsync(role).Wait();
        }
    }
}
