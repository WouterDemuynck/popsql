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
    public class SqlUpdateTests
    {
        [TestMethod]
        public void Ctor_WithNullTable_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlUpdate(null));
        }

        [TestMethod]
        public void Ctor_WithSqlTable_SetsTargetProperty()
        {
            var update = new SqlUpdate("Users");
            Assert.IsNotNull(update.Table);
            Assert.AreEqual("Users", update.Table.TableName);
        }

        [TestMethod]
        public void Set_WithNullColumn_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlUpdate("Users").Set(null, null));
        }

        [TestMethod]
        public void Values_WithNullValue_AutomaticallyConvertsToSqlConstantNull()
        {
            var update = new SqlUpdate("Users").Set("Id", null);
            Assert.IsNotNull(update.Values);
            Assert.AreEqual(1, update.Values.Count());
            Assert.AreSame(SqlConstant.Null, update.Values.First().Value);
            Assert.AreEqual("Id", update.Values.First().Column.ColumnName);
        }

        [TestMethod]
        public void Values_WithNullValue_AddsToValuesProperty()
        {
            var update = new SqlUpdate("Users").Set("Id", "Test" + (SqlConstant)5.0f);
            Assert.IsNotNull(update.Values);
            Assert.AreEqual(1, update.Values.Count());
            Assert.AreEqual("Id", update.Values.First().Column.ColumnName);
            Assert.IsInstanceOfType(update.Values.First().Value, typeof(SqlParameter));
            Assert.AreEqual("Test", ((SqlParameter)update.Values.First().Value).ParameterName);
            Assert.AreEqual(5.0f, ((SqlParameter)update.Values.First().Value).Value);
        }

        [TestMethod]
        public void Where_WithSqlExpression_SetsPredicateProperty()
        {
            SqlUpdate update = new SqlUpdate("Users");
            update.Where(SqlExpression.Equal("Id", 5));

            Assert.IsNotNull(update.Predicate);
            Assert.IsInstanceOfType(update.Predicate, typeof(SqlBinaryExpression));
            Assert.IsInstanceOfType(((SqlBinaryExpression)update.Predicate).Left, typeof(SqlColumn));
            Assert.AreEqual("Id", ((SqlColumn)((SqlBinaryExpression)update.Predicate).Left).ColumnName);

            Assert.AreEqual(SqlBinaryOperator.Equal, ((SqlBinaryExpression)update.Predicate).Operator);

            Assert.IsInstanceOfType(((SqlBinaryExpression)update.Predicate).Right, typeof(SqlConstant));
            Assert.AreEqual(5, ((SqlConstant)((SqlBinaryExpression)update.Predicate).Right).Value);
        }
    }
}
