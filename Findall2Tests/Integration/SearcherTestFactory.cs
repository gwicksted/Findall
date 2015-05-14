using System;
using Findall2.Searchers;
using Findall2Tests.Utilities;

namespace Findall2Tests.Integration
{
    public static class SearcherTestFactory
    {
        private const string XmlPattern = "^\\s*\\<[a-z]+?\\>[^<]+\\</[a-z]+?\\>\\s*$";

        public static Searcher GetSearcher()
        {
            SearcherFactory factory = new SearcherFactory
            {
                FileNamePattern = "*.xml",
                Path =  ExecutionDirectory.FindExecutionDirectory(),
                Recursive = true,
                Hidden = false,
                LinePattern = XmlPattern,
                System = false,
                LinesNotMatching = false,
                FilesNotMatching = false
            };

            return factory.ConstructSearcher();
        }

        public static Searcher GetOneDaySearcher()
        {
            SearcherFactory factory = new SearcherFactory
            {
                FileNamePattern = "*.xml",
                Path = ExecutionDirectory.FindExecutionDirectory(),
                Recursive = true,
                Hidden = false,
                LinePattern = XmlPattern,
                System = false,
                LinesNotMatching = false,
                FilesNotMatching = false,
                MinimumFileDate = DateTime.Now - new TimeSpan(1,0,0,0),
                MaximumFileDate = DateTime.Now
            };

            return factory.ConstructSearcher();
        }

        public static Searcher GetTenDaysSearcher()
        {
            SearcherFactory factory = new SearcherFactory
            {
                FileNamePattern = "*.xml",
                Path = ExecutionDirectory.FindExecutionDirectory(),
                Recursive = true,
                Hidden = false,
                LinePattern = XmlPattern,
                System = false,
                LinesNotMatching = false,
                FilesNotMatching = false,
                MinimumFileDate = DateTime.Now - new TimeSpan(10, 0, 0, 0),
                MaximumFileDate = DateTime.Now
            };

            return factory.ConstructSearcher();
        }
    }
}
