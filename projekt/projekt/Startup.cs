using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using projekt.Models;
using WebApplication9.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using projekt.Hubs;
//using projekt.Extensions;


namespace projekt
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
        
            Configuration = configuration;
        
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddRazorPages();
            services.AddMvc();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddSwaggerGen();
            services.AddSignalR();
        }

       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            //app.UseElapsedTimeMiddleware();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
          
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               c.RoutePrefix = string.Empty;
           });

            app.UseEndpoints(routes =>
            {
                
                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=List}/{id?}");

                routes.MapControllerRoute(
                    name: null,
                    pattern: "Product/category",
                    defaults: new
                    {
                        controller ="Product",
                        action = "List",
                    }
                    );

                routes.MapControllerRoute(
                    name: null,
                    pattern: "Admin/{action=Index}",
                    defaults: new
                    {
                        controller = "Admin",
                        action = "Index",
                    }
                    );

                routes.MapHub<ChatHub>("/chathub");
                routes.MapHub<CounterHub>("/counterhub");
                
               
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
