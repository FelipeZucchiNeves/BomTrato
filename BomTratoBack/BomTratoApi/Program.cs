using BomTratoApi.Configurations;
using BomTratoData.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BomTratoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()
                .MigrateDatabase<BomtratoContext>()
                .MigrateDatabase<ApplicationDbContext>()
                .MigrateDatabase<EventStoreContext>()
                .Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
