using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vivosis.MarketPlace.API.Middleware;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Data.AbstractRepositories;
using Vivosis.MarketPlace.Data.ConcreteRepositories;
using Vivosis.MarketPlace.Service.Abstract;
using Vivosis.MarketPlace.Service.Concrete;

namespace Vivosis.MarketPlace.API
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
            services.AddControllers();
            services.AddMvc();
            services.AddScoped<IProductRepositoryAdo, ProductRepositoryAdo>();
            services.AddScoped<IProductRepositoryEf, ProductRepositoryEf>();
            services.AddScoped<IN11Service, N11Service>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IGlobalService, GlobalService>();
            services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<MarketPlaceDbContext>();
            services.AddDbContext<MarketPlaceDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MarketPlaceDatabase"), b=>b.MigrationsAssembly("Vivosis.MarketPlace.API")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseGlobalExceptionHandling();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
