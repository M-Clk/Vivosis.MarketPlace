using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            services.AddScoped<ILocalService, LocalService>();
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
            services.AddAccountDbContext(accountConnectionString);
            services.AddMarketPlaceDbContext(unformattedDynamicConnectionString);
            
            return services;
        }
        public static IApplicationBuilder UseCommonMiddlewares(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
