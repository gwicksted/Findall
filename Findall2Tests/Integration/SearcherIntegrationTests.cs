using System;
using System.Diagnostics;
using System.Threading;
using Findall2.Searchers;
using Findall2Tests.Logging;
using NUnit.Framework;

namespace Findall2Tests.Integration
{
    [TestFixture]
    public class SearcherIntegrationTests
    {
        [Test]
        [Ignore("Integration")]
        public void TestSearcher()
        {
            Searcher searcher = SearcherIntegrationFactory.GetSearcher();

            ManualResetEvent finished = new ManualResetEvent(false);

            searcher.SearchFinished += s => finished.Set();

            DateTime start = DateTime.Now;

            searcher.Begin();

            finished.WaitOne();

            Debug.Print("Search took {0}ms", (DateTime.Now - start).TotalMilliseconds);

            MatchesLogger.LogResult(searcher.Matches);
        }
    }
}
