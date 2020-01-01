using Nest;
using PivotIt.Search.Analyzers;
using PivotIt.Search.Documents;
using PivotIt.Search.Records;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PivotIt.Search.IndexProviders
{
    public class UserMessagesIndexProvider
    {
        public const string IndexName = "usermessages";

        private readonly ElasticClient _client;

        public UserMessagesIndexProvider(ElasticClient client)
        {
            _client = client;
        }

        public async Task RunAsync(IEnumerable<UserMessageRecord> records)
        {
            var index = await _client.Indices.ExistsAsync(IndexName).ConfigureAwait(false);

            if (index.Exists)
            {
                await _client.Indices.DeleteAsync(IndexName).ConfigureAwait(false);
            }

            // create index
            var createResult =
                await _client.Indices.CreateAsync(IndexName, c =>
                    c.Settings(s =>
                        s.Analysis(a =>
                            // custom search analyzer
                            a.AddSearchAnalyzer()
                        )
                    ).Map<UserMessageDocument>(m => m.AutoMap())
                ).ConfigureAwait(false);

            // push data to index
            var bulkResult =
                await _client.BulkAsync(b =>
                    b.Index(IndexName).CreateMany(records)
                ).ConfigureAwait(false);
        }
    }
}
