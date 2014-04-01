using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csv.formatting
{
    public class CSVtoASCIITableFormatter
    {
        public string Format(string csv)
        {
            var records = Parse(csv);
            var colWidths = Calculate_column_widths(records);
            return Build_table(records, colWidths);
        }

        private string Build_table(IEnumerable<string[]> records, int[] colWidths)
        {
            var header = Format_header(records, colWidths);
            var underline = Format_header_unline(colWidths);
            var body = Format_body(records, colWidths);
            return Assemble_table(header, underline, body);
        }

        private static string Assemble_table(string header, string underline, IEnumerable<string> body)
        {
            return string.Join("\n", new[] { header, underline }.Concat(body));
        }

        private IEnumerable<string> Format_body(IEnumerable<string[]> records, int[] colWidths)
        {
            return records.Where((r, i) => i > 0).Select(r => Assemble_table_line(r, colWidths));
        }

        private static string Format_header_unline(int[] colWidths)
        {
            var underlineRecord = Enumerable.Range(0, colWidths.Length).Select(i => new string('-', colWidths[i]));
            return string.Join("+", underlineRecord);
        }

        private string Format_header(IEnumerable<string[]> records, int[] colWidths)
        {
            return Assemble_table_line(records.First(), colWidths);
        }

        private static int[] Calculate_column_widths(IEnumerable<string[]> records)
        {
            var colWidths = Enumerable.Range(0, records.First().Count())
                                      .Select(i => records.Select(r => r[i].Length).Max())
                                      .ToArray();
            return colWidths;
        }

        private IEnumerable<string[]> Parse(string csv)
        {
            var csvLines = csv.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            var records = csvLines.Select(l => Convert_CSV_line_to_record_fields(l, ";"));
            return records;
        }


        private string[] Convert_CSV_line_to_record_fields(string line, string delimiter)
        {
            return Convert_CSV_line_to_record_fields(line, delimiter, new List<string>()).ToArray();
        }

        private List<string> Convert_CSV_line_to_record_fields(string line, string delimiter, List<string> fields)
        {
            if (line == "") return fields;

            if (line.StartsWith("\""))
            {
                line = line.Substring(1);
                var iApo = line.IndexOf("\"");
                fields.Add(line.Substring(0, iApo).Trim());

                line = line.Substring(iApo + 1);
                var iDelim = line.IndexOf(delimiter);
                if (iDelim >= 0)
                    line = line.Substring(iDelim + 1);
                else
                    line = "";
            }
            else
            {
                var iDelim = line.IndexOf(delimiter);
                if (iDelim >= 0)
                {
                    fields.Add(line.Substring(0, iDelim).Trim());
                    line = line.Substring(iDelim + 1);
                }
                else
                {
                    fields.Add(line.Trim());
                    line = "";
                }
            }

            return Convert_CSV_line_to_record_fields(line, delimiter, fields);
        }

        private string Assemble_table_line(string[] recordFields, int[] colWidths)
        {
            return string.Join("|", recordFields.Select((f, i) => f.PadRight(colWidths[i])));
        }
    }
}
