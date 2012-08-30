using System;
using System.Collections.Generic;

namespace Findall2.Models
{
    /// <summary>
    /// A matched file containing one or more <see cref="LineMatch"/>es.
    /// </summary>
    public class FileMatch
    {
        /// <summary>
        /// Constructs a new instance of FileMatch.
        /// </summary>
        /// <param name="path">The full path to the file on disk.</param>
        /// <param name="matches">
        /// A list of <see cref="LineMatch"/>es that were found within
        /// the file.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="path"/> is null or <see cref="string.Empty"/>
        /// or <paramref name="matches"/> is null.
        /// </exception>
        public FileMatch(string path, IList<LineMatch> matches)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (matches == null)
            {
                throw new ArgumentNullException("matches");
            }

            Path = path;

            Matches = matches;
        }

        /// <summary>
        /// The full path to the file on disk.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// A list of <see cref="LineMatch"/>es that were found within
        /// the file.
        /// </summary>
        public IList<LineMatch> Matches { get; private set; }
    }
}
