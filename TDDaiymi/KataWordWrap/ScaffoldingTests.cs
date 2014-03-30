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
        public void Build_multi_word_lines()
        {
            var words = new[] {"a", "bc", "def"};
            var maxLineLen = 5;

            var result = "";
            var line = "";
            Action append_line = () => {
                if (line != "") {
                    if (result != "") result += "\n";
                    result += line;
                    line = "";
                }
            }; 
            
            while (words.Length > 0)
            {
                var word = words[0];

                if (line.Length + 1 + word.Length > maxLineLen)
                    append_line();
                else
                    if (line != "") line += " ";

                line += word;
                words = words.Skip(1).ToArray();
            }
            append_line();

            Assert.AreEqual("a bc\ndef", result);
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