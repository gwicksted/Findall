using System.Collections.Generic;

namespace Findall2.Readers
{
    /// <summary>
    /// Reads lines of text from a file.
    /// </summary>
    public interface ILineReader
    {
        /// <summary>
        /// Reads all lines of text from a file.
        /// </summary>
        /// <param name="file">The full path to the file on disk.</param>
        /// <returns>A list of strings representing the lines of text in the file.</returns>
        IEnumerable<string> GetLines(string file);
    }
}
