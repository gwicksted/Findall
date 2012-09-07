using System;
using System.Collections.Generic;
using System.Linq;
using Findall2.Models;

namespace Findall2.Matchers
{
    /// <summary>
    /// Returns a match if no lines of text in a file are matched using a <see cref="LineMatcher"/>.
    /// </summary>
    public class FileNotMatchingMatcher : FileMatcherBase
    {
        /// <summary>
        /// Constructs a new instance of FileMatcher.
        /// </summary>
        /// <param name="matcher">
        /// The <see cref="ILineMatcher"/> to be used when matching individual lines.
        /// </param>
        public FileNotMatchingMatcher(ILineMatcher matcher)
            : base(matcher)
        {
        }

        /// <summary>
        /// Uses the <see cref="LineMatcher"/> to obtain matches on each of the
        /// <paramref name="lines"/>.
        /// </summary>
        /// <param name="lines">A list of all the lines of the file.</param>
        /// <returns>
        /// A list of <see cref="LineMatch"/>es which is:
        /// Empty if this file had any matches
        /// Or contains a single result if this file had no matches
        /// </returns>
        public override IEnumerable<LineMatch> MatchAll(IEnumerable<string> lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException("lines");
            }

            bool anyMatches = (from line in lines
                               let matches = Matcher.Match(line)
                               where matches != null && matches.Any()
                               select new LineMatch(line, 0, matches)).Any();

            IList<LineMatch> results = new List<LineMatch>();

            if (!anyMatches)
            {
                results.Add(new LineMatch(string.Empty, 0, new ColumnMatch[0]));
            }

            return results;
        }
    }
}
