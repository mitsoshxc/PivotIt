using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using PivotIt.Infrastructure.Services.UserMessages;
using PivotIt.Web.Helpers;
using System.Threading.Tasks;

namespace PivotIt.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                await scope.RebuildAllAsync().ConfigureAwait(false);
            }

            await webHost.RunAsync().ConfigureAwait(false);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().UseNLog();
    }
}
