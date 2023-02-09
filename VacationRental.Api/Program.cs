using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("VacationRental.Api.Tests")]

namespace VacationRental.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
