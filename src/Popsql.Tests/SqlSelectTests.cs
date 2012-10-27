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
    public class SqlSelectTests
    {
        [TestMethod]
        public void Ctor_WithNullColumns_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlSelect(null));
        }

        [TestMethod]
        public void Ctor_WithColumns_SetsColumnsProperty()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name", "Email" });
            Assert.IsNotNull(select.Columns);
            Assert.AreEqual(3, select.Columns.Count());
            Assert.AreEqual("Id", select.Columns.First().ColumnName);
            Assert.AreEqual("Name", select.Columns.Skip(1).First().ColumnName);
            Assert.AreEqual("Email", select.Columns.Last().ColumnName);
        }

        public void From_WithNullSqlTable_ThrowsArgumentNull()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
            AssertEx.Throws<ArgumentNullException>(() => select.From(null));
        }

        public void From_WithSqlTable_SetsSourceProperty()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
            select.From("Users");

            Assert.IsNotNull(select.Source);
            Assert.AreEqual("Users", select.Source.TableName);
        }
    }
}
