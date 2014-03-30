using NUnit.Framework;

namespace KataWordWrap
{
    [TestFixture]
    public class ScaffoldingTests
    {
        [Test]
        public void Single_short_word_in_line()
        {
            var sut = new Wrapper();

            var result = sut.Wrap("word", 4);

            Assert.AreEqual("word", result);
        } 
    }
}