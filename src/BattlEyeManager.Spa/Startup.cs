using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.ServerFactory;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Core;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.DataLayer.Repositories.Players;
using BattlEyeManager.Services;
using BattlEyeManager.Spa.Auth;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Core.Mapping;
using BattlEyeManager.Spa.Hubs;
using BattlEyeManager.Spa.Infrastructure;
using BattlEyeManager.Spa.Infrastructure.Featues;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Steam;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace BattlEyeManager.Spa
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Console.WriteLine("-------------------------------------------------");
            // Console.WriteLine(Configuration.GetConnectionString("DefaultConnection"));
            // Console.WriteLine("--------------------------------------------------");

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.Configure<ChatBotFeatureConfig>(chatBotFeatureConfig =>
            {
                Configuration.GetSection("ChatBotFeatureConfig").Bind(chatBotFeatureConfig);
            });

            services.AddDbContext<AppDbContext>(opt => opt
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });



            services.AddTransient<ILog, LogImpl>();


            services.AddSingleton<IIpService, IpService>(sp =>
            {
                var he = sp.GetService<IWebHostEnvironment>();
                var logger = sp.GetService<ILog>();
                // ReSharper disable once PossibleNullReferenceException
                var path = Path.Combine(he.ContentRootPath, "Data", "GeoLite2-Country.mmdb");
                var ipService = new IpService(path, logger);
                return ipService;
            });

            services.AddSingleton<IBattlEyeServerFactory, WatcherBEServerFactory>();
            services.AddSingleton<IBeServerAggregator, BeServerAggregator>();

            services.AddSingleton<ServerStateService, ServerStateService>();
            services.AddSingleton<ServerModeratorService, ServerModeratorService>();

            services.AddSingleton<OnlinePlayerStateService, OnlinePlayerStateService>();
            services.AddSingleton<OnlineChatStateService, OnlineChatStateService>();

            services.AddSingleton(new GetServerInfoSettings());
            services.AddSingleton<ISteamService, SteamService>();

            services.AddSingleton<ServerStatsService, ServerStatsService>();

            services.AddSingleton<DataRegistrator, DataRegistrator>();

            services.AddSingleton<BELogic, BELogic>();

            services.AddScoped<OnlinePlayerService, OnlinePlayerService>();
            services.AddScoped<OnlineBanService, OnlineBanService>();
            services.AddScoped<OnlineMissionService, OnlineMissionService>();
            services.AddScoped<OnlineServerService, OnlineServerService>();

            services.AddSingleton<OnlineChatService, OnlineChatService>();

            services.AddScoped<PlayerSyncService, PlayerSyncService>();

            // features

            services.AddSingleton<WelcomeFeature, WelcomeFeature>();
            services.AddSingleton<ChatBotFeature, ChatBotFeature>();

            // end features


            services.AddTransient<IGenericRepository<BanReason, int>, BanReasonRepository>();
            services.AddTransient<IGenericRepository<KickReason, int>, KickReasonRepository>();
            services.AddTransient<IGenericRepository<Server, int>, ServerRepository>();
            services.AddTransient<ServerScriptRepository, ServerScriptRepository>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<ServerModeratorRepository, ServerModeratorRepository>();
            services.AddTransient<ServerStatsRepository, ServerStatsRepository>();
            services.AddTransient<PlayerRepository, PlayerRepository>();


            services.AddSingleton<PlayersCache, PlayersCache>();

            services.AddInternalMapper();

            services.AddTransient<UserRepository, UserRepository>();

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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
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

            applicationBuilder.ApplicationServices.GetService<OnlineChatService>();

            applicationBuilder.ApplicationServices.GetService<WelcomeFeature>();
            applicationBuilder.ApplicationServices.GetService<ChatBotFeature>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IBeServerAggregator beServerAggregator,
            AppDbContext store,
            ServerStateService service,
            DataRegistrator dataRegistrator,
            BELogic beLogic,
            ServerModeratorService moderatorService,
            ServerStatsService serverStatsService,
            PlayersCache playersCache
        )
        {
            store.Database.Migrate();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Battleye Manager API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllers();
                endPoints.MapHub<FallbackHub>("/api/serverfallback");
                endPoints.MapFallbackToController("Index", "Home");
            });



            moderatorService.Init().Wait();
            dataRegistrator.Init().Wait();
            playersCache.Reload().Wait();
            beLogic.Init();

            CheckAdminUser(userManager, roleManager).Wait();
            RunActiveServers(beServerAggregator, store, service).Wait();

            InitSingletones(app);
            InitFeatures(store, app);

            serverStatsService.Start();
        }

        private void InitFeatures(AppDbContext store, IApplicationBuilder applicationBuilder)
        {
            var servers = store.Servers.Where(x => x.WelcomeFeatureEnabled).ToArray();
            var welcomeFeature = applicationBuilder.ApplicationServices.GetService<WelcomeFeature>();
            foreach (var server in servers)
            {
                welcomeFeature?.SetEnabled(server);
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
                var adminModel = new ApplicationUser { UserName = "admin", DisplayName = "admin", LastName = "admin", FirstName = "admin", Email = "admin@admin.sample" };
                await userManager.CreateAsync(adminModel, "12qw!@QW");
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
