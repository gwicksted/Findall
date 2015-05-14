using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Findall2.Models;

namespace Findall2.Matchers
{
    /// <summary>
    /// Matches lines of text against a regular expression.
    /// </summary>
    public class LineMatcher : ILineMatcher
    {
        private readonly Regex _expression;

        /// <summary>
        /// Constructs a new instance of LineMatcher.
        /// </summary>
        /// <param name="expression">
        /// The regular expression to use for matching lines in the
        /// <see cref="Match"/> function.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="expression"/> was null.
        /// </exception>
        public LineMatcher(Regex expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            
            _expression = expression;
        }

        /// <summary>
        /// Uses the expression passed during construction to match all
        /// instances found within the <paramref name="line"/>.
        /// </summary>
        /// <param name="line">A line of text to match.</param>
        /// <returns>
        /// A list of <see cref="ColumnMatch"/>es that were found in
        /// the <paramref name="line"/>.
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

            return from Match lineMatch in _expression.Matches(line) select new ColumnMatch(lineMatch.Index, lineMatch.Length);
        }
    }
}
