using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Findall2.Scanners
{
    /// <summary>
    /// Ability to recursively scan directories safely (without getting access errors on folders such as Recycle bin).
    /// </summary>
    /// <remarks>
    /// Code inspired by: http://stackoverflow.com/a/10321453
    /// </remarks>
    public class SafeDirectoryEnumerator
    {
        /// <summary>
        /// Used to indicate to the file system that all directories must be returned.
        /// </summary>
        private const string AllDirectories = "*.*";

        /// <summary>
        /// Safely enumerate all files in a directory.
        /// </summary>
        /// <param name="path">The directory to search in.</param>
        /// <param name="pattern">The file pattern to be used.</param>
        /// <param name="recursive">Indicates subdirectories should also be searched for matches.</param>
        /// <returns>A list of full paths to files matching the provided pattern.</returns>
        public static IEnumerable<string> EnumerateFiles(string path, string pattern, bool recursive)
        {
            IEnumerable<string> files = EnumerateFileSystemEntries(path, pattern);

            foreach (string file in files)
            {
                yield return file;
            }

            if (recursive)
            {
                foreach (string directory in EnumerateDirectories(path))
                {
                    files = EnumerateFiles(directory, pattern, true);

                    foreach (string file in files)
                    {
                        yield return file;
                    }
                }
            }
        }

        /// <summary>
        /// List all subdirectories inside the provided <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The full file system path to the parent directory.</param>
        /// <returns>A list of full paths to subdirectories within the parent directory.</returns>
        public static IEnumerable<string> EnumerateDirectories(string path)
        {
            IEnumerable<string> directories = null;

            try
            {
                directories = Directory.EnumerateDirectories(path, AllDirectories, SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException)
            {
            }

            if (directories == null) yield break;

            foreach (string directory in directories)
            {
                yield return directory;
            }
        }

        /// <summary>
        /// List all files matching a certain file system <paramref name="pattern"/> inside the parent directory.
        /// </summary>
        /// <param name="path">The directory containing files to match.</param>
        /// <param name="pattern">The file system pattern (such as *.txt or *.*).</param>
        /// <returns>A list of files inside the <paramref name="path"/> matching the <paramref name="pattern"/>.</returns>
        /// <remarks>This is not a recursive search.</remarks>
        public static IEnumerable<string> EnumerateFileSystemEntries(string path, string pattern)
        {
            IEnumerable<string> files = null;

            try
            {
                files = Directory.EnumerateFileSystemEntries(path, pattern, SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (IOException)
            {
                // Path not found
            }

            if (files == null) yield break;

            // Check exists otherwise this could return matching directories
            foreach (string file in files.Where(File.Exists))
            {
                yield return file;
            }
        }
    }
}
