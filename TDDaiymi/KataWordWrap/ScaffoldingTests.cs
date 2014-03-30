using NUnit.Framework;

namespace KataWordWrap
{
    [TestFixture]
    public class ScaffoldingTests
    {
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