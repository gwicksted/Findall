using Findall2.Searchers;

namespace Findall2Tests.Integration
{
    public static class SearcherIntegrationFactory
    {
        public static Searcher GetSearcher()
        {
            SearcherFactory factory = new SearcherFactory
            {
                FileNamePattern = "*.txt",
                Path = @"C:\test\",
                Recursive = true,
                Hidden = false,
                LinePattern = "[a-z]+",
                System = false,
                LinesNotMatching = false,
                FilesNotMatching = false
            };

            return factory.ConstructSearcher();
        }
    }
}
