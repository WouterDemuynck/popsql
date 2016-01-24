using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlSelectTests
	{
		[Fact]
		public void Ctor_WithNullColumns_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlSelect(null));
		}

		[Fact]
		public void Ctor_WithColumns_SetsColumnsProperty()
		{
			var columns = new List<SqlColumn>();
			columns.Add("Id");
			columns.Add("Name");
			columns.Add("Email");
			var query = new SqlSelect(columns);

			Assert.NotNull(query.Columns);
			Assert.Equal(3, query.Columns.Count());
			Assert.Equal("Id", query.Columns.First().ColumnName.Segments[0]);
			Assert.Equal("Name", query.Columns.Skip(1).First().ColumnName.Segments[0]);
			Assert.Equal("Email", query.Columns.Last().ColumnName.Segments[0]);
		}

		[Fact]
		public void From_WithNullSqlTable_ThrowsArgumentNull()
		{
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
			Assert.Throws<ArgumentNullException>(() => select.From(null));
		}


		[Fact]
		public void From_WithSqlTable_SetsTableProperty()
		{
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
			select.From("Users");

			Assert.NotNull(select.Table);
			Assert.Equal("Users", select.Table.TableName.Segments.First());
		}

		[Fact]
		public void Where_WithSqlExpression_SetsPredicateProperty()
		{
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
			select.Where(SqlExpression.Equal("Id", 5));

			Assert.NotNull(select.Predicate);
			Assert.IsType<SqlBinaryExpression>(select.Predicate);
			Assert.IsType<SqlColumn>(((SqlBinaryExpression)select.Predicate).Left);
			Assert.Equal("Id", ((SqlColumn)((SqlBinaryExpression)select.Predicate).Left).ColumnName.Segments.First());

			Assert.Equal(SqlBinaryOperator.Equal, ((SqlBinaryExpression)select.Predicate).Operator);

			Assert.IsType<SqlConstant>(((SqlBinaryExpression)select.Predicate).Right);
			Assert.Equal(5, ((SqlConstant)((SqlBinaryExpression)select.Predicate).Right).Value);
		}

		[Fact]
		public void Join_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.Join(null, SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void Join_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users)
				.Join(profiles, predicate);

			Assert.Equal(1, select.Joins.Count());
			var join = select.Joins.First();

			Assert.Same(profiles, join.Table);
			Assert.Same(predicate, join.Predicate);
			Assert.Equal(SqlJoinType.Default, join.Type);
		}

		[Fact]
		public void InnerJoin_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.InnerJoin(null, SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void InnerJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users)
				.InnerJoin(profiles, predicate);

			Assert.Equal(1, select.Joins.Count());
			var join = select.Joins.First();

			Assert.Same(profiles, join.Table);
			Assert.Same(predicate, join.Predicate);
			Assert.Equal(SqlJoinType.Inner, join.Type);
		}

		[Fact]
		public void LeftJoin_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.LeftJoin(null, SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void LeftJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users)
				.LeftJoin(profiles, predicate);

			Assert.Equal(1, select.Joins.Count());
			var join = select.Joins.First();

			Assert.Same(profiles, join.Table);
			Assert.Same(predicate, join.Predicate);
			Assert.Equal(SqlJoinType.Left, join.Type);
		}

		[Fact]
		public void RightJoin_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.RightJoin(null, SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void RightJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users)
				.RightJoin(profiles, predicate);

			Assert.Equal(1, select.Joins.Count());
			var join = select.Joins.First();

			Assert.Same(profiles, join.Table);
			Assert.Same(predicate, join.Predicate);
			Assert.Equal(SqlJoinType.Right, join.Type);
		}

		[Fact]
		public void Join_WithMultipleJoins_AddsAllJoins()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var addresses = new SqlTable("dbo.Addresses", "a");
			var profilesPredicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			var addressesPredicate = SqlExpression.Equal(users + "Id", addresses + "UserId");
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" })
				.From(users)
				.LeftJoin(profiles, profilesPredicate)
				.LeftJoin(addresses, addressesPredicate);

			Assert.Equal(2, select.Joins.Count());
			var join = select.Joins.First();
			Assert.Same(profiles, join.Table);
			Assert.Same(profilesPredicate, join.Predicate);
			Assert.Equal(SqlJoinType.Left, join.Type);

			join = select.Joins.Last();
			Assert.Same(addresses, join.Table);
			Assert.Same(addressesPredicate, join.Predicate);
			Assert.Equal(SqlJoinType.Left, join.Type);
		}

		[Fact]
		public void Joins_IsNotNullWhenFirstCalled()
		{
			var select = new SqlSelect(new SqlColumn[] { "Id" });
			Assert.NotNull(select.Joins);
		}

		[Fact]
		public void OrderBy_WithNullColumn_ThrowsArgumentNull()
		{
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });

			Assert.Throws<ArgumentNullException>(() => select.OrderBy(null));
		}

		[Fact]
		public void OrderBy_WithColumnAndSortOrder_AddsSorting()
		{
			SqlColumn column = "dbo.Users.CreatedOn";
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
			select.OrderBy(column, SqlSortOrder.Descending);

			Assert.Equal(1, select.Sorting.Count);
			var sort = select.Sorting.First();
			Assert.Same(column, sort.Column);
			Assert.Same(SqlSortOrder.Descending, sort.SortOrder);
		}

		[Fact]
		public void Sorting_IsNotNullWhenFirstCalled()
		{
			var select = new SqlSelect(new SqlColumn[] { "Id" });
			Assert.NotNull(select.Sorting);
		}

		[Fact]
		public void ExpressionType_ReturnsSelect()
		{
			var query = new SqlSelect(new SqlColumn[] { "Id" });

			Assert.Equal(SqlExpressionType.Select, query.ExpressionType);
		}
	}
}
