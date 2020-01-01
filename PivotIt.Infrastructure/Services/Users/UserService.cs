using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PivotIt.Infrastructure.Entities;
using PivotIt.Infrastructure.Persistence;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PivotIt.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly SiteContext _siteContext;

        public UserService(SiteContext siteContext, ILogger<UserService> logger)
        {
            _siteContext = siteContext;
            _logger = logger;
        }

        public async Task<SiteUser> GetSiteUserByUserNameAsync(string userName)
        {
            var user = await _siteContext.SiteUser.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName).ConfigureAwait(false);

            return user;
        }

        public async Task<bool> TryAddUserAsync(SiteUser siteUser)
        {
            try
            {
                await _siteContext.AddAsync(siteUser).ConfigureAwait(false);
                await _siteContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not add user: {JsonSerializer.Serialize(siteUser)}");
                return false;
            }
        }
    }
}
