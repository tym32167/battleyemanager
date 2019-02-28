using AutoMapper;
using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.ServerFactory;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Core;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Services;
using BattlEyeManager.Spa.Auth;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Hubs;
using BattlEyeManager.Spa.Infrastructure;
using BattlEyeManager.Spa.Infrastructure.Featues;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Model;
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
using ChatMessage = BattlEyeManager.BE.Models.ChatMessage;
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

            // Console.WriteLine("-------------------------------------------------");
            // Console.WriteLine(Configuration.GetConnectionString("DefaultConnection"));
            // Console.WriteLine("--------------------------------------------------");

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

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });


            services.AddTransient<ILog, LogImpl>();

            services.AddSingleton<IIpService, IpService>();
            services.AddSingleton<IBattlEyeServerFactory, WatcherBEServerFactory>();
            services.AddSingleton<IBeServerAggregator, BeServerAggregator>();

            services.AddSingleton<ServerStateService, ServerStateService>();
            services.AddSingleton<OnlinePlayerStateService, OnlinePlayerStateService>();
            services.AddSingleton<OnlineChatStateService, OnlineChatStateService>();

            services.AddSingleton<DataRegistrator, DataRegistrator>();

            services.AddSingleton<BELogic, BELogic>();

            services.AddScoped<OnlinePlayerService, OnlinePlayerService>();
            services.AddScoped<OnlineBanService, OnlineBanService>();
            services.AddScoped<OnlineMissionService, OnlineMissionService>();
            services.AddScoped<OnlineServerService, OnlineServerService>();
            services.AddScoped<OnlineChatService, OnlineChatService>();

            // features

            services.AddSingleton<WelcomeFeature, WelcomeFeature>();

            // end features


            services.AddTransient<IGenericRepository<BanReason, int>, BanReasonRepository>();
            services.AddTransient<IGenericRepository<KickReason, int>, KickReasonRepository>();

            services.AddTransient<IGenericRepository<Server, int>, ServerRepository>();
            services.AddTransient<IServerRepository, ServerRepository>();


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

        private void InitSingletones(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.ApplicationServices.GetService<IIpService>();
            applicationBuilder.ApplicationServices.GetService<IBattlEyeServerFactory>();
            applicationBuilder.ApplicationServices.GetService<IBeServerAggregator>();
            applicationBuilder.ApplicationServices.GetService<ServerStateService>();
            applicationBuilder.ApplicationServices.GetService<OnlinePlayerStateService>();
            applicationBuilder.ApplicationServices.GetService<OnlineChatStateService>();
            applicationBuilder.ApplicationServices.GetService<DataRegistrator>();
            applicationBuilder.ApplicationServices.GetService<BELogic>();

            applicationBuilder.ApplicationServices.GetService<WelcomeFeature>();
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

            InitSingletones(app);
            InitFeatures(store, app);
        }

        private void InitFeatures(AppDbContext store, IApplicationBuilder applicationBuilder)
        {
            var servers = store.Servers.Where(x => x.WelcomeFeatureEnabled).ToArray();
            var welcomeFeature = applicationBuilder.ApplicationServices.GetService<WelcomeFeature>();
            foreach (var server in servers)
            {
                welcomeFeature.SetEnabled(server);
            }
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
                config.CreateMap<ServerModel, ServerInfoDto>();

                config.CreateMap<Server, OnlineServerModel>();

                config.CreateMap<Ban, OnlineBanViewModel>();

                config.CreateMap<Player, OnlinePlayerModel>();

                config.CreateMap<Mission, OnlineMissionModel>();

                config.CreateMap<ChatMessage, ChatMessageModel>()
                    .AfterMap((message, messageModel) =>
                        {
                            messageModel.Date = DateTime.SpecifyKind(messageModel.Date, DateTimeKind.Utc);
                        });
            });
        }
    }
}
