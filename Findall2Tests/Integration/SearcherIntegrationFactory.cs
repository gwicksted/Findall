﻿using System;
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

        public static Searcher GetOneDaySearcher()
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
                FileNamePattern = "*.txt",
                Path = @"C:\test\",
                Recursive = true,
                Hidden = false,
                LinePattern = "[a-z]+",
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
