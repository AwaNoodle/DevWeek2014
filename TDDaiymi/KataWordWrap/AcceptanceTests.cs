using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KataWordWrap
{
    [TestFixture]
    public class AcceptanceTests
    {
        [TestCase("The quick brown fox jumps over the lazy dog", 9, "The quick\nbrown fox\njumps\nover the\nlazy dog")]
        [TestCase("Mary had a little lamb", 5, "Mary\nhad a\nlittl\ne\nlamb")]
        public void Popular_texts(string text, int maxLineLength, string expected)
        {
            var sut = new Wrapper();
            var result = sut.Wrap(text, maxLineLength);
            Assert.AreEqual(expected, result);
        }

        [TestCase("zeile1 zeile2", 6, "zeile1\nzeile2")]
        [TestCase("zeile1 zeile2", 7, "zeile1\nzeile2")]
        [TestCase("zeile1 zeile2", 8, "zeile1\nzeile2")]
        [TestCase("zeile1 z zeile2", 8, "zeile1 z\nzeile2")]
        public void Wrap_words_shorter_than_line(string text, int maxLineLength, string expected)
        {
            var sut = new Wrapper();
            var result = sut.Wrap(text, maxLineLength);
            Assert.AreEqual(expected, result);
        }

        [TestCase("zeile", 5, "zeile")]
        [TestCase("zeile1zeile2", 6, "zeile1\nzeile2")]
        [TestCase("zeile1zeile2zeile3", 6, "zeile1\nzeile2\nzeile3")]
        public void Wrap_words_longer_than_line(string text, int maxLineLength, string expected)
        {
            var sut = new Wrapper();
            var result = sut.Wrap(text, maxLineLength);
            Assert.AreEqual(expected, result);
        }
    }
}
