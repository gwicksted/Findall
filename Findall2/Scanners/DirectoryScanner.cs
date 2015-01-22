using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Findall2.Scanners
{
    /// <summary>
    /// Scans for files in a directory.
    /// </summary>
    public class DirectoryScanner
    {
        private readonly string _path;

        private readonly bool _forceRefresh;
        private readonly bool _recursive;

        private readonly string _pattern;

        private readonly bool _hiddenAllowed;
        private readonly bool _systemAllowed;

        private readonly DateTime? _minDate;
        private readonly DateTime? _maxDate;

        /// <summary>
        /// Creates a new instance of DirectoryScanner.
        /// </summary>
        /// <param name="path">The full path to the base directory to start scanning from.</param>
        /// <param name="pattern">The file name pattern for matching (such as *.*).</param>
        /// <param name="recursive">Indicates subfolders should also be scanned.</param>
        /// <param name="hiddenAllowed">Indicates hidden files are acceptable.</param>
        /// <param name="systemAllowed">Indicates system files are acceptable.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="path"/> is null or <see cref="string.Empty"/>.
        /// If <paramref name="pattern"/> is null or <see cref="string.Empty"/>.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// If the <paramref name="path"/> could not be located on disk.
        /// </exception>
        public DirectoryScanner(string path, string pattern, bool recursive, bool hiddenAllowed, bool systemAllowed)
            : this(path, pattern, recursive, hiddenAllowed, systemAllowed, null, null, false)
        {
        }

        /// <summary>
        /// Creates a new instance of DirectoryScanner.
        /// </summary>
        /// <param name="path">The full path to the base directory to start scanning from.</param>
        /// <param name="pattern">The file name pattern for matching (such as *.*).</param>
        /// <param name="recursive">Indicates subfolders should also be scanned.</param>
        /// <param name="hiddenAllowed">Indicates hidden files are acceptable.</param>
        /// <param name="systemAllowed">Indicates system files are acceptable.</param>
        /// <param name="minimumFileDate">
        /// The created or last write time of the file must be greater than or equal to this value.
        /// Provide a null to indicate no constraint on minimum time.
        /// </param>
        /// <param name="maximumFileDate">
        /// The created or last write time of the file must be less than or equal to this value.
        /// Provide a null to indicate no constraint on the maximum time.
        /// </param>
        /// <param name="forceLastWriteRefresh">
        /// Forces each file's last properties (LastWriteTime) to be refreshed by opening and closing
        /// the file without requesting any access to it.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="path"/> is null or <see cref="string.Empty"/>.
        /// If <paramref name="pattern"/> is null or <see cref="string.Empty"/>.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// If the <paramref name="path"/> could not be located on disk.
        /// </exception>
        public DirectoryScanner(string path, string pattern, bool recursive, bool hiddenAllowed, bool systemAllowed,
                                DateTime? minimumFileDate, DateTime? maximumFileDate, bool forceLastWriteRefresh)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException("pattern");
            }

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(string.Format("Directory '{0}' not found", path));
            }

            _path = path;

            _recursive = recursive;

            _pattern = pattern;

            _hiddenAllowed = hiddenAllowed;

            _systemAllowed = systemAllowed;

            _minDate = minimumFileDate;

            _maxDate = maximumFileDate;

            _forceRefresh = forceLastWriteRefresh;
        }

        /// <summary>
        /// Obtain a list of files located by the scanner.
        /// </summary>
        /// <returns>A list of full file paths.</returns>
        public IEnumerable<string> GetFiles()
        {
            // The following gets access errors due to Recycle bin permissions:
            //return Directory.EnumerateFiles(_path, _pattern, _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Where(IsFileAcceptable);

            return SafeDirectoryEnumerator.EnumerateFiles(_path, _pattern, _recursive).Where(IsFileAcceptable);
        }

        /// <summary>
        /// Indicates the file attributes and dates match what requirements were passed in during construction.
        /// </summary>
        /// <param name="file">The full path to the file on disk.</param>
        /// <returns>
        /// True if the file matches the requirements.
        /// False if it fails one or more requirement.
        /// </returns>
        private bool IsFileAcceptable(string file)
        {
            FileInfo info = new FileInfo(file);

            return AreAttributesAcceptable(info) && AreTimesAcceptable(info);
        }

        /// <summary>
        /// Indicates the file dates match what requirements were passed in during construction.
        /// The <see cref="FileInfo.CreationTime"/> must be &gt;= the minimum date or the
        /// <see cref="FileInfo.LastWriteTime"/> must be &gt;= the minimum date or the
        /// minimum date passed in during construction must be null.
        /// The <see cref="FileInfo.CreationTime"/> must be &lt;= the maximum date or the
        /// <see cref="FileInfo.LastWriteTime"/> must be &lt;= the maximum date or the
        /// maximum date passed in during construction must be null.
        /// </summary>
        /// <param name="info"><see cref="FileInfo"/> to collect file dates from.</param>
        /// <returns>
        /// True if the file matches the requirements.
        /// False if it fails one or more requirement.
        /// </returns>
        private bool AreTimesAcceptable(FileInfo info)
        {
            bool valid = false;

            if (_forceRefresh)
            {
                FileRefresher.RefreshFile(info.FullName);
            }

            if (_minDate != null || _maxDate != null)
            {
                DateTime created = info.CreationTime;
                DateTime modified = info.LastWriteTime;

                valid = (_minDate == null || created >= _minDate || modified >= _minDate) &&
                        (_maxDate == null || created <= _maxDate || modified <= _maxDate);
            }

            return valid;
        }

        /// <summary>
        /// Indicates the file attributes match what requirements were passed in during construction.
        /// Examples are <see cref="FileAttributes.Hidden"/> or <see cref="FileAttributes.System"/>.
        /// </summary>
        /// <param name="info"><see cref="FileInfo"/> to collect attributes from.</param>
        /// <returns>
        /// True if the file matches the requirements.
        /// False if it fails one or more requirement.
        /// </returns>
        private bool AreAttributesAcceptable(FileInfo info)
        {
            FileAttributes attributes = info.Attributes;

            bool fileIsHidden = (attributes & FileAttributes.Hidden) != FileAttributes.Hidden;

            bool fileIsSystem = (attributes & FileAttributes.System) != FileAttributes.System;

            return (_hiddenAllowed || fileIsHidden) && (_systemAllowed || fileIsSystem);
        }
    }
}