using System.Collections.Generic;
using Findall2.Models;

namespace Findall2.Matchers
{
    /// <summary>
    /// An interface used for matching lines of a file.
    /// </summary>
    public interface IFileMatcher
    {
        /// <summary>
        /// Obtains <see cref="LineMatch"/>es for each of the <paramref name="lines"/>.
        /// </summary>
        /// <param name="lines">A list of all the lines of the file.</param>
        /// <returns>
        /// A list of <see cref="LineMatch"/>es (one for each matched line).
        /// </returns>
        IEnumerable<LineMatch> MatchAll(IEnumerable<string> lines);
    }
}