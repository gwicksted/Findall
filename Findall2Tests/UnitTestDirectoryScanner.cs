using System.Collections.Generic;
using Findall2.Scanners;

namespace Findall2Tests
{
    /// <summary>
    /// A unit-test version of the <see cref="DirectoryScanner"/>.
    /// This class does not touch the file system.
    /// </summary>
    public class UnitTestDirectoryScanner : IDirectoryScanner
    {
        private readonly IEnumerable<string> _results;

        public UnitTestDirectoryScanner(IEnumerable<string> resultsToReturn)
        {
            _results = resultsToReturn;
        }

        public IEnumerable<string> GetFiles()
        {
            return _results;
        }
    }
}
