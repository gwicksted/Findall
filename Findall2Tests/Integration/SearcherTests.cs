using System;
using System.Reflection;
using System.Threading;
using Findall2.Searchers;
using Findall2Tests.Utilities;
using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Findall2Tests.Integration
{
    [TestFixture]
    public class SearcherTests
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SearcherTests()
        {
            XmlConfigurator.Configure();
        }

        [Test]
        public void TestSearcher()
        {
            Searcher searcher = SearcherTestFactory.GetSearcher();

            ManualResetEvent finished = new ManualResetEvent(false);

            searcher.SearchFinished += s => finished.Set();

            DateTime start = DateTime.Now;

            searcher.Begin();

            finished.WaitOne();
            
            Log.InfoFormat("Search took {0}ms, ({1} results)", (DateTime.Now - start).TotalMilliseconds, searcher.Matches.Count);

            MatchesLogger.LogFiles(searcher.Matches);
        }

        [Test]
        public void TestOneDaySearcher()
        {
            Searcher searcher = SearcherTestFactory.GetOneDaySearcher();

            ManualResetEvent finished = new ManualResetEvent(false);

            searcher.SearchFinished += s => finished.Set();

            DateTime start = DateTime.Now;

            searcher.Begin();

            finished.WaitOne();

            Log.InfoFormat("Search took {0}ms", (DateTime.Now - start).TotalMilliseconds);

            MatchesLogger.LogResult(searcher.Matches);
        }

        [Test]
        public void TestTenDaysSearcher()
        {
            Searcher searcher = SearcherTestFactory.GetTenDaysSearcher();

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
