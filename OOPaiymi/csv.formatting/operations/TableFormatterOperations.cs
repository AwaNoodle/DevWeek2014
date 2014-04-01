using System.Collections.Generic;
using System.Linq;

namespace csv.formatting.operations
{
    class TableFormatterOperations
    {
        public static int[] Calculate_column_widths(IEnumerable<string[]> records)
        {
            var colWidths = Enumerable.Range(0, records.First().Count())
                                      .Select(i => records.Select(r => r[i].Length).Max())
                                      .ToArray();
            return colWidths;
        }


        public string Format_header(IEnumerable<string[]> records, int[] colWidths)
        {
            return Assemble_table_line(records.First(), colWidths);
        }

        public static string Format_header_unline(int[] colWidths)
        {
            var underlineRecord = Enumerable.Range(0, colWidths.Length).Select(i => new string('-', colWidths[i]));
            return string.Join("+", underlineRecord);
        }

        public IEnumerable<string> Format_body(IEnumerable<string[]> records, int[] colWidths)
        {
            return records.Where((r, i) => i > 0).Select<string[], string>(r => Assemble_table_line(r, colWidths));
        }


        public string Assemble_table_line(string[] recordFields, int[] colWidths)
        {
            return string.Join("|", recordFields.Select((f, i) => f.PadRight(colWidths[i])));
        }

     
        public static string Assemble_table(string header, string underline, IEnumerable<string> body)
        {
            return string.Join("\n", new[] { header, underline }.Concat(body));
        }
    }
}