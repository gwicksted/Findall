using System;

namespace Findall2.Models
{
    /// <summary>
    /// A single match on a line of text.
    /// </summary>
    public class ColumnMatch
    {
        /// <summary>
        /// Constructs a new instance of ColumnMatch.
        /// </summary>
        /// <param name="column">
        /// The 0-based offset from the beginning of the text on
        /// the line in the file where this match begins.
        /// </param>
        /// <param name="length">
        /// The length of the match starting from the <paramref name="column"/>.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If column is less than 0 or length is less than 0.
        /// </exception>
        /// <remarks>
        /// A 0 length is allowed for Regular expression matches of 0 length.
        /// </remarks>
        public ColumnMatch(int column, int length)
        {
            if (column < 0)
            {
                throw new ArgumentOutOfRangeException("column", "The column must be greater than or equal to 0.");
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "The length of the match must be greater than or equal to 0.");
            }

            Column = column;

            Length = length;
        }

        /// <summary>
        /// The 0-based offset from the beginning of the text on
        /// the line in the file where this match begins.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// The length of the match starting from the <see cref="Column"/>.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Convenience method for obtaining the matched portion of the line.
        /// </summary>
        /// <param name="line">
        /// The full line of text where this match occurred.
        /// </param>
        /// <returns>
        /// The substring of the line containing only the matched portion.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="line"/> is null.
        /// </exception>
        public string GetMatchedSubstring(string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException("line");
            }

            return line.Substring(Column, Length);
        }
    }
}
