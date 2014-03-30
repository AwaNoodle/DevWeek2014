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
            return Build_lines_from_words(words, maxLineLength);
        }


        private IEnumerable<string> Split_line_into_words(string text)
        {
            return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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