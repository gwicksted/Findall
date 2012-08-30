using System.Collections.Generic;
using System.Diagnostics;
using Findall2.Models;

namespace Findall2Tests.Logging
{
    public static class MatchesLogger
    {
        public static void LogResult(IList<FileMatch> matches)
        {
            foreach (FileMatch fileMatch in matches)
            {
                Debug.Print("Match {0}", fileMatch.Path);

                foreach (LineMatch lineMatch in fileMatch.Matches)
                {
                    Debug.Print("\tLine {0}", lineMatch.LineNumber);

                    foreach (ColumnMatch columnMatch in lineMatch.Matches)
                    {
                        Debug.Print("\t\tColumn {0} ({1} length): {2}", columnMatch.Column, columnMatch.Length,
                                    columnMatch.GetMatchedSubstring(lineMatch.Line));
                    }
                }
            }
        }
    }
}
