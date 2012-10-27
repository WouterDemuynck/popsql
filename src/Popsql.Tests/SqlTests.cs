using Microsoft.VisualStudio.TestTools.UnitTesting;
using Popsql.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Tests
{
    [TestClass]
    public class SqlTests
    {
        [TestMethod]
        public void Select_WithNullColumns_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => Sql.Select(null));
        }

        [TestMethod]
        public void Select_WithColumns_ReturnsSqlSelectWithColumns()
        {
            var query = Sql
                .Select("Id", "Name", "Email");

            Assert.AreEqual(3, query.Columns.Count());
            Assert.AreEqual("Id", query.Columns.First().ColumnName);
            Assert.AreEqual("Name", query.Columns.Skip(1).First().ColumnName);
            Assert.AreEqual("Email", query.Columns.Last().ColumnName);
        }
    }
}
