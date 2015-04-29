using System;
using System.Linq;
using System.Threading;
using Findall2.Models;
using Findall2.Searchers;
using Findall2Tests.Utilities;
using NUnit.Framework;
using log4net;
using log4net.Config;

namespace Findall2Tests.Integration
{
    [TestFixture]
    public class SearcherIntegrationTests
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SearcherIntegrationTests()
        {
            XmlConfigurator.Configure();
        }

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
            
            Log.InfoFormat("Search took {0}ms, ({1} results)", (DateTime.Now - start).TotalMilliseconds, searcher.Matches.Count);

            MatchesLogger.LogFiles(searcher.Matches);
        }

        [Test]
        [Ignore("Integration")]
        public void TestOneDaySearcher()
        {
            Searcher searcher = SearcherIntegrationFactory.GetOneDaySearcher();

            ManualResetEvent finished = new ManualResetEvent(false);

            searcher.SearchFinished += s => finished.Set();

            DateTime start = DateTime.Now;

            searcher.Begin();

            finished.WaitOne();

            Log.InfoFormat("Search took {0}ms", (DateTime.Now - start).TotalMilliseconds);

            MatchesLogger.LogResult(searcher.Matches);
        }

        [Test]
        [Ignore("Integration")]
        public void TestTenDaysSearcher()
        {
            Searcher searcher = SearcherIntegrationFactory.GetTenDaysSearcher();

            ManualResetEvent finished = new ManualResetEvent(false);

            searcher.SearchFinished += s => finished.Set();

            DateTime start = DateTime.Now;

            searcher.Begin();

            finished.WaitOne();

            Log.InfoFormat("Search took {0}ms", (DateTime.Now - start).TotalMilliseconds);

            MatchesLogger.LogResult(searcher.Matches);
        }
    }
}
