using Microsoft.EntityFrameworkCore;
using PivotIt.Core.Entities;
using PivotIt.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PivotIt.Infrastructure.Services.UserMessages
{
    public class UserMessageService : IUserMessageService
    {
        private readonly SiteContext _siteContext;

        public UserMessageService(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }

        public async Task<IEnumerable<UserMessage>> GetUserMessagesAsync()
        {
            return await _siteContext.UserMessage.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }
    }
}
