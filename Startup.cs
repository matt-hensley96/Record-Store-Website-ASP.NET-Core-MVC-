using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goldies.Data;
using Goldies.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Goldies
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GoldiesContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("GoldiesConnectionString"));
            });

            services.AddTransient<IMailService, NullMailService>();
            //TO DO: Add support for a real mail service, not just the null one.
            services.AddTransient<GoldiesSeeder>();

            services.AddScoped<IGoldiesRepository, GoldiesRepository>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseNodeModules();

            app.UseRouting();

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Default",
                    "{controller}/{action}/{id?}",
                    new {controller = "App", action = "Index" });

                cfg.MapRazorPages();
            });
        }
    }
}
