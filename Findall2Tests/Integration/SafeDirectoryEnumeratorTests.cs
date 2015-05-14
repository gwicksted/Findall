using System.Reflection;
using Findall2.Scanners;
using log4net;
using NUnit.Framework;

namespace Findall2Tests.Integration
{
    [TestFixture]
    public class SafeDirectoryEnumeratorTests
    {
        [Test]
        public void TestRecycleBinError()
        {
            Assert.DoesNotThrow(() => SafeDirectoryEnumerator.EnumerateDirectories(@"C:\$Recycle.Bin\"));
        }

        [Test]
        public void TestNotFoundDirectory()
        {
            Assert.DoesNotThrow(() => SafeDirectoryEnumerator.EnumerateDirectories(@"C:\averylongdirectorynamethathopefullydoesntexist\"));
        }
    }
}
