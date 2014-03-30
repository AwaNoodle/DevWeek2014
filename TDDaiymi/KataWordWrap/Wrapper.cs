using System;
using System.Collections.Generic;
using System.Linq;

namespace KataWordWrap
{
    public class Wrapper
    {
        public string Wrap(string text, int maxLineLength)
        {
            var words = Split_line_into_words(text);
            words = Split_long_words(words, maxLineLength);
            return Build_lines_from_words(words, maxLineLength);
        }


        private IEnumerable<string> Split_line_into_words(string text)
        {
            return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }


        private IEnumerable<string> Split_long_words(IEnumerable<string> words, int maxLineLength)
        {
            return words.SelectMany(word => Wrapper.Split_single_long_word(word, maxLineLength));
        }

        private static IEnumerable<string> Split_single_long_word(string word, int maxLineLength)
        {
            var syllables = new List<string>();
            while (word.Length > maxLineLength)
            {
                syllables.Add(word.Substring(0, maxLineLength));
                word = word.Substring(maxLineLength);
            }
            if (word != "") syllables.Add(word);

            return syllables;
        }


        private string Build_lines_from_words(IEnumerable<string> words, int maxLineLength)
        {
            var result = "";
            var line = "";
            Action append_line = () => {
                if (line != "") {
                    if (result != "") result += "\n";
                    result += line;
                    line = "";
                }
            };

            while (words.Any())
            {
                var word = words.First();

                if (line.Length + 1 + word.Length > maxLineLength)
                    append_line();
                else
                    if (line != "") line += " ";

                line += word;
                words = words.Skip(1);
            }
            append_line();

            return result;
        }
    }
}