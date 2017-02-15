using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BattlEyeManager.Web
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            //loggerFactory.AddConsole();
            loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configure a rewrite rule to auto-lookup for standard default files such as index.html.    
            app.UseDefaultFiles();

            // Serve static files (html, css, js, images & more). See also the following URL: 
            // https://docs.asp.net/en/latest/fundamentals/static-files.html for further reference. 

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    // Disable caching for all static files.        
                    context.Context.Response.Headers["Cache-Control"] = Configuration["StaticFiles:Headers:Cache-Control"];
                    context.Context.Response.Headers["Pragma"] = Configuration["StaticFiles:Headers:Pragma"];
                    context.Context.Response.Headers["Expires"] = Configuration["StaticFiles:Headers:Expires"];
                }
            });

            // Add MVC to the pipeline     
            app.UseMvc();
        }
    }
}
