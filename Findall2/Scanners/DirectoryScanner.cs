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

        private readonly bool _hidden;

        /// <summary>
        /// Creates a new instance of DirectoryScanner.
        /// </summary>
        /// <param name="path">The full path to the base directory to start scanning from.</param>
        /// <param name="pattern">The file name pattern for matching (such as *.*).</param>
        /// <param name="recursive">Indicates subfolders should also be scanned.</param>
        /// <param name="hidden">Indicates hidden files are acceptable.</param>
        public DirectoryScanner(string path, string pattern, bool recursive, bool hidden)
        {
            _path = path;

            _recursive = recursive;

            _pattern = pattern;

            _hidden = hidden;
        }

        /// <summary>
        /// Obtain a list of files located by the scanner.
        /// </summary>
        /// <returns>A list of full file paths.</returns>
        public IEnumerable<string> GetFiles()
        {
            IEnumerable<string> files = Directory.EnumerateFiles(_path, _pattern, _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            return files.Where(file => _hidden || !IsHidden(file));
        }

        /// <summary>
        /// Indicates a file is marked with the <see cref="FileAttributes.Hidden"/> attribute.
        /// </summary>
        /// <param name="file">The full path to the file on disk.</param>
        /// <returns>True if the file is hidden, false otherwise.</returns>
        private static bool IsHidden(string file)
        {
            return (new FileInfo(file).Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
        }
    }
}
