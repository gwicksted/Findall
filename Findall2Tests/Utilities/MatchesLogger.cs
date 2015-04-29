using System.Collections.Generic;
using Findall2.Models;
using log4net;

namespace Findall2Tests.Utilities
{
    public static class MatchesLogger
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void LogResult(IList<FileMatch> matches)
        {
            foreach (FileMatch fileMatch in matches)
            {
                Log.DebugFormat("Match {0}", fileMatch.Path);

                foreach (LineMatch lineMatch in fileMatch.Matches)
                {
                    Log.DebugFormat("\tLine {0}", lineMatch.LineNumber);

                    foreach (ColumnMatch columnMatch in lineMatch.Matches)
                    {
                        Log.DebugFormat("\t\tColumn {0} ({1} length): {2}", columnMatch.Column, columnMatch.Length,
                                    columnMatch.GetMatchedSubstring(lineMatch.Line));
                    }
                }
            }
        }

        public static void LogFiles(IList<FileMatch> matches)
        {
            foreach (FileMatch fileMatch in matches)
            {
                Log.DebugFormat("Match {0}", fileMatch.Path);
            }
        }
    }
}
