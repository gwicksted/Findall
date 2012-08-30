using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Findall2.Models;
using Findall2.Searchers;
using Timer = System.Timers.Timer;

namespace Findall2.Utilities
{
    /// <summary>
    /// Indicates there is at least one new result.
    /// </summary>
    /// <param name="matches">
    /// The full list of <see cref="FileMatch"/>es found so far.
    /// </param>
    /// <remarks>
    /// The last NewResults call may not contain the full list
    /// of matches. Always use the <see cref="Finished"/> list
    /// of matches to get the final results.
    /// </remarks>
    public delegate void NewResults(IList<FileMatch> matches);

    /// <summary>
    /// Indicates searching has completed.
    /// </summary>
    /// <param name="matches">
    /// The full list of <see cref="FileMatch"/>es.
    /// </param>
    public delegate void Finished(IList<FileMatch> matches);

    /// <summary>
    /// Wraps the <see cref="Searcher"/> with a <see cref="Timer"/>
    /// that periodically polls for new results and calls back
    /// indicating when there are new matches.
    /// </summary>
    public class TimeDelayResultsReturner : IDisposable
    {
        private readonly Searcher _searcher;

        private readonly Timer _timer;

        private int _lastCount;

        private bool _finished;

        /// <summary>
        /// Constructs a new instance of TimeDelayResultsReturner.
        /// </summary>
        /// <param name="searcher">
        /// The <see cref="Searcher"/> to use for obtaining results.
        /// </param>
        /// <param name="delay">
        /// The delay (in milliseconds) to wait between checks to
        /// see if there are new results from the
        /// <see cref="Searcher"/>.
        /// </param>
        public TimeDelayResultsReturner(Searcher searcher, int delay)
        {
            _searcher = searcher;

            _searcher.SearchFinished += SearchFinished;

            _timer = new Timer(delay) { AutoReset = false };

            _timer.Elapsed += CheckNow;
        }

        /// <summary>
        /// Indicates there is at least one new result.
        /// </summary>
        /// <remarks>
        /// The last NewResults call may not contain the full list
        /// of matches. Always use the <see cref="Finished"/> list
        /// of matches to get the final results.
        /// </remarks>
        public NewResults NewResults { get; set; }

        /// <summary>
        /// Indicates searching has completed.
        /// </summary>
        public Finished Finished { get; set; }

        /// <summary>
        /// Asynchronously begins the process of searching.
        /// </summary>
        public void Begin()
        {
            ThreadPool.QueueUserWorkItem(o =>
                                             {
                                                 _finished = false;

                                                 _lastCount = 0;

                                                 _searcher.Begin();

                                                 _timer.Start();
                                             });
        }

        /// <summary>
        /// Indicates the period between polling for new matches has
        /// expired.
        /// </summary>
        /// <param name="sender">
        /// The <see cref="Timer"/> firing the event.
        /// </param>
        /// <param name="args">
        /// The <see cref="ElapsedEventArgs"/> provided by the event.
        /// </param>
        private void CheckNow(object sender, ElapsedEventArgs args)
        {
            if (!_finished)
            {
                if (NewResults != null)
                {
                    IList<FileMatch> matches = _searcher.Matches;

                    int count = matches.Count;

                    if (count > _lastCount)
                    {
                        _lastCount = count;

                        NewResults.Invoke(matches);
                    }
                }

                _timer.Start();
            }
        }

        /// <summary>
        /// Indicates the <see cref="Searcher"/> has reported a
        /// finished state.
        /// </summary>
        /// <param name="sender">
        /// The <see cref="Searcher"/> reporting the event.
        /// </param>
        private void SearchFinished(Searcher sender)
        {
            _finished = true;

            _timer.Stop();

            if (Finished != null)
            {
                Finished.Invoke(_searcher.Matches);
            }
        }

        /// <summary>
        /// Dispose of the resources used by this class.
        /// </summary>
        public void Dispose()
        {
            _searcher.SearchFinished -= SearchFinished;

            _timer.Dispose();
        }
    }
}
