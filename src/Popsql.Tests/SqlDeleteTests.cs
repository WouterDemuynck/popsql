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

        [TestMethod]
        public void Where_WithSqlExpression_SetsPredicateProperty()
        {
            SqlDelete delete = new SqlDelete();
            delete.Where(SqlExpression.Equal("Id", 5));

            Assert.IsNotNull(delete.Predicate);
            Assert.IsInstanceOfType(delete.Predicate, typeof(SqlBinaryExpression));
            Assert.IsInstanceOfType(((SqlBinaryExpression)delete.Predicate).Left, typeof(SqlColumn));
            Assert.AreEqual("Id", ((SqlColumn)((SqlBinaryExpression)delete.Predicate).Left).ColumnName);

            Assert.AreEqual(SqlBinaryOperator.Equal, ((SqlBinaryExpression)delete.Predicate).Operator);

            Assert.IsInstanceOfType(((SqlBinaryExpression)delete.Predicate).Right, typeof(SqlConstant));
            Assert.AreEqual(5, ((SqlConstant)((SqlBinaryExpression)delete.Predicate).Right).Value);
        }
    }
}
