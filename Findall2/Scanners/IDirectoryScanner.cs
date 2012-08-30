using System.Collections.Generic;

namespace Findall2.Scanners
{
    /// <summary>
    /// Scans for files in a directory.
    /// </summary>
    public interface IDirectoryScanner
    {
        IEnumerable<string> GetFiles();
    }
}
