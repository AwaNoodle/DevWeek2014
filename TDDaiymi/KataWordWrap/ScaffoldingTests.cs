using System.Collections.Generic;
using NUnit.Framework;
using Telerik.JustMock;

namespace KataWordWrap
{
    [TestFixture]
    public class ScaffoldingTests
    {
        [Test]
        public void Split_line_into_words()
        {
            var text = " a  b   c ";

            var result = new string[0];

            Assert.AreEqual(new[] { "a", "b", "c" }, result);
        }


        [Test]
        public void Wrap_two_short_words()
        {
            var sut = new Wrapper();
            Mock.NonPublic.Arrange<IEnumerable<string>>(sut, "Split_line_into_words", "word1 word2")
                          .Returns(new[]{"word1", "word2"}).MustBeCalled();
            Mock.NonPublic.Arrange<string>(sut, "Build_lines_from_words", new[]{"word1", "word2"}, 5)
                          .Returns("word1\nword2").MustBeCalled();

            var result = sut.Wrap("word1 word2", 5);

            Assert.AreEqual("word1\nword2", result);
            Mock.Assert(sut);
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