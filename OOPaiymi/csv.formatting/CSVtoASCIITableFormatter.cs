using csv.formatting.integration;
using csv.formatting.operations;

namespace csv.formatting
{
    public class CSVtoASCIITableFormatter
    {
        public string Format(string csv)
        {
            var csvParser = new CSVParser();
            var formatter = new TableFormatter();

            var records = csvParser.Parse(csv);
            return formatter.Format_as_table(records);
        }
    }
}
