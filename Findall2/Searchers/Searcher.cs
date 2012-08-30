using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Findall2.Matchers;
using Findall2.Models;
using Findall2.Readers;
using Findall2.Scanners;

namespace Findall2.Searchers
{
    /// <summary>
    /// Used to indicate the search process has completed.
    /// </summary>
    /// <param name="sender">The <see cref="Searcher"/> that completed.</param>
    public delegate void SearchFinished(Searcher sender);

    /// <summary>
    /// Used to connect the pieces of the system together to provide file search capability.
    /// </summary>
    public class Searcher
    {
        private readonly IList<FileMatch> _matches;

        private readonly object _matchesMutex = new object();

        private readonly IDirectoryScanner _scanner;

        private readonly FileMatcher _matcher;

        private readonly LineReader _reader;

        /// <summary>
        /// Constructs a new instance of Searcher.
        /// </summary>
        /// <param name="scanner">
        /// The <see cref="IDirectoryScanner"/> to obtain a list of files from.
        /// </param>
        /// <param name="matcher">
        /// The <see cref="FileMatcher"/> to obtain <see cref="FileMatch"/>es from.
        /// </param>
        /// <param name="reader">
        /// The <see cref="LineReader"/> used to read lines from the file.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="scanner"/>, <paramref name="matcher"/>, or
        /// <paramref name="reader"/> are null.
        /// </exception>
        public Searcher(IDirectoryScanner scanner, FileMatcher matcher, LineReader reader)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException("scanner");
            }

            if (matcher == null)
            {
                throw new ArgumentNullException("matcher");
            }

            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            _matches = new List<FileMatch>();

            _scanner = scanner;

            _matcher = matcher;

            _reader = reader;
        }

        /// <summary>
        /// Called once the <see cref="Begin"/> method's asynchronous work is complete.
        /// </summary>
        public SearchFinished SearchFinished { get; set; }

        public void Begin()
        {
            ThreadPool.QueueUserWorkItem(o =>
                                             {
                                                 Search();

                                                 if (SearchFinished != null)
                                                 {
                                                     SearchFinished.Invoke(this);
                                                 }
                                             });
        }

        /// <summary>
        /// Obtains all matches in all files.
        /// </summary>
        private void Search()
        {
            foreach (string file in _scanner.GetFiles())
            {
                IEnumerable<string> lines = _reader.GetLines(file);

                IEnumerable<LineMatch> results = _matcher.MatchAll(lines, file);

                if (results != null)
                {
                    IList<LineMatch> resultList = results.ToList();

                    // TODO: option to accept only files not containing any matches
                    if (resultList.Any())
                    {
                        FileMatch fileResults = new FileMatch(file, resultList);

                        lock (_matchesMutex)
                        {
                            _matches.Add(fileResults);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Safe for reading while <see cref="Begin"/> is executing
        /// the search on a different thread.
        /// </summary>
        /// <remarks>
        /// Search locking occurs once per file (briefly).
        /// This function creates a shallow copy of the internal
        /// <see cref="_matches"/> structure.
        /// </remarks>
        public IList<FileMatch> Matches
        {
            get
            {
                FileMatch[] copy;

                lock (_matchesMutex)
                {
                    copy = new FileMatch[_matches.Count];

                    _matches.CopyTo(copy, 0);
                }

                return copy;
            }
        }
    }
}
