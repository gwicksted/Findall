using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Findall2.Matchers;
using Findall2.Models;
using NUnit.Framework;

namespace Findall2Tests.Matchers
{
    [TestFixture]
    public class LineNotMatchingMatcherTests
    {
        [Test]
        public void TestDoesNotMatchLineWithMultipleMatches()
        {
            Regex expression = new Regex("[a-z]");

            const string line = "abc";

            LineNotMatchingMatcher matcher = new LineNotMatchingMatcher(expression);

            IList<ColumnMatch> results = matcher.Match(line).ToList();

            Assert.AreEqual(0, results.Count());
        }

        [Test]
        public void TestDoesNotMatchLine()
        {
            Regex expression = new Regex("^[a-z]+$");

            const string line = "abc";

            LineNotMatchingMatcher matcher = new LineNotMatchingMatcher(expression);

            IList<ColumnMatch> results = matcher.Match(line).ToList();

            Assert.AreEqual(0, results.Count());
        }

        [Test]
        public void TestMatchesLine()
        {
            Regex expression = new Regex("^[a-z]+$");

            const string line = "abc1";

            LineNotMatchingMatcher matcher = new LineNotMatchingMatcher(expression);

            IList<ColumnMatch> results = matcher.Match(line).ToList();

            Assert.AreEqual(1, results.Count());

            Assert.AreEqual(0, results[0].Column);
            Assert.AreEqual(0, results[0].Length);
        }

        [Test]
        public void TestMatchesEmpty()
        {
            Regex expression = new Regex("^[a-z]+$");

            string line = string.Empty;

            LineNotMatchingMatcher matcher = new LineNotMatchingMatcher(expression);

            IList<ColumnMatch> results = matcher.Match(line).ToList();

            Assert.AreEqual(1, results.Count());

            Assert.AreEqual(0, results[0].Column);
            Assert.AreEqual(0, results[0].Length);
        }

        [Test]
        public void TestNull()
        {
            Regex expression = new Regex("^[a-z]+$");

            LineNotMatchingMatcher matcher = new LineNotMatchingMatcher(expression);

            Assert.Throws<ArgumentNullException>(() => matcher.Match(null));
        }

        [Test]
        public void TestNullConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new LineNotMatchingMatcher(null));
        }
    }
}
