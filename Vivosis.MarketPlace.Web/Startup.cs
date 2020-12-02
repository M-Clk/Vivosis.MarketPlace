using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.AbstractRepositories;
using Vivosis.MarketPlace.Data.ConcreteRepositories;
using Vivosis.MarketPlace.Data.Entities;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Service.Concrete;

namespace Vivosis.MarketPlace.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IN11Service, N11Service>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IGlobalService, GlobalService>();
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

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
                .AddEntityFrameworkStores<MarketPlaceDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<MarketPlaceDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MarketPlaceDatabase"), b => b.MigrationsAssembly("Vivosis.MarketPlace.Web")));
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddRazorRuntimeCompilation();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
