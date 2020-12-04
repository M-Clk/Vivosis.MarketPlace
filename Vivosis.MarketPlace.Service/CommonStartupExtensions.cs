extern alias MySqlConnectorAlias;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Service.Concrete;

namespace Vivosis.MarketPlace.Service
{
    public static class CommonStartupExtensions
    {
        public static IServiceCollection AddCommonVivosisServices(this IServiceCollection services, string accountConnectionString, string unformattedDynamicConnectionString)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IN11Service, N11Service>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IGlobalService, GlobalService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddIdentity<SystemUser, SystemRole>(setupAction =>
            {
                setupAction.Lockout.DefaultLockoutTimeSpan = new TimeSpan(3, 1, 0);
                setupAction.Lockout.MaxFailedAccessAttempts = 5;
                setupAction.User.RequireUniqueEmail = false;
                setupAction.Password.RequireDigit = false;
                setupAction.Password.RequiredLength = 1;
                setupAction.Password.RequireLowercase = false;
                setupAction.Password.RequireNonAlphanumeric = false;
                setupAction.Password.RequireUppercase = false;
                setupAction.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
            })
                .AddEntityFrameworkStores<AccountDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<AccountDbContext>(options => options.UseMySql(accountConnectionString, b => b.MigrationsAssembly("Vivosis.MarketPlace.Service")));
            services.BuildServiceProvider().GetRequiredService<AccountDbContext>().Database.Migrate();
            //Migration tek yerden olmali. Bunu ayarla. Ortak kullanilan bir projede olabilir. /Keyword: how to change migration folder location
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
                var connectionString = string.Format(unformattedDynamicConnectionString, $"db_{httpContext.User.Identity.Name}");
                options.UseMySql(connectionString);
            });
            services.SeedStores().SeedIdentity();
            return services;
        }
        public static IApplicationBuilder UseCommonMiddlewares(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
