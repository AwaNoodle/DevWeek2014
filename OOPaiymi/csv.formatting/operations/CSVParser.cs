using System;
using System.Collections.Generic;
using System.Linq;
using csv.formatting.data;

namespace csv.formatting.operations
{
    class CSVParser
    {
        public IEnumerable<CSVRecord> Parse(string csv)
        {
            var csvLines = csv.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            return csvLines.Select(l => Convert_CSV_line_to_record_fields(l, ";", new List<string>()).ToArray())
                           .Select(fields => new CSVRecord{Fields = fields});
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
                line = iDelim >= 0 ? line.Substring(iDelim + 1) : "";
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
    }
}