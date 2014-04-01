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
            var csvLines = csv.Split(new[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            var records = csvLines.Select(l => Convert_line_to_record_fields(l, ";"));

            var colWidths = Enumerable.Range(0, records.First().Count())
                          .Select(i => records.Select(r => r[i].Length).Max())
                          .ToArray();
            var headline = Create_disply_line_for_record(records.First(), colWidths);

            var underlineRecord = Enumerable.Range(0, colWidths.Length).Select(i => new string('-', colWidths[i]));
            var underline = string.Join("+", underlineRecord);

            var displayLines = new[] { headline, underline }.Concat(records.Where((r, i) => i > 0).Select(r => Create_disply_line_for_record(r, colWidths)));

            return string.Join("\n", displayLines);
        }


        private string[] Convert_line_to_record_fields(string line, string delimiter)
        {
            return Convert_line_to_record_fields(line, delimiter, new List<string>()).ToArray();
        }

        private List<string> Convert_line_to_record_fields(string line, string delimiter, List<string> fields)
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

            return Convert_line_to_record_fields(line, delimiter, fields);
        }

        private string Create_disply_line_for_record(string[] recordFields, int[] colWidths)
        {
            return string.Join("|", recordFields.Select((f, i) => f.PadRight(colWidths[i])));
        }
    }
}
