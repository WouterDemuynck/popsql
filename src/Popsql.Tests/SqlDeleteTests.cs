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
    public class SqlDeleteTests
    {
        [TestMethod]
        public void From_WithNullSqlTable_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlDelete().From(null));
        }

        [TestMethod]
        public void From_WithSqlTable_SetsTargetProperty()
        {
            var delete = new SqlDelete().From("Users");
            Assert.IsNotNull(delete.Table);
            Assert.AreEqual("Users", delete.Table.TableName);
        }
    }
}
