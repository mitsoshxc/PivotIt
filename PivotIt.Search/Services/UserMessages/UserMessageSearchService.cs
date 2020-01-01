using Nest;
using PivotIt.Search.Documents;
using System.Threading.Tasks;

namespace PivotIt.Search.Services.UserMessages
{
    public class UserMessageSearchService : IUserMessageSearchService
    {
        private readonly ElasticClient _client;

        public UserMessageSearchService(ElasticClient client)
        {
            _client = client;
        }

        public async Task<ISearchResponse<UserMessageDocument>> BySubject(string term)
        {
            var response = await _client.SearchAsync<UserMessageDocument>(s =>
                s.Index(IndexProviders.UserMessagesIndexProvider.IndexName)
                .Size(10)
                .Query(q =>
                    q.Match(m =>
                        m.Field(f =>
                            f.Subject)
                        .Query(term)
                    )
                )
            ).ConfigureAwait(false);

            return response;
        }
    }
}
