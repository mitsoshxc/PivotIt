using Nest;

namespace PivotIt.Search.Analyzers
{
    public static class SearchAnalyzer
    {
        public static IAnalysis AddSearchAnalyzer(this AnalysisDescriptor analysis)
        {
            const string lowercase = nameof(lowercase);

            // https://www.elastic.co/guide/en/elasticsearch/reference/current/analysis-edgengram-tokenizer.html
            // names aren't really important, they are just keys
            return
                analysis
                    .Analyzers(a => a
                        .Custom(AnalyzerNames.IndexAnalyzerName, c => c
                            .Tokenizer(AnalyzerNames.IndexAnalyzerName)
                            .Filters(lowercase)
                        )
                        .Custom(AnalyzerNames.SearchAnalyzerName, c =>
                            c.Tokenizer(lowercase)
                        )
                    )
                    .Tokenizers(t => t
                        .EdgeNGram(AnalyzerNames.IndexAnalyzerName, e => e
                            .MinGram(1)
                            .MaxGram(20)
                            .TokenChars(TokenChar.Letter)
                        )
                    );
        }
    }
}
