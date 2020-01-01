using PivotIt.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PivotIt.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<SiteUser> GetSiteUserByUserNameAsync(string userName);

        Task<bool> TryAddUserAsync(SiteUser siteUser);
    }
}
