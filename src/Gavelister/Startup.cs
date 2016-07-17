using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Gavelister.Data;
using Gavelister.Models;
using Microsoft.AspNetCore.Identity;

namespace Gavelister
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);


            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(o => {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false; ;
                o.Password.RequiredLength = 6;
            })      
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            SeedUsers(app);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private async void SeedUsers(IApplicationBuilder app)
        {
            var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
            var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            // Add missing roles
            var adminRole = await roleManager.FindByNameAsync("Administrator");
            if (adminRole == null)
            {
                adminRole = new IdentityRole("Administrator");
                await roleManager.CreateAsync(adminRole);
            }

            // Create test users
            var admin = await userManager.FindByNameAsync("shervin");
            if (admin == null)
            {
                admin = new ApplicationUser()
                {
                    UserName = "shervin",
                };
                await userManager.CreateAsync(admin, "JctZ6WPap8");
                await userManager.AddToRoleAsync(admin, "Administrator");
            }

            // Add missing roles
            var userRole = await roleManager.FindByNameAsync("User");
            if (userRole == null)
            {
                userRole = new IdentityRole("User");
                await roleManager.CreateAsync(userRole);
            }
            var user1 = await userManager.FindByNameAsync("eporkg");
            if(user1 == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "eporkg",
                };
                await userManager.CreateAsync(newUser, "apehue");
                await userManager.AddToRoleAsync(newUser, "User");
            }

            var user2 = await userManager.FindByNameAsync("bruker");
            if (user2 == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "bruker",
                };
                await userManager.CreateAsync(newUser, "øyvind123");
                await userManager.AddToRoleAsync(newUser, "User");
            }

        }
    }
}
