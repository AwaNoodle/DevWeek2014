using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace csv.formatting.tests
{
    [TestFixture]
    public class AcceptanceTests
    {
        [TestCase("h1;h2;h34\nab;cde;e\nf;ghij;kl", 
                  "h1|h2  |h34\n--+----+---\nab|cde |e  \nf |ghij|kl ")]
        public void Some_CSV(string csv, string expected)
        {
            var sut = new CSVtoASCIITableFormatter();

            var table = sut.Format(csv);

            Assert.AreEqual(expected, table);
        }
    }
}
