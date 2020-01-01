using PivotIt.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PivotIt.Infrastructure.Services.UserMessages
{
    public interface IUserMessageService
    {
        Task<IEnumerable<UserMessage>> GetUserMessagesAsync();
    }
}
