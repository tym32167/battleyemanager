using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Net;
using BattlEyeManager.BE.ServerFactory;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Models;
using BattlEyeManager.MongoDB;
using BattlEyeManager.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BattlEyeManager.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IKeyValueStore<UserModel, Guid>, MongoDBStoreGuid<UserModel>>();
            services.AddSingleton<IKeyValueStore<RoleModel, Guid>, MongoDBStoreGuid<RoleModel>>();
            services.AddSingleton<IKeyValueStore<UserRole, Guid>, MongoDBStoreGuid<UserRole>>();
            services.AddSingleton<IKeyValueStore<ChatModel, Guid>, MongoDBStoreGuid<ChatModel>>();


            services.AddSingleton<IRoleStore<RoleModel>, RoleStore>();
            services.AddSingleton<IUserRoleStore<UserModel>, UserStore>();
            services.AddSingleton<IUserStore<UserModel>, UserStore>();
            services.AddSingleton<IUserPasswordStore<UserModel>, UserStore>();

            services.AddIdentity<UserModel, RoleModel>().AddDefaultTokenProviders();

            services.AddSingleton<IIpService, IpService>();

            services.AddSingleton<IBattlEyeServerFactory, WatcherBEServerFactory>();
            services.AddSingleton<IBeServerAggregator, BeServerAggregator>();

            services.AddSingleton<ServerStateService, ServerStateService>();


            services.AddSingleton<IKeyValueStore<ServerModel, Guid>, MongoDBStoreGuid<ServerModel>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<UserModel> userManager,
            RoleManager<RoleModel> roleManager,
            IBeServerAggregator beServerAggregator, IKeyValueStore<ServerModel, Guid> store,
            ServerStateService service
            )
        {
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
            UserManager<UserModel> userManager,
            RoleManager<RoleModel> roleManager)
        {
            const string adminRole = "Administrator";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                var role = new RoleModel { Name = adminRole };
                await roleManager.CreateAsync(role);
            }

            var admin = await userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                var adminModel = new UserModel { UserName = "admin", Password = "12qw!@QW", LastName = "admin", FirstName = "admin", Email = "admin@admin.sample" };
                await userManager.CreateAsync(adminModel, adminModel.Password);
                await userManager.AddToRoleAsync(adminModel, adminRole);
            }
        }


        private async Task RunActiveServers(IBeServerAggregator beServerAggregator, IKeyValueStore<ServerModel, Guid> store, ServerStateService service)
        {
            await service.InitAsync();

            var activeServers = await store.FindAsync(s => s.Active);
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
