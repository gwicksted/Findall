using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Findall2.Matchers;
using Findall2.Models;
using NUnit.Framework;
using log4net.Config;

namespace Findall2Tests.Matchers
{
    [TestFixture]
    public class LineMatcherTests
    {
        [Test]
        public void TestMatchesEachLetter()
        {
            Regex expression = new Regex("[a-z]");

            const string line = "abc";

            LineMatcher matcher = new LineMatcher(expression);

            IList<ColumnMatch> results = matcher.Match(line).ToList();

            Assert.AreEqual(3, results.Count());

            Assert.AreEqual(0, results[0].Column);
            Assert.AreEqual(1, results[0].Length);

            Assert.AreEqual(1, results[1].Column);
            Assert.AreEqual(1, results[1].Length);

            Assert.AreEqual(2, results[2].Column);
            Assert.AreEqual(1, results[2].Length);
        }

        [Test]
        public void TestMatchesEntireLine()
        {
            Regex expression = new Regex("^[a-z]+$");

            const string line = "abc";

            LineMatcher matcher = new LineMatcher(expression);

            IList<ColumnMatch> results = matcher.Match(line).ToList();

            Assert.AreEqual(1, results.Count());

            Assert.AreEqual(0, results[0].Column);
            Assert.AreEqual(3, results[0].Length);
        }

        [Test]
        public void TestDoesNotMatchLine()
        {
            Regex expression = new Regex("^[a-z]+$");

            const string line = "abc1";

            LineMatcher matcher = new LineMatcher(expression);

            IList<ColumnMatch> results = matcher.Match(line).ToList();

            Assert.AreEqual(0, results.Count());
        }

        [Test]
        public void TestEmpty()
        {
            Regex expression = new Regex("^[a-z]+$");

            string line = string.Empty;

            LineMatcher matcher = new LineMatcher(expression);

            IList<ColumnMatch> results = matcher.Match(line).ToList();

            Assert.AreEqual(0, results.Count());
        }

        [Test]
        public void TestNull()
        {
            Regex expression = new Regex("^[a-z]+$");

            LineMatcher matcher = new LineMatcher(expression);

            Assert.Throws<ArgumentNullException>(() => matcher.Match(null));
        }

        [Test]
        public void TestNullConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new LineMatcher(null));
        }
    }
}
