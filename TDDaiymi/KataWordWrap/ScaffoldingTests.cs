using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Telerik.JustMock;

namespace KataWordWrap
{
    [TestFixture]
    public class ScaffoldingTests
    {
        [Test]
        public void Split_long_word()
        {
            var words = new[] {"abc"};
            var maxLineLength = 2;

            var result = new string[0];

            Assert.AreEqual(new[]{"ab", "c"}, result);
        }


        [Test]
        public void Wrap_word_longer_than_line()
        {
            var sut = new Wrapper();
            Mock.NonPublic.Arrange<IEnumerable<string>>(sut, "Split_long_words", (IEnumerable<string>)new[]{"longword"})
                          .Returns(new[]{"long", "word"}).MustBeCalled();

            var result = sut.Wrap("longword", 4);

            Assert.AreEqual("long\nword", result);
            Mock.Assert(sut);
        }


        [Test]
        public void Put_more_words_in_one_line()
        {
            var sut = new Wrapper();

            var result = sut.Wrap("a bc def", 5);

            Assert.AreEqual("a bc\ndef", result);
        }

        [Test]
        public void Wrap_two_short_words()
        {
            var sut = new Wrapper();

            var result = sut.Wrap("word1 word2", 5);

            Assert.AreEqual("word1\nword2", result);
        }


        [Test]
        public void Single_short_word_in_line()
        {
            var sut = new Wrapper();

            var result = sut.Wrap("word", 4);

            Assert.AreEqual("word", result);
        }
    }
}