using Nest;
using PivotIt.Search.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PivotIt.Search.Services.UserMessages
{
    public interface IUserMessageSearchService
    {
        Task<ISearchResponse<UserMessageDocument>> BySubject(string term);
    }
}
