using System;
using System.Collections.Generic;
using Findall2.Scanners;
using NUnit.Framework;
using log4net;
using log4net.Config;

namespace Findall2Tests.Integration
{
    [TestFixture]
    public class DirectoryScannerTests
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DirectoryScannerTests()
        {
            XmlConfigurator.Configure();
        }

        [Test]
        [Ignore("Integration")]
        public void TestRecycleBinError()
        {
            DirectoryScanner scanner = new DirectoryScanner(@"C:\$Recycle.Bin\", "*.*", true, false, false, DateTime.Now - new TimeSpan(1, 0, 0, 0), DateTime.Now);

            IEnumerable<string> files = scanner.GetFiles();

            foreach (string file in files)
            {
                Log.Debug(file);
            }
        }
    }
}
