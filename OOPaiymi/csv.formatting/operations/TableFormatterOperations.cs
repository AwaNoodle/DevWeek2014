using System.Collections.Generic;
using System.Linq;
using csv.formatting.data;

namespace csv.formatting.operations
{
    class TableFormatterOperations
    {
        public int[] Calculate_column_widths(IEnumerable<CSVRecord> records)
        {
            var colWidths = Enumerable.Range(0, records.First().Fields.Count())
                                      .Select(i => records.Select(r => r.Fields[i].Length).Max())
                                      .ToArray();
            return colWidths;
        }


        public string Format_header(IEnumerable<CSVRecord> records, int[] colWidths)
        {
            return Assemble_table_line(records.First(), colWidths);
        }

        public string Format_header_unline(int[] colWidths)
        {
            var underlineRecord = Enumerable.Range(0, colWidths.Length).Select(i => new string('-', colWidths[i]));
            return string.Join("+", underlineRecord);
        }

        public IEnumerable<string> Format_body(IEnumerable<CSVRecord> records, int[] colWidths)
        {
            return records.Where((r, i) => i > 0).Select<CSVRecord, string>(r => Assemble_table_line(r, colWidths));
        }


        public string Assemble_table_line(CSVRecord record, int[] colWidths)
        {
            return string.Join("|", record.Fields.Select((f, i) => f.PadRight(colWidths[i])));
        }

     
        public string Assemble_table(string header, string underline, IEnumerable<string> body)
        {
            return string.Join("\n", new[] { header, underline }.Concat(body));
        }
    }
}