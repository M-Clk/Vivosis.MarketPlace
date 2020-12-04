using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vivosis.MarketPlace.API.Middleware;
using Vivosis.MarketPlace.Data;
using Vivosis.MarketPlace.Service;

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
            var unformattedDynamicConnectionString = Configuration.GetConnectionString("DynamicLocalDatabase");
            var accountConnectionString = Configuration.GetConnectionString("MarketPlaceDatabase");
            services.AddControllers();
            services.AddMvc();
            services.AddCommonVivosisServices(accountConnectionString, "Vivosis.MarketPlace.API", unformattedDynamicConnectionString);
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
