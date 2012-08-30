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
    public class FileMatcherTests
    {
        [Test]
        public void TestNullConstructor()
        {
            Assert.Throws<ArgumentNullException>(() => new FileMatcher(null));
        }

        [Test]
        public void MatchMultipleLines()
        {
            FileMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new[] {"abc", "def", "ghi"};

            IList<LineMatch> matches = matcher.MatchAll(lines, "test.txt").ToList();

            Assert.AreEqual(3, matches.Count());

            Assert.AreEqual(0, matches[0].LineNumber);
            Assert.AreEqual(1, matches[0].Matches.Count());
            Assert.AreEqual("abc", matches[0].Line);

            Assert.AreEqual(1, matches[1].LineNumber);
            Assert.AreEqual(1, matches[1].Matches.Count());
            Assert.AreEqual("def", matches[1].Line);

            Assert.AreEqual(2, matches[2].LineNumber);
            Assert.AreEqual(1, matches[2].Matches.Count());
            Assert.AreEqual("ghi", matches[2].Line);
        }

        [Test]
        public void MatchOneLine()
        {
            FileMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new[] { "abc" };

            IList<LineMatch> matches = matcher.MatchAll(lines, "test.txt").ToList();

            Assert.AreEqual(1, matches.Count());

            Assert.AreEqual(0, matches[0].LineNumber);
            Assert.AreEqual(1, matches[0].Matches.Count());
            Assert.AreEqual("abc", matches[0].Line);
        }

        [Test]
        public void NoMatchNoLines()
        {
            FileMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new string[0];

            IList<LineMatch> matches = matcher.MatchAll(lines, "test.txt").ToList();

            Assert.AreEqual(0, matches.Count());
        }

        [Test]
        public void NoMatchManyLines()
        {
            FileMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new [] { "123", "456", "789" };

            IList<LineMatch> matches = matcher.MatchAll(lines, "test.txt").ToList();

            Assert.AreEqual(0, matches.Count());
        }

        [Test]
        public void NullLines()
        {
            FileMatcher matcher = GetLowerCaseWordFileMatcher();

            Assert.Throws<ArgumentNullException>(() => matcher.MatchAll(null, "test.txt"));
        }

        [Test]
        public void EmptyFileNameNoMatches()
        {
            FileMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new[] { "123", "456", "789" };

            Assert.Throws<ArgumentNullException>(() => matcher.MatchAll(lines, string.Empty));
        }

        [Test]
        public void NullFileNameNoMatches()
        {
            FileMatcher matcher = GetLowerCaseWordFileMatcher();

            string[] lines = new[] { "123", "456", "789" };

            Assert.Throws<ArgumentNullException>(() => matcher.MatchAll(lines, null));
        }

        private static FileMatcher GetLowerCaseWordFileMatcher()
        {
            Regex expression = new Regex("[a-z]+");

            LineMatcher lineMatcher = new LineMatcher(expression);

            FileMatcher matcher = new FileMatcher(lineMatcher);
            return matcher;
        }
    }
}
