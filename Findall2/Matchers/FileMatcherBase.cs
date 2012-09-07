using System;
using System.Collections.Generic;
using Findall2.Models;

namespace Findall2.Matchers
{
    /// <summary>
    /// Used for all implementers of <see cref="IFileMatcher"/>
    /// to hold the reference to the <see cref="ILineMatcher"/>
    /// and perform null checks during construction.
    /// </summary>
    public abstract class FileMatcherBase : IFileMatcher
    {
        /// <summary>
        /// The <see cref="ILineMatcher"/> used to match lines of the file
        /// later during the <see cref="MatchAll"/> function.
        /// </summary>
        protected readonly ILineMatcher Matcher;

        /// <summary>
        /// Constructs a new instance of FileMatcherBase.
        /// </summary>
        /// <param name="matcher">
        /// The <see cref="ILineMatcher"/> to be used by implementers
        /// of this abstract class.
        /// </param>
        protected FileMatcherBase(ILineMatcher matcher)
        {
            if (matcher == null)
            {
                throw new ArgumentNullException("matcher");
            }

            Matcher = matcher;
        }

        /// <summary>
        /// Obtains <see cref="LineMatch"/>es for each of the <paramref name="lines"/>.
        /// </summary>
        /// <param name="lines">A list of all the lines of the file.</param>
        /// <returns>
        /// A list of <see cref="LineMatch"/>es (one for each matched line).
        /// </returns>
        public abstract IEnumerable<LineMatch> MatchAll(IEnumerable<string> lines);
    }
}
