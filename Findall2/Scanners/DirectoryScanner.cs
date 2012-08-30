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

        private readonly bool _recursive;

        private readonly string _pattern;

        private readonly bool _hiddenAllowed;

        private readonly bool _systemAllowed;

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
        }

        /// <summary>
        /// Obtain a list of files located by the scanner.
        /// </summary>
        /// <returns>A list of full file paths.</returns>
        public IEnumerable<string> GetFiles()
        {
            return Directory.EnumerateFiles(_path, _pattern, _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Where(IsFileAcceptable);
        }

        /// <summary>
        /// Indicates the file attributes match what requirements were passed in during construction.
        /// Examples are <see cref="FileAttributes.Hidden"/> or <see cref="FileAttributes.System"/>.
        /// </summary>
        /// <param name="file">The full path to the file on disk.</param>
        /// <returns>
        /// True if the file matches the requirements.
        /// False if it fails one or more requirement.
        /// </returns>
        private bool IsFileAcceptable(string file)
        {
            FileAttributes attributes = new FileInfo(file).Attributes;

            bool fileIsHidden = (attributes & FileAttributes.Hidden) != FileAttributes.Hidden;
            
            bool fileIsSystem = (attributes & FileAttributes.System) != FileAttributes.System;
            
            return (_hiddenAllowed || fileIsHidden) && (_systemAllowed || fileIsSystem);
        }
    }
}
