﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void From_WithNullSqlTable_ThrowsArgumentNull()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
            AssertEx.Throws<ArgumentNullException>(() => select.From(null));
        }

        [TestMethod]
        public void From_WithSqlTable_SetsTableProperty()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
            select.From("Users");

            Assert.IsNotNull(select.Table);
            Assert.AreEqual("Users", select.Table.TableName);
        }

        [TestMethod]
        public void Where_WithSqlExpression_SetsPredicateProperty()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
            select.Where(SqlExpression.Equal("Id", 5));

            Assert.IsNotNull(select.Predicate);
            Assert.IsInstanceOfType(select.Predicate, typeof(SqlBinaryExpression));
            Assert.IsInstanceOfType(((SqlBinaryExpression)select.Predicate).Left, typeof(SqlColumn));
            Assert.AreEqual("Id", ((SqlColumn)((SqlBinaryExpression)select.Predicate).Left).ColumnName);

            Assert.AreEqual(SqlBinaryOperator.Equal, ((SqlBinaryExpression)select.Predicate).Operator);

            Assert.IsInstanceOfType(((SqlBinaryExpression)select.Predicate).Right, typeof(SqlConstant));
            Assert.AreEqual(5, ((SqlConstant)((SqlBinaryExpression)select.Predicate).Right).Value);
        }

        [TestMethod]
        public void OrderBy_WithNullColumn_ThrowsArgumentNull()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
            AssertEx.Throws<ArgumentNullException>(() => select.OrderBy(null, SqlSortOrder.Ascending));
        }

        [TestMethod]
        public void OrderBy_WithColumnAndSortOrder_SetsSortingProperty()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name", "Email" });
            select.OrderBy((SqlColumn)"Id" + SqlSortOrder.Descending);
            select.OrderBy("Name", SqlSortOrder.Ascending);
            select.OrderBy("Email");

            Assert.AreEqual(3, select.Sorting.Count());
            Assert.AreEqual("Id", select.Sorting.First().Column.ColumnName);
            Assert.AreEqual(SqlSortOrder.Descending, select.Sorting.First().SortOrder);
            Assert.AreEqual("Name", select.Sorting.Skip(1).First().Column.ColumnName);
            Assert.AreEqual(SqlSortOrder.Ascending, select.Sorting.Skip(1).First().SortOrder);
            Assert.AreEqual("Email", select.Sorting.Last().Column.ColumnName);
            Assert.AreEqual(SqlSortOrder.Ascending, select.Sorting.Last().SortOrder);
        }

        [TestMethod]
        public void Join_WithNullTable_ThrowsArgumentNull()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name", "Email" });
            AssertEx.Throws<ArgumentNullException>(() => select.Join(null));
        }

        [TestMethod]
        public void InnerJoin_WithNullTable_ThrowsArgumentNull()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name", "Email" });
            AssertEx.Throws<ArgumentNullException>(() => select.InnerJoin(null));
        }

        [TestMethod]
        public void LeftJoin_WithNullTable_ThrowsArgumentNull()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name", "Email" });
            AssertEx.Throws<ArgumentNullException>(() => select.LeftJoin(null));
        }

        [TestMethod]
        public void RightJoin_WithNullTable_ThrowsArgumentNull()
        {
            SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name", "Email" });
            AssertEx.Throws<ArgumentNullException>(() => select.RightJoin(null));
        }

        [TestMethod]
        public void Join_WithTable_AddsJoin()
        {
            SqlTable foo = new SqlTable("Foo", "f");
            SqlTable bar = new SqlTable("Bar", "b");
            SqlSelect select = Sql
                .Select(foo + "Id", bar + "Name")
                .From(foo)
                .Join(bar, SqlExpression.Equal(foo + "Id", bar + "FooId"));

            Assert.IsNotNull(select.Joins);
            Assert.AreEqual(1, select.Joins.Count());
            Assert.AreEqual(SqlJoinType.Default, select.Joins.First().Type);
            Assert.AreEqual("Bar", select.Joins.First().Table.TableName);
            Assert.IsNotNull(select.Joins.First().Predicate);
        }

        [TestMethod]
        public void InnerJoin_WithTable_AddsJoin()
        {
            SqlTable foo = new SqlTable("Foo", "f");
            SqlTable bar = new SqlTable("Bar", "b");
            SqlSelect select = Sql
                .Select(foo + "Id", bar + "Name")
                .From(foo)
                .InnerJoin(bar, SqlExpression.Equal(foo + "Id", bar + "FooId"));

            Assert.IsNotNull(select.Joins);
            Assert.AreEqual(1, select.Joins.Count());
            Assert.AreEqual(SqlJoinType.Inner, select.Joins.First().Type);
            Assert.AreEqual("Bar", select.Joins.First().Table.TableName);
            Assert.IsNotNull(select.Joins.First().Predicate);
        }

        [TestMethod]
        public void LeftJoin_WithTable_AddsJoin()
        {
            SqlTable foo = new SqlTable("Foo", "f");
            SqlTable bar = new SqlTable("Bar", "b");
            SqlSelect select = Sql
                .Select(foo + "Id", bar + "Name")
                .From(foo)
                .LeftJoin(bar, SqlExpression.Equal(foo + "Id", bar + "FooId"));

            Assert.IsNotNull(select.Joins);
            Assert.AreEqual(1, select.Joins.Count());
            Assert.AreEqual(SqlJoinType.Left, select.Joins.First().Type);
            Assert.AreEqual("Bar", select.Joins.First().Table.TableName);
            Assert.IsNotNull(select.Joins.First().Predicate);
        }

        [TestMethod]
        public void RightJoin_WithTable_AddsJoin()
        {
            SqlTable foo = new SqlTable("Foo", "f");
            SqlTable bar = new SqlTable("Bar", "b");
            SqlSelect select = Sql
                .Select(foo + "Id", bar + "Name")
                .From(foo)
                .RightJoin(bar, SqlExpression.Equal(foo + "Id", bar + "FooId"));

            Assert.IsNotNull(select.Joins);
            Assert.AreEqual(1, select.Joins.Count());
            Assert.AreEqual(SqlJoinType.Right, select.Joins.First().Type);
            Assert.AreEqual("Bar", select.Joins.First().Table.TableName);
            Assert.IsNotNull(select.Joins.First().Predicate);
        }
    }
}
