using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Findall2.Models;

namespace Findall2.Matchers
{
    /// <summary>
    /// Matches lines that do not match a regular expression.
    /// </summary>
    public class LineNotMatchingMatcher : ILineMatcher
    {
        private readonly Regex _expression;

        /// <summary>
        /// Constructs a new instance of LineNotMatchingMatcher.
        /// </summary>
        /// <param name="expression">
        /// The regular expression to use for matching lines in the
        /// <see cref="Match"/> function.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="expression"/> was null.
        /// </exception>
        public LineNotMatchingMatcher(Regex expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            
            _expression = expression;
        }

        /// <summary>
        /// Uses the expression passed during construction to
        /// determine if the <paramref name="line"/> matches or not.
        /// </summary>
        /// <param name="line">A line of text to match.</param>
        /// <returns>
        /// A single <see cref="ColumnMatch"/> with 0 index and 0 length
        /// if the line did not match the regular expression.
        /// An empty list of <see cref="ColumnMatch"/>es if the regular
        /// expression matched at least once in the line.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="line"/> was null.
        /// </exception>
        public IEnumerable<ColumnMatch> Match(string line)
        {
            if (line == null)
            {
                throw new ArgumentNullException("line");
            }

            return !_expression.IsMatch(line) ? new[] { new ColumnMatch(0, 0) } : new ColumnMatch[0];
        }
    }
}
