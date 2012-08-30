using System;
using System.Diagnostics;
using System.Threading;
using Findall2.Searchers;
using Findall2.Utilities;
using Findall2Tests.Logging;
using NUnit.Framework;

namespace Findall2Tests.Integration
{
    [TestFixture]
    public class TimeDelayResultsReturnerIntegrationTests
    {
        [Test]
        [Ignore("Integration")]
        public void TestTimeDelayResultsReturner()
        {
            Searcher searcher = SearcherIntegrationFactory.GetSearcher();

            ManualResetEvent finished = new ManualResetEvent(false);

            TimeDelayResultsReturner returner = new TimeDelayResultsReturner(searcher, 1);

            DateTime start = DateTime.Now;

            returner.Finished += s => finished.Set();

            returner.NewResults += r => Debug.Print("Found {0} results at {1}ms", r.Count, (DateTime.Now - start).TotalMilliseconds);

            returner.Begin();

            finished.WaitOne();

            Debug.Print("Search took {0}ms", (DateTime.Now - start).TotalMilliseconds);

            MatchesLogger.LogResult(searcher.Matches);
        }

    }
}
