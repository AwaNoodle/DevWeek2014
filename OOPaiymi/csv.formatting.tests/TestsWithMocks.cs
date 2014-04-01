using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Telerik.JustMock;
using csv.formatting.data;
using csv.formatting.operations;

namespace csv.formatting.tests
{
    [TestFixture]
    public class TestsWithMocks
    {
        [TestCase("h1;h2;h3\na;b;c\nd;e;f","h1|h2 |h3  \n--+---+----\na |b  |c   \nd |e  |f   ")]
        public void Mock_col_width_calculation(string csv, string expected)
        {
            var tbopsMock = Mock.Create<TableFormatterOperations>();
            Mock.Arrange(() => tbopsMock.Calculate_column_widths(null))
                .IgnoreArguments()
                .IgnoreInstance()
                .Returns(new[] {2, 3, 4});

            var sut = new CSVtoASCIITableFormatter();

            var table = sut.Format(csv);

            Assert.AreEqual(expected, table);
        }
    }
}
