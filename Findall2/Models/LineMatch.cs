using System;
using System.Collections.Generic;

namespace Findall2.Models
{
    /// <summary>
    /// Represents all matches found on a single line of text.
    /// </summary>
    public class LineMatch
    {
        /// <summary>
        /// Constructs a new instance of LineMatch.
        /// </summary>
        /// <param name="line">The full line of text in the file.</param>
        /// <param name="lineNumber">The 0-offset line number in the file.</param>
        /// <param name="matches">
        /// All <see cref="ColumnMatch"/>es found on the line of text.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="line"/> was null (empty allowed for regular
        /// expressions matching blank lines).
        /// If <paramref name="matches"/> was null (empty allowed to prevent
        /// multiple enumerations of <paramref name="matches"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If the <paramref name="lineNumber"/> was less than 0.
        /// </exception>
        public LineMatch(string line, int lineNumber, IEnumerable<ColumnMatch> matches)
        {
            if (line == null)
            {
                throw new ArgumentNullException("line");
            }

            if (lineNumber < 0)
            {
                throw new ArgumentOutOfRangeException("lineNumber", "Must be greater than or equal to 0.");
            }

            if (matches == null)
            {
                throw new ArgumentNullException("matches");
            }

            Line = line;

            LineNumber = lineNumber;

            Matches = matches;
        }

        /// <summary>
        /// The full line of text in the file.
        /// </summary>
        public string Line { get; private set; }

        /// <summary>
        /// The 0-offset line number in the file.
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// All <see cref="ColumnMatch"/>es found on the line of text.
        /// </summary>
        public IEnumerable<ColumnMatch> Matches { get; private set; }
    }
}
