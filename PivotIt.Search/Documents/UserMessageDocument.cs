using Nest;
using PivotIt.Search.Analyzers;
using PivotIt.Search.Records;

namespace PivotIt.Search.Documents
{
    public class UserMessageDocument
    {
        public UserMessageDocument(UserMessageRecord record)
        {
            ID = record.ID;
            Subject = record.Subject;
            MessageBody = record.MessageBody;
        }

        public int ID { get; private set; }

        [Text(Analyzer = AnalyzerNames.IndexAnalyzerName, SearchAnalyzer = AnalyzerNames.SearchAnalyzerName)]
        public string Subject { get; private set; }

        [Text]
        public string MessageBody { get; private set; }
    }
}
