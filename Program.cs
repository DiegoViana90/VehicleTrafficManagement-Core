using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace VehicleTrafficManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:7053", "http://0.0.0.0:7053");
                });
    }
}
