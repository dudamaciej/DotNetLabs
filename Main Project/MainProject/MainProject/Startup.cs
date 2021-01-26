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
using SignalRChat.Hubs;


namespace MainProject
{
    public class Startup
    {
      

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSignalR();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
                                          .AddEntityFrameworkStores<AppDbContext>();
            services.AddControllers();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop V1");
                c.RoutePrefix = "API";
            });
            app.UseMiddleware<TimeMiddleware>();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(routes =>
            {
                routes.MapHub<ChatHub>("/chathub");
                

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
                     name: "Admin",
                     pattern: "Admin/{action=Index}",
                     defaults: new
                     {
                         controller = "Admin",
                         action = "Index",
                     });
            });
                SeedData.EnsurePopulated(app);
            }
    }
}
