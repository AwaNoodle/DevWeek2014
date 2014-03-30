using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }
    }
}