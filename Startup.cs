using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]);
            });
            services.AddTransient<IProductRepository, EFProductRepository>();
            //services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddRazorPages();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(options =>
            {
                options.MapControllerRoute(name: null,
                    pattern: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Products", action = "List" });

                options.MapControllerRoute(name: null,
                    pattern: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

                options.MapControllerRoute(name: null,
                pattern: "{category}",
                defaults: new { controller = "Product", action = "List", productPage = 1 });

                options.MapControllerRoute(name: null,
                pattern: "",
                defaults: new { controller = "Product", action = "List", productPage = 1 });

                options.MapControllerRoute(name: null, pattern: "{controller}/{action}/{id?}");
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
