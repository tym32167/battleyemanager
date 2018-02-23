using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Net;
using BattlEyeManager.BE.ServerFactory;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(@"server=localhost; database=battleyemanager; port=3306; user=root;"));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            //--------------------------------------------------------------------------

            services.AddMvc();

            services.AddSingleton<IIpService, IpService>();
            services.AddSingleton<IBattlEyeServerFactory, WatcherBEServerFactory>();
            services.AddSingleton<IBeServerAggregator, BeServerAggregator>();

            services.AddSingleton<ServerStateService, ServerStateService>();


            //services.AddSingleton<IKeyValueStore<ServerModel, Guid>, MongoDBStoreGuid<ServerModel>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IBeServerAggregator beServerAggregator,
            AppDbContext store,
            ServerStateService service
            )
        {
            store.Database.Migrate();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();



            CheckAdminUser(userManager, roleManager).Wait();
            RunActiveServers(beServerAggregator, store, service).Wait();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private async Task CheckAdminUser(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            const string adminRole = "Administrator";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                var role = new ApplicationRole { Name = adminRole };
                await roleManager.CreateAsync(role);
            }

            var admin = await userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                var adminModel = new ApplicationUser { UserName = "admin", Password = "12qw!@QW", LastName = "admin", FirstName = "admin", Email = "admin@admin.sample" };
                await userManager.CreateAsync(adminModel, adminModel.Password);
                await userManager.AddToRoleAsync(adminModel, adminRole);
            }
        }


        private async Task RunActiveServers(IBeServerAggregator beServerAggregator, AppDbContext store, ServerStateService service)
        {
            await service.InitAsync();

            var activeServers = await store.Servers.Where(s => s.Active).ToListAsync();
            foreach (var server in activeServers)
            {
                beServerAggregator.AddServer(new ServerInfo
                {
                    Id = server.Id,
                    Password = server.Password,
                    Port = server.Port,
                    Host = server.Host,
                    Name = server.Name
                });
            }
        }
    }
}
