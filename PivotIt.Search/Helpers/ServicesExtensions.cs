using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace PivotIt.Search.Helpers
{
    public static class ServicesExtensions
    {
        public static void DeclareElasticSearchClient(this IServiceCollection services, string connectionString)
        {
            var settings = new ConnectionSettings(new Uri(connectionString)).DefaultIndex("*");
            services.AddSingleton(settings);

            services.AddScoped(x =>
            {
                var connectionSettings = x.GetRequiredService<ConnectionSettings>();
                var client = new ElasticClient(connectionSettings);
                return client;
            });
        }
    }
}
