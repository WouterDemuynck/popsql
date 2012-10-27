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

            Assert.IsNotNull(insert.Target);
            Assert.AreEqual("Users", insert.Target.TableName);
        }
    }
}
