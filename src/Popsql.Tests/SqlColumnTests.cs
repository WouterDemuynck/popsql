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
    public class SqlColumnTests
    {
        [TestMethod]
        public void Ctor_WithNullColumnName_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlColumn(null));
        }

        [TestMethod]
        public void Ctor_WithEmptyColumnName_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlColumn(string.Empty));
        }

        [TestMethod]
        public void Ctor_WithWhiteSpaceColumnName_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlColumn("\t"));
        }

        [TestMethod]
        public void Ctor_WithColumnName_SetsColumnNameProperty()
        {
            SqlColumn column = new SqlColumn("Id");
            Assert.AreEqual("Id", column.ColumnName);
        }

        [TestMethod]
        public void Ctor_WithColumnNameAndAlias_SetsProperties()
        {
            SqlColumn column = new SqlColumn("Id", "i");
            Assert.AreEqual("Id", column.ColumnName);
            Assert.AreEqual("i", column.Alias);
        }

        [TestMethod]
        public void Ctor_WithTableNameColumnNameAndAlias_SetsProperties()
        {
            SqlColumn column = new SqlColumn("Users", "Id", "i");
            Assert.AreEqual("Id", column.ColumnName);
            Assert.AreEqual("i", column.Alias);
            Assert.AreEqual("Users", column.TableName);
        }

        [TestMethod]
        public void ImplicitCast_FromString_SetsColumnNameProperty()
        {
            SqlColumn column = "Id";
            Assert.AreEqual("Id", column.ColumnName);
        }
    }
}
