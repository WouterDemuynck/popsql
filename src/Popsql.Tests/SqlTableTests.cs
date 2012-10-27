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
    public class SqlTableTests
    {
        [TestMethod]
        public void Ctor_WithNullTableName_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlTable(null));
        }

        [TestMethod]
        public void Ctor_WithEmptyTableName_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlTable(string.Empty));
        }

        [TestMethod]
        public void Ctor_WithWhiteSpaceTableName_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlTable("\t"));
        }

        [TestMethod]
        public void Ctor_WithTableName_SetsTableNameProperty()
        {
            SqlTable table = new SqlTable("Users");
            Assert.AreEqual("Users", table.TableName);
        }

        [TestMethod]
        public void Ctor_WithTableNameAndAlias_SetsProperties()
        {
            SqlTable table = new SqlTable("Users", "u");
            Assert.AreEqual("Users", table.TableName);
            Assert.AreEqual("u", table.Alias);
        }

        [TestMethod]
        public void ImplicitCast_FromString_SetsTableNameProperty()
        {
            SqlTable table = "Users";
            Assert.AreEqual("Users", table.TableName);
        }
    }
}
