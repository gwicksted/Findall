﻿using System;
using System.Collections.Generic;
using System.Linq;
using Findall2.Models;

namespace Findall2.Matchers
{
    /// <summary>
    /// Matches all lines of text in a file using a <see cref="LineMatcher"/>.
    /// </summary>
    public class FileMatcher : FileMatcherBase
    {
        /// <summary>
        /// Constructs a new instance of FileMatcher.
        /// </summary>
        /// <param name="matcher">
        /// The <see cref="ILineMatcher"/> to be used when matching individual lines.
        /// </param>
        public FileMatcher(ILineMatcher matcher)
            : base(matcher)
        {
        }

        /// <summary>
        /// Uses the <see cref="LineMatcher"/> to obtain matches on each of the
        /// <paramref name="lines"/>.
        /// </summary>
        /// <param name="lines">A list of all the lines of the file.</param>
        /// <returns>
        /// A list of <see cref="LineMatch"/>es (one for each matched line).
        /// </returns>
        public override IEnumerable<LineMatch> MatchAll(IEnumerable<string> lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException("lines");
            }

            int lineNumber = 0;

            return from line in lines
                   let matches = Matcher.Match(line)
                   where matches != null && matches.Any()
                   select new LineMatch(line, lineNumber++, matches);
        }
    }
}
