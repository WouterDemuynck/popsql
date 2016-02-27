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

			Assert.NotNull(query.Select);
			Assert.Equal(3, query.Select.Count());
			Assert.Equal("Id", query.Select.Cast<SqlColumn>().First().ColumnName.Segments[0]);
			Assert.Equal("Name", query.Select.Cast<SqlColumn>().Skip(1).First().ColumnName.Segments[0]);
			Assert.Equal("Email", query.Select.Cast<SqlColumn>().Last().ColumnName.Segments[0]);
		}

		[Fact]
		public void From_WithNullTable_ThrowsArgumentNull()
		{
			var select = Sql.Select("Id", "Name");
			Assert.Throws<ArgumentNullException>(() => select.From(null));
		}


		[Fact]
		public void From_WithTable_SetsTableProperty()
		{
			var select = Sql
				.Select("Id", "Name")
				.From("Users")
				.Go();

			Assert.NotNull(select.From);
			Assert.Equal("Users", select.From.TableName.Segments.First());
		}

		[Fact]
		public void Where_WithExpression_SetsWhereProperty()
		{
			var select = Sql
				.Select("Id", "Name")
				.From("User")
				.Where(SqlExpression.Equal("Id", 5))
				.Go();

			Assert.NotNull(select.Where);
			Assert.IsType<SqlBinaryExpression>(select.Where);
			Assert.IsType<SqlColumn>(((SqlBinaryExpression)select.Where).Left);
			Assert.Equal("Id", ((SqlColumn)((SqlBinaryExpression)select.Where).Left).ColumnName.Segments.First());

			Assert.Equal(SqlBinaryOperator.Equal, ((SqlBinaryExpression)select.Where).Operator);

			Assert.IsType<SqlConstant>(((SqlBinaryExpression)select.Where).Right);
			Assert.Equal(5, ((SqlConstant)((SqlBinaryExpression)select.Where).Right).Value);
		}

		[Fact]
		public void Join_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var select = Sql
				.Select("Id", "Name")
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.Join(null, SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void Join_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			SqlSelect select = Sql.Select("Id", "Name")
				.From(users)
				.Join(profiles, predicate)
				.Go();

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
			var select = Sql.Select("Id", "Name")
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.InnerJoin(null, SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void InnerJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			var select = Sql.Select("Id", "Name")
				.From(users)
				.InnerJoin(profiles, predicate)
				.Go();

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
			var select = Sql.Select("Id", "Name")
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.LeftJoin(null, SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void LeftJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			var select = Sql.Select("Id", "Name")
				.From(users)
				.LeftJoin(profiles, predicate)
				.Go();

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
			var select = Sql.Select("Id", "Name")
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.RightJoin(null, SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void RightJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			SqlSelect select = Sql.Select("Id", "Name")
				.From(users)
				.RightJoin(profiles, predicate)
				.Go();

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
			var select = Sql.Select("Id", "Name")
				.From(users)
				.LeftJoin(profiles, profilesPredicate)
				.LeftJoin(addresses, addressesPredicate)
				.Go();

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
			var select = Sql
				.Select("Id", "Name")
				.From("User");

			Assert.Throws<ArgumentNullException>(() => select.OrderBy((SqlColumn)null));
		}

		[Fact]
		public void OrderBy_WithNullSorting_ThrowsArgumentNull()
		{
			var select = Sql.Select("Id", "Name").From("User");

			Assert.Throws<ArgumentNullException>(() => select.OrderBy(null));
		}

		[Fact]
		public void OrderBy_WithColumnAndSortOrder_AddsSorting()
		{
			SqlColumn column = "dbo.Users.CreatedOn";
			SqlSelect select = Sql
				.Select("Id", "Name")
				.From("User")
				.OrderBy(column, SqlSortOrder.Descending)
				.Go();

			Assert.Equal(1, select.OrderBy.Count);
			var sort = select.OrderBy.First();
			Assert.Same(column, sort.Column);
			Assert.Equal(SqlSortOrder.Descending, sort.SortOrder);
		}
		[Fact]
		public void OrderBy_WithSortExpression_AddsSorting()
		{
			SqlColumn column = "dbo.Users.CreatedOn";
			SqlSelect select = Sql
				.Select("Id", "Name")
				.From("User")
				.OrderBy(column + SqlSortOrder.Descending)
				.Go();

			Assert.Equal(1, select.OrderBy.Count);
			var sort = select.OrderBy.First();
			Assert.Same(column, sort.Column);
			Assert.Equal(SqlSortOrder.Descending, sort.SortOrder);
		}

		[Fact]
		public void Sorting_IsNotNullWhenFirstCalled()
		{
			var select = new SqlSelect(new SqlColumn[] { "Id" });
			Assert.NotNull(select.OrderBy);
		}

		[Fact]
		public void ExpressionType_ReturnsSelect()
		{
			var query = new SqlSelect(new SqlColumn[] { "Id" });

			Assert.Equal(SqlExpressionType.Select, query.ExpressionType);
		}
	}
}
