using BattlEyeManager.Models;
using BattlEyeManager.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

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


            services.AddSingleton<IRoleStore<RoleModel>, RoleStore>();

            services.AddSingleton<IUserRoleStore<UserModel>, UserStore>();
            services.AddSingleton<IUserStore<UserModel>, UserStore>();
            services.AddSingleton<IUserPasswordStore<UserModel>, UserStore>();

            services.AddIdentity<UserModel, RoleModel>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
