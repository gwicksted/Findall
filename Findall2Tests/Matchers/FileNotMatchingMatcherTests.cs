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
    public class FileNotMatchingMatcherTests
    {
        [Test]
        public void TestNullConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new FileNotMatchingMatcher(null));
        }

        [Test]
        public void MatchMultipleLines()
        {
            FileNotMatchingMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new[] {"abc", "def", "ghi"};

            IList<LineMatch> matches = matcher.MatchAll(lines).ToList();

            Assert.AreEqual(0, matches.Count());
        }

        [Test]
        public void MatchOneLine()
        {
            FileNotMatchingMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new[] { "abc" };

            IList<LineMatch> matches = matcher.MatchAll(lines).ToList();

            Assert.AreEqual(0, matches.Count());
        }

        [Test]
        public void NoMatchNoLines()
        {
            FileNotMatchingMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new string[0];

            IList<LineMatch> matches = matcher.MatchAll(lines).ToList();

            Assert.AreEqual(1, matches.Count());
        }

        [Test]
        public void NoMatchManyLines()
        {
            FileNotMatchingMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new [] { "123", "456", "789" };

            IList<LineMatch> matches = matcher.MatchAll(lines).ToList();

            Assert.AreEqual(1, matches.Count());
        }

        [Test]
        public void NullLines()
        {
            FileNotMatchingMatcher matcher = GetLowerCaseWordFileMatcher();

            Assert.Throws<ArgumentNullException>(() => matcher.MatchAll(null));
        }

        private static FileNotMatchingMatcher GetLowerCaseWordFileMatcher()
        {
            Regex expression = new Regex("[a-z]+");

            LineMatcher lineMatcher = new LineMatcher(expression);

            FileNotMatchingMatcher matcher = new FileNotMatchingMatcher(lineMatcher);

            return matcher;
        }
    }
}
