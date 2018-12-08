using AutoMapper;
using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.ServerFactory;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Core;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Services;
using BattlEyeManager.Spa.Auth;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Hubs;
using BattlEyeManager.Spa.Model;
using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Player = BattlEyeManager.DataLayer.Models.Player;

namespace BattlEyeManager.Spa
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = false,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = false,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            //--------------------------------------------------------------------------

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });


            services.AddTransient<ILog, LogImpl>();

            services.AddSingleton<IIpService, IpService>();
            services.AddSingleton<IBattlEyeServerFactory, WatcherBEServerFactory>();
            services.AddSingleton<IBeServerAggregator, BeServerAggregator>();
            services.AddSingleton<ServerStateService, ServerStateService>();
            services.AddSingleton<DataRegistrator, DataRegistrator>();

            services.AddSingleton<BELogic, BELogic>();

            services.AddScoped<OnlinePlayerService, OnlinePlayerService>();
            services.AddScoped<OnlineBanService, OnlineBanService>();

            services.AddTransient<MessageHelper, MessageHelper>();
            services.AddTransient<ISettingsStore, SettingsStore>();


            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                }));

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IBeServerAggregator beServerAggregator,
            AppDbContext store,
            ServerStateService service,
            DataRegistrator dataRegistrator,
            BELogic beLogic
        )
        {
            store.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<FallbackHub>("/api/serverfallback");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            SetupMappings();
            dataRegistrator.Init().Wait();
            beLogic.Init();
            CheckAdminUser(userManager, roleManager).Wait();
            RunActiveServers(beServerAggregator, store, service).Wait();
        }

        private async Task CheckAdminUser(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            const string adminRole = RoleConstants.Administrator;

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

        private void SetupMappings()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Server, ServerModel>();
                config.CreateMap<ServerModel, Server>();

                config.CreateMap<KickReason, KickReasonModel>();
                config.CreateMap<KickReasonModel, KickReason>();

                config.CreateMap<BanReason, BanReasonModel>();
                config.CreateMap<BanReasonModel, BanReason>();

                config.CreateMap<Server, ServerInfo>();
                config.CreateMap<ServerModel, ServerInfo>();

                config.CreateMap<Server, OnlineServerModel>();
                config.CreateMap<Ban, OnlineBanViewModel>();


                config.CreateMap<Player, OnlinePlayerModel>();
            });
        }
    }
}
