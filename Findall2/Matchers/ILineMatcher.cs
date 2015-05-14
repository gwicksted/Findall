using System.Collections.Generic;
using Findall2.Models;

namespace Findall2.Matchers
{
    /// <summary>
    /// Matches lines of text.
    /// </summary>
    public interface ILineMatcher
    {
        /// <summary>
        /// Find matches within the <paramref name="line"/>.
        /// </summary>
        /// <param name="line">A line of text to match.</param>
        /// <returns>
        /// A list of <see cref="ColumnMatch"/>es that were found in
        /// the <paramref name="line"/>.
        /// </returns>
        IEnumerable<ColumnMatch> Match(string line);
    }
}
