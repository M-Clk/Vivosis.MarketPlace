using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private static void SeedUser(UserManager<SystemUser> userManager)
        {
            if(userManager.Users.Any())
                return;
            var defaultUser = new SystemUser
            {
                Id = 1,
                UserName = "admin",
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
