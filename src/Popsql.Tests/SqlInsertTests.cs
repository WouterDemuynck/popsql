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
    public class SqlInsertTests
    {
        [TestMethod]
        public void Into_WithNullSqlTable_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlInsert().Into(null));
        }

        [TestMethod]
        public void Into_WithSqlTable_SetsTargetProperty()
        {
            var insert = new SqlInsert().Into("Users");

            Assert.IsNotNull(insert.Table);
            Assert.AreEqual("Users", insert.Table.TableName);
        }

        [TestMethod]
        public void Values_WithNullValues_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlInsert().Values(null));
        }

        [TestMethod]
        public void Values_WithEmptyValues_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlInsert().Values(new SqlValue[0]));
        }

        [TestMethod]
        public void Values_WithValues_AddsValuesToProperty()
        {
            var insert = new SqlInsert().Values(5, "String", 5.5f);
            Assert.IsNotNull(insert.Rows);
            Assert.AreEqual(1, insert.Rows.Count());
            Assert.IsNotNull(insert.Rows.First());
            Assert.AreEqual(3, insert.Rows.First().Count());
            Assert.AreEqual(5, insert.Rows.First().Cast<SqlConstant>().Select(v => v.Value).First());
            Assert.AreEqual("String", insert.Rows.First().Cast<SqlConstant>().Select(v => v.Value).Skip(1).First());
            Assert.AreEqual(5.5f, insert.Rows.First().Cast<SqlConstant>().Select(v => v.Value).Last());

            insert.Values(10, "AnotherString", 3.14f);
            Assert.AreEqual(2, insert.Rows.Count());
            Assert.IsNotNull(insert.Rows.Last());
            Assert.AreEqual(3, insert.Rows.Last().Count());
            Assert.AreEqual(10, insert.Rows.Last().Cast<SqlConstant>().Select(v => v.Value).First());
            Assert.AreEqual("AnotherString", insert.Rows.Last().Cast<SqlConstant>().Select(v => v.Value).Skip(1).First());
            Assert.AreEqual(3.14f, insert.Rows.Last().Cast<SqlConstant>().Select(v => v.Value).Last());
        }
    }
}
