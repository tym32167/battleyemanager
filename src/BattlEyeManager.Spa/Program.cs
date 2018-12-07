using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BattlEyeManager.Spa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:58175", "http://192.168.0.10:58175")
                .UseStartup<Startup>()
                .Build();
    }
}
