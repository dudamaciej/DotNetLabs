using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MainProject.Middlewares;
using Microsoft.AspNetCore.Identity;


namespace MainProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddTransient<IProductRepository, EFProductRepository>();
           // services.AddDbContext<AppDbContext>(options =>
           // options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<TimeMiddleware>();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(routes => {

                routes.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Product}/{action=List}/{id?}");

                routes.MapControllerRoute(
                         name: "Product",
                        pattern: "Product/{category}",
                        defaults: new
                        {
                            controller = "Product",
                            action = "List"
                        });
                routes.MapControllerRoute(
                     name: "defaultAdmin",
                    pattern: "{controller=Admin}/{action=Index}");

                routes.MapControllerRoute(
                     name: "AdminEdit",
                    pattern: "{controller=Admin}/{action=Edit}/{id?}");

                routes.MapControllerRoute(
                     name: "AdminDelete",
                    pattern: "{controller=Admin}/{action=Delete}/{id?}");
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
