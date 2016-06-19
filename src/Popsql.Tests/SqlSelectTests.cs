using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Popsql.Visitors;
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
			var columns = new List<SqlColumn> { "Id", "Name", "Email" };
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
			Assert.Throws<ArgumentNullException>(() => select.From((SqlTable)null));
		}

		[Fact]
		public void From_WithNullSubquery_ThrowsArgumentNull()
		{
			var select = Sql.Select("Id", "Name");
			Assert.Throws<ArgumentNullException>(() => select.From((SqlSubquery)null));
		}

		[Fact]
		public void From_WithNullSubquery_ThrowsArgument()
		{
			var select = Sql.Select("Id", "Name");
			var subquery = new SqlSubquery(Sql.Select("Id", "Name").From("User").Go());
			Assert.Throws<ArgumentException>(() => select.From(subquery));
		}

		[Fact]
		public void From_WithTable_SetsFromProperty()
		{
			var select = Sql
				.Select("Id", "Name")
				.From("Users")
				.Go();

			Assert.NotNull(select.From);
			Assert.IsType<SqlTable>(select.From.Table);
			Assert.Equal("Users", ((SqlTable)select.From.Table).TableName.Segments.Single());
		}


		[Fact]
		public void From_WithSubQuery_SetsFromProperty()
		{
			var subquery = Sql
				.Select("Id", "Name")
				.From("Users")
				.Go() + "u";

			var select = Sql
				.Select("Id", "Name")
				.From(subquery)
				.Go();

			Assert.NotNull(select.From);
			Assert.NotNull(select.From.Table);
			Assert.IsType<SqlSubquery>(select.From.Table);
			Assert.Same(subquery, select.From.Table);
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
			Assert.NotNull(select.Where.Predicate);
			Assert.IsType<SqlBinaryExpression>(select.Where.Predicate);
			Assert.IsType<SqlColumn>(((SqlBinaryExpression)select.Where.Predicate).Left);
			Assert.Equal("Id", ((SqlColumn)((SqlBinaryExpression)select.Where.Predicate).Left).ColumnName.Segments.First());

			Assert.Equal(SqlBinaryOperator.Equal, ((SqlBinaryExpression)select.Where.Predicate).Operator);

			Assert.IsType<SqlConstant>(((SqlBinaryExpression)select.Where.Predicate).Right);
			Assert.Equal(5, ((SqlConstant)((SqlBinaryExpression)select.Where.Predicate).Right).Value);
		}

		[Fact]
		public void Join_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var select = Sql
				.Select("Id", "Name")
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.Join(null).On(SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void Join_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			SqlSelect select = Sql.Select("Id", "Name")
				.From(users)
				.Join(profiles)
				.On(predicate)
				.Go();

			Assert.Equal(1, select.From.Joins.Count());
			var join = select.From.Joins.First();

			Assert.Same(profiles, join.Table);
			Assert.NotNull(join.On);
			Assert.NotNull(join.On.Predicate);
			Assert.Same(predicate, join.On.Predicate);
			Assert.Equal(SqlJoinType.Default, join.Type);
		}

		[Fact]
		public void InnerJoin_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var select = Sql.Select("Id", "Name")
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.InnerJoin(null).On(SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void InnerJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			var select = Sql.Select("Id", "Name")
				.From(users)
				.InnerJoin(profiles)
				.On(predicate)
				.Go();

			Assert.Equal(1, select.From.Joins.Count());
			var join = select.From.Joins.First();

			Assert.Same(profiles, join.Table);
			Assert.Same(predicate, join.On.Predicate);
			Assert.Equal(SqlJoinType.Inner, join.Type);
		}

		[Fact]
		public void LeftJoin_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var select = Sql.Select("Id", "Name")
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.LeftJoin(null).On(SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void LeftJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			var select = Sql.Select("Id", "Name")
				.From(users)
				.LeftJoin(profiles)
				.On(predicate)
				.Go();

			Assert.Equal(1, select.From.Joins.Count());
			var join = select.From.Joins.First();

			Assert.Same(profiles, join.Table);
			Assert.NotNull(join.On);
			Assert.NotNull(join.On.Predicate);
			Assert.Same(predicate, join.On.Predicate);
			Assert.Equal(SqlJoinType.Left, join.Type);
		}

		[Fact]
		public void RightJoin_WithNullTable_ThrowsArgumentNull()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var select = Sql.Select("Id", "Name")
				.From(users);

			Assert.Throws<ArgumentNullException>(() => select.RightJoin(null).On(SqlExpression.Equal(users + "Id", profiles + "UserId")));
		}

		[Fact]
		public void RightJoin_WithTableAndPredicate_AddsJoin()
		{
			var users = new SqlTable("dbo.Users", "u");
			var profiles = new SqlTable("dbo.Profiles", "p");
			var predicate = SqlExpression.Equal(users + "Id", profiles + "UserId");
			SqlSelect select = Sql.Select("Id", "Name")
				.From(users)
				.RightJoin(profiles)
				.On(predicate)
				.Go();

			Assert.Equal(1, select.From.Joins.Count());
			var join = select.From.Joins.First();

			Assert.Same(profiles, join.Table);
			Assert.NotNull(join.On);
			Assert.NotNull(join.On.Predicate);
			Assert.Same(predicate, join.On.Predicate);
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
				.LeftJoin(profiles).On(profilesPredicate)
				.LeftJoin(addresses).On(addressesPredicate)
				.Go();

			Assert.Equal(2, select.From.Joins.Count());
			var join = select.From.Joins.First();
			Assert.Same(profiles, join.Table);
			Assert.NotNull(join.On);
			Assert.NotNull(join.On.Predicate);
			Assert.Same(profilesPredicate, join.On.Predicate);
			Assert.Equal(SqlJoinType.Left, join.Type);

			join = select.From.Joins.Last();
			Assert.Same(addresses, join.Table);
			Assert.NotNull(join.On);
			Assert.NotNull(join.On.Predicate);
			Assert.Same(addressesPredicate, join.On.Predicate);
			Assert.Equal(SqlJoinType.Left, join.Type);
		}

		[Fact]
		public void Joins_IsNotNullWhenFirstCalled()
		{
			var select = Sql.Select("Id").From("User").Go();
			Assert.NotNull(select.From.Joins);
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
		public void OrderBy_WithColumnAndSortOrder_AddsOrderBy()
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
		public void OrderBy_WithSortExpression_AddsOrderBy()
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
		public void OrderBy_WithWhereClauesAndWithColumnAndSortOrder_AddsOrderBy()
		{
			SqlColumn column = "dbo.Users.CreatedOn";
			SqlSelect select = Sql
				.Select("Id", "Name")
				.From("User")
				.Where(SqlExpression.Equal("Id", 8))
				.OrderBy(column, SqlSortOrder.Descending)
				.Go();

			Assert.Equal(1, select.OrderBy.Count);
			var sort = select.OrderBy.First();
			Assert.Same(column, sort.Column);
			Assert.Equal(SqlSortOrder.Descending, sort.SortOrder);
		}

		[Fact]
		public void OrderBy_WithWhereClauesAndSortExpression_AddsSorting()
		{
			SqlColumn column = "dbo.Users.CreatedOn";
			SqlSelect select = Sql
				.Select("Id", "Name")
				.From("User")
				.Where(SqlExpression.Equal("Id", 8))
				.OrderBy(column + SqlSortOrder.Descending)
				.Go();

			Assert.Equal(1, select.OrderBy.Count);
			var sort = select.OrderBy.First();
			Assert.Same(column, sort.Column);
			Assert.Equal(SqlSortOrder.Descending, sort.SortOrder);
		}

		[Fact]
		public void OrderBy_IsNotNullWhenFirstCalled()
		{
			var select = new SqlSelect(new SqlColumn[] { "Id" });
			Assert.NotNull(select.OrderBy);
		}

		[Fact]
		public void Limit_WithNegativeOffset_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(
				() => Sql
					.Select("Id", "Name")
					.From("User")
					.OrderBy("Name")
					.Limit(-1, 10));
		}

		[Fact]
		public void Limit_WithCountLessThanOne_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(
				() => Sql
					.Select("Id", "Name")
					.From("User")
					.OrderBy("Name")
					.Limit(0, 0));

			Assert.Throws<ArgumentException>(
				() => Sql
					.Select("Id", "Name")
					.From("User")
					.OrderBy("Name")
					.Limit(0));
		}

		[Fact]
		public void Limit_WithCount_SetsCount()
		{
			var select = Sql
				.Select("Id", "Name")
				.From("User")
				.OrderBy("Name")
				.Limit(42)
				.Go();

			Assert.NotNull(select.Limit);
			Assert.Equal(42, select.Limit.Count);
		}

		[Fact]
		public void Limit_WithOffsetAndCount_SetsOffsetAndCount()
		{
			var select = Sql
				.Select("Id", "Name")
				.From("User")
				.OrderBy("Name")
				.Limit(42, 25)
				.Go();

			Assert.NotNull(select.Limit);
			Assert.Equal(42, select.Limit.Offset);
			Assert.Equal(25, select.Limit.Count);
		}

		[Fact]
		public void GroupBy_Limit_WithNegativeOffset_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(
				() => Sql
					.Select("Id", "Name")
					.From("User")
					.GroupBy("Realm")
					.OrderBy("Name")
					.Limit(-1, 1));
		}

		[Fact]
		public void GroupBy_Limit_WithOffsetAndCount_SetsOffsetAndCount()
		{
			var select = Sql
				.Select("Id", "Name")
				.From("User")
				.GroupBy("Realm")
				.OrderBy("Name")
				.Limit(42, 25)
				.Go();

			Assert.NotNull(select.Limit);
			Assert.Equal(42, select.Limit.Offset);
			Assert.Equal(25, select.Limit.Count);
		}

		[Fact]
		public void GroupBy_Limit_WithCount_SetsCount()
		{
			var select = Sql
				.Select("Id", "Name")
				.From("User")
				.GroupBy("Realm")
				.OrderBy("Name")
				.Limit(25)
				.Go();

			Assert.NotNull(select.Limit);
			Assert.Null(select.Limit.Offset);
			Assert.Equal(25, select.Limit.Count);
		}

		[Fact]
		public void GroupBy_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(
				() =>
				{
					Sql
						.Select("FirstName")
						.From("User")
						.GroupBy(null)
						.Go();
				});
		}

		[Fact]
		public void GroupBy_WithColumn_AddsGrouping()
		{
			var select = Sql
				.Select("FirstName")
				.From("User")
				.GroupBy("FirstName")
				.Go();

			Assert.NotNull(select.GroupBy);
			Assert.Equal(1, select.GroupBy.Count);
			Assert.Equal(((SqlColumn)"FirstName").ColumnName, select.GroupBy.First().ColumnName);
		}

		[Fact]
		public void GroupBy_WithWhereClauseAndColumn_AddsGrouping()
		{
			var select = Sql
				.Select("FirstName")
				.From("User")
				.Where(SqlExpression.Equal("Age", 18))
				.GroupBy("FirstName")
				.Go();

			Assert.NotNull(select.GroupBy);
			Assert.Equal(1, select.GroupBy.Count);
			Assert.Equal(((SqlColumn)"FirstName").ColumnName, select.GroupBy.First().ColumnName);
		}

		[Fact]
		public void GroupBy_WithOrderByWithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				Sql
					.Select("FirstName")
					.From("User")
					.GroupBy("FirstName")
					.OrderBy(null, SqlSortOrder.Descending)
					.Go();
			});
		}
		[Fact]
		public void GroupBy_WithOrderByWithNullSortExpression_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				Sql
					.Select("FirstName")
					.From("User")
					.GroupBy("FirstName")
					.OrderBy((SqlSort)null)
					.Go();
			});
		}

		[Fact]
		public void GroupBy_WithOrderBy_AddsSorting()
		{
			var column = (SqlColumn)"Age";
			var select = Sql
				.Select("FirstName")
				.From("User")
				.GroupBy("FirstName")
				.OrderBy(column, SqlSortOrder.Descending)
				.Go();

			Assert.NotNull(select.OrderBy);
			Assert.Equal(1, select.OrderBy.Count);
			var sort = select.OrderBy.First();
			Assert.Same(column, sort.Column);
			Assert.Equal(SqlSortOrder.Descending, sort.SortOrder);
		}

		[Fact]
		public void GroupBy_WithOrderByWithSortExpression_AddsSorting()
		{
			var column = (SqlColumn)"Age";
			var select = Sql
				.Select("FirstName")
				.From("User")
				.GroupBy("FirstName")
				.OrderBy(new SqlSort(column, SqlSortOrder.Descending))
				.Go();

			Assert.NotNull(select.OrderBy);
			Assert.Equal(1, select.OrderBy.Count);
			var sort = select.OrderBy.First();
			Assert.Same(column, sort.Column);
			Assert.Equal(SqlSortOrder.Descending, sort.SortOrder);
		}

		[Fact]
		public void Having_WithNullPredicate_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(
				() =>
				{
					Sql
						.Select("FirstName")
						.From("User")
						.GroupBy("FirstName")
						.Having(null)
						.Go();
				});
		}

		[Fact]
		public void Having_WithPredicate_AddsHaving()
		{
			var predicate = SqlExpression.Equal("Age", 42);
			var select = Sql
				.Select("FirstName")
				.From("User")
				.GroupBy("FirstName")
				.Having(predicate)
				.Go();

			Assert.NotNull(select.GroupBy);
			Assert.NotNull(select.GroupBy.Having);
			Assert.NotNull(select.GroupBy.Having.Predicate);
			Assert.Same(predicate, select.GroupBy.Having.Predicate);
		}

		[Fact]
		public void ExpressionType_ReturnsSelect()
		{
			var query = new SqlSelect(new SqlColumn[] { "Id" });

			Assert.Equal(SqlExpressionType.Select, query.ExpressionType);
		}

		[Fact]
		public void Accept_WithoutWhere_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = Sql
				.Select("Id", "Name")
				.From("User")
				.Go();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlSelect>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlFrom>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlTable>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlColumn>()), Times.Exactly(2));
			mock.Verify(_ => _.Visit(It.IsAny<SqlWhere>()), Times.Never);
			mock.Verify(_ => _.Visit(It.IsAny<SqlJoin>()), Times.Never);
			mock.Verify(_ => _.Visit(It.IsAny<SqlOrderBy>()), Times.Never);
		}

		[Fact]
		public void Accept_WithLimit_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = Sql
				.Select("Id", "Name")
				.From("User")
				.OrderBy("Name")
				.Limit(42, 10)
				.Go();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlLimit>()), Times.Once);
		}

		[Fact]
		public void Accept_WithLimitWithCountOnly_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = Sql
				.Select("Id", "Name")
				.From("User")
				.OrderBy("Name")
				.Limit(10)
				.Go();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlLimit>()), Times.Once);
		}
	}
}
