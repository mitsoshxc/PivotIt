using Microsoft.Extensions.DependencyInjection;
using PivotIt.Core.Interfaces;
using PivotIt.Infrastructure.DomainEvents;
using PivotIt.Infrastructure.Services.UserMessages;
using PivotIt.Infrastructure.Services.Users;
using PivotIt.Search.IndexProviders;
using PivotIt.Search.Services.UserMessages;

namespace PivotIt.Web.Helpers
{
    public static class ServicesExtensions
    {
        public static void DeclareServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMessageService, UserMessageService>();
            services.AddScoped<IUserMessageSearchService, UserMessageSearchService>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            // index providers
            services.AddScoped<UserMessagesIndexProvider>();
        }
    }
}
