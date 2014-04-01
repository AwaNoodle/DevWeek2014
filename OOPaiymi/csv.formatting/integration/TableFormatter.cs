using System.Collections.Generic;
using csv.formatting.data;
using csv.formatting.operations;

namespace csv.formatting.integration
{
    class TableFormatter
    {
        private readonly TableFormatterOperations _tableFormatterOperations = new TableFormatterOperations();

        public string Format_as_table(IEnumerable<CSVRecord> records)
        {
            var colWidths = _tableFormatterOperations.Calculate_column_widths(records);
            return Build_table(records, colWidths);
        }

        private string Build_table(IEnumerable<CSVRecord> records, int[] colWidths)
        {
            var header = _tableFormatterOperations.Format_header(records, colWidths);
            var underline = _tableFormatterOperations.Format_header_unline(colWidths);
            var body = _tableFormatterOperations.Format_body(records, colWidths);
            return _tableFormatterOperations.Assemble_table(header, underline, body);
        }
    }
}