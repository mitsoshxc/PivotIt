using Microsoft.Extensions.DependencyInjection;
using PivotIt.Infrastructure.Services.UserMessages;
using PivotIt.Search.IndexProviders;
using PivotIt.Search.Records;
using System.Linq;
using System.Threading.Tasks;

namespace PivotIt.Web.Helpers
{
    public static class SearchIndexesExtensions
    {
        public static async Task RebuildAllAsync(this IServiceScope serviceScope)
        {
            var userMessageService = serviceScope.ServiceProvider.GetRequiredService<IUserMessageService>();

            var messages = await userMessageService.GetUserMessagesAsync().ConfigureAwait(false);
            var messageRecords = messages.Select(x => new UserMessageRecord
            {
                ID = x.ID,
                Subject = x.Subject,
                MessageBody = x.MessageBody
            });

            var userMessagesIndexProvider = serviceScope.ServiceProvider.GetRequiredService<UserMessagesIndexProvider>();
            await userMessagesIndexProvider.RunAsync(messageRecords).ConfigureAwait(false);
        }
    }
}
