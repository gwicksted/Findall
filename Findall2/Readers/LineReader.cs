using System.Collections.Generic;
using System.IO;

namespace Findall2.Readers
{
    /// <summary>
    /// Reads lines of text from a file.
    /// </summary>
    public class LineReader : ILineReader
    {
        /// <summary>
        /// Reads all lines of text from a file.
        /// </summary>
        /// <param name="file">The full path to the file on disk.</param>
        /// <returns>A list of strings representing the lines of text in the file.</returns>
        /// <remarks>
        /// Uses <see cref="FileAccess.Read"/> and <see cref="FileShare.ReadWrite"/> to be
        /// the friendliest with other applications that may wish to access the same file.
        /// Uses <see cref="FileStream"/> combined with a <see cref="StreamReader"/> and
        /// yields lines to defer reading and keep memory usage low.
        /// </remarks>
        public IEnumerable<string> GetLines(string file)
        {
            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        yield return reader.ReadLine();
                    }
                }
            }
        }
    }
}
