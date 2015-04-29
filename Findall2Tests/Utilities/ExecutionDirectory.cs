using System;
using System.IO;
using System.Reflection;

namespace Findall2Tests.Utilities
{
    internal static class ExecutionDirectory
    {
        public static string FindExecutionDirectory()
        {
            return FindExecutionDirectory(Assembly.GetExecutingAssembly());
        }

        public static string FindExecutionDirectory(Assembly assembly)
        {
            string applicationPath = assembly.CodeBase;
            UriBuilder uri = new UriBuilder(applicationPath);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
