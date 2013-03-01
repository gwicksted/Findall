using System;
using System.Text.RegularExpressions;
using Findall2.Matchers;
using Findall2.Readers;
using Findall2.Scanners;

namespace Findall2.Searchers
{
    /// <summary>
    /// Convenience class for easily constructing <see cref="Searcher"/> instances.
    /// </summary>
    /// <remarks>Each property has reasonable defaults for ease of use.</remarks>
    public class SearcherFactory
    {
        private string _path = @"c:\";

        private string _fileNamePattern = "*.*";

        private bool _recursive = true;

        private string _linePattern = "\\d+";

        public SearcherFactory()
        {
            MaximumFileDate = null;
            MinimumFileDate = null;
            FilesNotMatching = false;
            LinesNotMatching = false;
            System = false;
            Hidden = false;
        }

        /// <summary>
        /// The base path to begin searching (default is c:\).
        /// </summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// The file name pattern (default is *.*).
        /// </summary>
        public string FileNamePattern
        {
            get { return _fileNamePattern; }
            set { _fileNamePattern = value; }
        }

        /// <summary>
        /// Indicates subdirectories should be scanned (default is true).
        /// </summary>
        public bool Recursive
        {
            get { return _recursive; }
            set { _recursive = value; }
        }

        /// <summary>
        /// Indicates hidden files should be searched (default is false).
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Indicates system files should be searched (default is false).
        /// </summary>
        public bool System { get; set; }

        /// <summary>
        /// The regular expression to use for matching lines (default is \d+).
        /// </summary>
        public string LinePattern
        {
            get { return _linePattern; }
            set { _linePattern = value; }
        }

        /// <summary>
        /// Indicates a line match when the line does not match the
        /// <see cref="LinePattern"/> (default is false).
        /// </summary>
        public bool LinesNotMatching { get; set; }

        /// <summary>
        /// Indicates only files that do not have any matches will be returned
        /// (default is false).
        /// </summary>
        public bool FilesNotMatching { get; set; }

        /// <summary>
        /// File created or last write date must be greater than or equal to this date.
        /// </summary>
        /// <remarks>Set to null to indicate no minimum date requirement.</remarks>
        public DateTime? MinimumFileDate { get; set; }

        /// <summary>
        /// File created or last write date must be less than or equal to this date.
        /// </summary>
        /// <remarks>Set to null to indicate no maximum date requirement.</remarks>
        public DateTime? MaximumFileDate { get; set; }

        /// <summary>
        /// Constructs a new instance of <see cref="Searcher"/>
        /// </summary>
        /// <returns></returns>
        public Searcher ConstructSearcher()
        {
            DirectoryScanner scanner = new DirectoryScanner(Path, FileNamePattern, Recursive, Hidden, System, MinimumFileDate, MaximumFileDate);

            Regex expression = new Regex(LinePattern);

            ILineMatcher lineMatcher = LinesNotMatching
                              ? (ILineMatcher) new LineNotMatchingMatcher(expression)
                              : new LineMatcher(expression);

            IFileMatcher fileMatcher = FilesNotMatching
                                           ? (IFileMatcher) new FileNotMatchingMatcher(lineMatcher)
                                           : new FileMatcher(lineMatcher);

            return new Searcher(scanner.GetFiles(), fileMatcher, new LineReader());
        }
    }
}
