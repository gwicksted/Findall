using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Findall2.Scanners;
using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Findall2Tests.Integration
{
    [TestFixture]
    public class DirectoryScannerTests
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        [Test]
        public void TestRecycleBinError()
        {
            DirectoryScanner scanner = new DirectoryScanner(@"C:\$Recycle.Bin\", "*.*", true, false, false, DateTime.Now - new TimeSpan(1, 0, 0, 0), DateTime.Now, true);

            IEnumerable<string> files = scanner.GetFiles();

            foreach (string file in files)
            {
                Log.Debug(file);
            }
        }

        [Test]
        public void TestNotFoundDirectory()
        {
            Assert.Throws<DirectoryNotFoundException>(() => new DirectoryScanner(@"C:\averylongdirectorynamethathopefullydoesntexist\", "*.*", true, false, false, DateTime.Now - new TimeSpan(1, 0, 0, 0), DateTime.Now, true));
        }
    }
}
