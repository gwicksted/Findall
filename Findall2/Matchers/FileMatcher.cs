using System;
using System.Collections.Generic;
using System.Linq;
using Findall2.Models;

namespace Findall2.Matchers
{
    /// <summary>
    /// Matches all lines of text in a file using a <see cref="LineMatcher"/>.
    /// </summary>
    public class FileMatcher : IFileMatcher
    {
        private readonly ILineMatcher _matcher;

        /// <summary>
        /// Constructs a new instance of FileMatcher.
        /// </summary>
        /// <param name="matcher">
        /// The <see cref="ILineMatcher"/> to be used when matching individual lines.
        /// </param>
        public FileMatcher(ILineMatcher matcher)
        {
            if (matcher == null)
            {
                throw new ArgumentNullException("matcher");
            }

            _matcher = matcher;
        }

        /// <summary>
        /// Uses the <see cref="LineMatcher"/> to obtain matches on each of the
        /// <paramref name="lines"/>.
        /// </summary>
        /// <param name="lines">A list of all the lines of the file.</param>
        /// <param name="fileName">
        /// The full path the the file containing the <paramref name="lines"/>.
        /// </param>
        /// <returns>
        /// A list of <see cref="LineMatch"/>es (one for each matched line).
        /// </returns>
        public IEnumerable<LineMatch> MatchAll(IEnumerable<string> lines, string fileName)
        {
            if (lines == null)
            {
                throw new ArgumentNullException("lines");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            int lineNumber = 0;

            return from line in lines
                   let matches = _matcher.Match(line)
                   where matches != null && matches.Any()
                   select new LineMatch(line, lineNumber++, matches);
        }
    }
}
