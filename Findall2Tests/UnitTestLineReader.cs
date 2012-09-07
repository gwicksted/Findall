using System.Collections.Generic;
using Findall2.Readers;

namespace Findall2Tests
{
    /// <summary>
    /// A unit-test version of the <see cref="LineReader"/>.
    /// </summary>
    public class UnitTestLineReader : ILineReader
    {
        private readonly IEnumerable<string> _results;

        public UnitTestLineReader(IEnumerable<string> resultsToReturn)
        {
            _results = resultsToReturn;
        }

        public IEnumerable<string> GetLines(string file)
        {
            return _results;
        }
    }
}
