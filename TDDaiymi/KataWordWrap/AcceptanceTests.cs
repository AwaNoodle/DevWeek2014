using System;
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
    }

    public class Wrapper
    {
        public string Wrap(string text, int maxLineLength)
        {
            throw new NotImplementedException();
        }
    }
}
