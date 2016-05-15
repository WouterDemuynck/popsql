using System;
using Popsql.Grammar;
using Xunit;

namespace Popsql.Tests
{
	public class ToSqlExtensionsTests
	{
		[Fact]
		public void ToSql_WithNullSqlGo_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((ISqlGo<SqlSelect>)null).ToSql());
		}

		[Fact]
		public void ToSql_WithNullSqlSelect_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((SqlSelect)null).ToSql());
		}

		[Fact]
		public void ToSql_WithSqlSelect_ReturnsSql()
		{
			const string expected = "SELECT [Id], [Name] FROM [User] WHERE [Id] = 5";
			var actual = Sql
				.Select("Id", "Name")
				.From("User")
				.Where(SqlExpression.Equal("Id", 5))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithParameter_ReturnsSql()
		{
			const string expected = "SELECT [Id], [Name] FROM [User] WHERE [Id] = @Id";
			var actual = Sql
				.Select("Id", "Name")
				.From("User")
				.Where(SqlExpression.Equal("Id", "Id" + (SqlConstant)42))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithoutWhere_ReturnsSql()
		{
			const string expected = "SELECT [Id], [Name] FROM [User]";
			var actual = Sql
				.Select("Id", "Name")
				.From("User")
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectFromSubQuery_ReturnsSql()
		{
			const string expected = "SELECT [Id], [Name] FROM (SELECT [Id], [Name] FROM [User]) [a] WHERE [Id] = 42";
			var actual = Sql
				.Select("Id", "Name")
				.From(new SqlSubquery(Sql.Select("Id", "Name").From("User").Go(), "a"))
				.Where(SqlExpression.Equal("Id", 42))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithInnerJoin_ReturnsSql()
		{
			SqlTable order = new SqlTable("Order", "o");
			SqlTable orderItem = new SqlTable("OrderItem", "oi");
			SqlColumn itemTotal = new SqlColumn("oi.Total", "ItemTotal");

			const string expected = "SELECT [o].[Id], [o].[CreatedOn], [oi].[ProductId], [oi].[ProductName], [oi].[Quantity], [oi].[UnitPrice], [oi].[Total] AS [ItemTotal] FROM [Order] [o] INNER JOIN [OrderItem] [oi] ON [o].[Id] = [oi].[OrderId]";
			var actual = Sql
				.Select(order + "Id", order + "CreatedOn", orderItem + "ProductId", orderItem + "ProductName", orderItem + "Quantity", orderItem + "UnitPrice", itemTotal)
				.From(order)
				.InnerJoin(orderItem)
				.On(SqlExpression.Equal(order + "Id", orderItem + "OrderId"))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithRightJoin_ReturnsSql()
		{
			SqlTable order = new SqlTable("Order", "o");
			SqlTable employee = new SqlTable("Employee", "e");

			const string expected = "SELECT [o].[Id], [e].[Name] FROM [Order] [o] RIGHT JOIN [Employee] [e] ON [o].[EmployeeId] = [e].[Id]";
			var actual = Sql
				.Select(order + "Id", employee + "Name")
				.From(order)
				.RightJoin(employee)
				.On(SqlExpression.Equal(order + "EmployeeId", employee + "Id"))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithMultipleSortExpressions_ReturnsSql()
		{
			SqlTable people = new SqlTable("People", "p");

			const string expected = "SELECT [p].[Id], [p].[FirstName], [p].[LastName] FROM [People] [p] ORDER BY [p].[LastName], [p].[FirstName], [p].[CreatedOn] DESC";
			var actual = Sql
				.Select(people + "Id", people + "FirstName", people + "LastName")
				.From(people)
				.OrderBy(people + "LastName")
				.OrderBy(people + "FirstName")
				.OrderBy(people + "CreatedOn", SqlSortOrder.Descending)
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithWhereClause_ReturnsSql()
		{
			SqlTable people = new SqlTable("People", "p");

			const string expected = "SELECT [p].[Id], [p].[FirstName], [p].[LastName] FROM [People] [p] WHERE ([p].[Age] > 18) AND ([p].[Age] < 40) ORDER BY [p].[LastName], [p].[FirstName], [p].[CreatedOn] DESC";
			var actual = Sql
				.Select(people + "Id", people + "FirstName", people + "LastName")
				.From(people)
				.Where(SqlExpression.And(SqlExpression.GreaterThan(people + "Age", 18), SqlExpression.LessThan(people + "Age", 40)))
				.OrderBy(people + "LastName")
				.OrderBy(people + "FirstName")
				.OrderBy(people + "CreatedOn", SqlSortOrder.Descending)
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithInUsingValues_ReturnsSql()
		{
			const string expected = "SELECT [Id], [FirstName], [LastName], [Age] FROM [Profile] WHERE [FirstName] IN ('John', 'Joe', 'Jimmy', 'Joel')";
			var actual = Sql
				.Select("Id", "FirstName", "LastName", "Age")
				.From("Profile")
				.Where(SqlExpression.In("FirstName", "John", "Joe", "Jimmy", "Joel"))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithInUsingParameters_ReturnsSql()
		{
			const string expected = "SELECT [Id], [FirstName], [LastName], [Age] FROM [Profile] WHERE [FirstName] IN (@__0, @__1, @__2, @__3)";
			var actual = Sql
				.Select("Id", "FirstName", "LastName", "Age")
				.From("Profile")
				.Where(SqlExpression.In("FirstName", "__0" + (SqlConstant)"John", "__1" + (SqlConstant)"Joe", "__2" + (SqlConstant)"Jimmy", "__3" + (SqlConstant)"Joel"))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithInUsingSubQuery_ReturnsSql()
		{
			const string expected = "SELECT [Id], [FirstName], [LastName], [Age] FROM [Profile] WHERE [FirstName] IN (SELECT [FirstName] FROM [Profile] WHERE [Age] >= 18)";
			var subquery = new SqlSubquery(Sql
				.Select("FirstName")
				.From("Profile")
				.Where(SqlExpression.GreaterThanOrEqual("Age", 18))
				.Go());
			var actual = Sql
				.Select("Id", "FirstName", "LastName", "Age")
				.From("Profile")
				.Where(SqlExpression.In("FirstName", subquery))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithGroupBy_ReturnsSql()
		{
			const string expected = "SELECT [p].[City], AVG([p].[Age]) AS [AverageAge] FROM [Profile] [p] GROUP BY ([p].[City])";
			var p = new SqlTable("Profile", "p");
			var actual = Sql
				.Select(p + "City", SqlAggregate.Average(p + "Age", "AverageAge"))
				.From(p)
				.GroupBy(p + "City")
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithGroupByAndHaving_ReturnsSql()
		{
			const string expected = "SELECT [p].[City], MIN([p].[Age]) AS [MinAge], MAX([p].[Age]) AS [MaxAge] FROM [Profile] [p] GROUP BY ([p].[City]) HAVING [p].[City] IN ('New York', 'London', 'Paris')";
			var p = new SqlTable("Profile", "p");
			var actual = Sql
				.Select(p + "City", SqlAggregate.Min(p + "Age", "MinAge"), SqlAggregate.Max(p + "Age", "MaxAge"))
				.From(p)
				.GroupBy(p + "City")
				.Having(SqlExpression.In(p + "City", "New York", "London", "Paris"))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlSelectWithGroupByAndSumAggregate_ReturnsSql()
		{
			const string expected = "SELECT [oi].[ProductCode], SUM([oi].[Total]) AS [TotalPerProduct] FROM [OrderItem] [oi] GROUP BY ([oi].[ProductCode])";
			var oi = new SqlTable("OrderItem", "oi");
			var actual = Sql
				.Select(oi + "ProductCode", SqlAggregate.Sum(oi + "Total", "TotalPerProduct"))
				.From(oi)
				.GroupBy(oi + "ProductCode")
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithNullSqlDelete_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((SqlDelete)null).ToSql());
		}

		[Fact]
		public void ToSql_WithSqlDelete_ReturnsSql()
		{
			const string expected = "DELETE FROM [User] WHERE [Id] = 5";
			var actual = Sql
				.Delete()
				.From("User")
				.Where(SqlExpression.Equal("Id", 5))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithNullSqlInsert_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((SqlInsert)null).ToSql());
		}

		[Fact]
		public void ToSql_WithSqlInsert_ReturnsSql()
		{
			const string expected = "INSERT INTO [User] ([Name], [Email]) VALUES ('John Doe', 'john@d.oe')";
			var actual = Sql
				.Insert()
				.Into("User", "Name", "Email")
				.Values("John Doe", "john@d.oe")
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlInsertWithMultipleRows_ReturnsSql()
		{
			const string expected = "INSERT INTO [User] ([Name], [Email]) VALUES ('John Doe', 'john@d.oe'), ('Jane Doe', 'jane@d.oe'), ('Jimmy Doe', 'jimmy@d.oe')";
			var actual = Sql
				.Insert()
				.Into("User", "Name", "Email")
				.Values("John Doe", "john@d.oe")
				.Values("Jane Doe", "jane@d.oe")
				.Values("Jimmy Doe", "jimmy@d.oe")
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithNullSqlUpdate_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((SqlUpdate)null).ToSql());
		}

		[Fact]
		public void ToSql_WithSqlUpdate_ReturnsSql()
		{
			const string expected = "UPDATE [User] SET [Name] = 'John Doe' WHERE [Id] = 5";
			var actual = Sql
				.Update("User")
				.Set("Name", "John Doe")
				.Where(SqlExpression.Equal("Id", 5))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToSql_WithSqlUpdateWithMultipleSets_ReturnsSql()
		{
			const string expected = "UPDATE [User] SET [Name] = 'John Doe', [Email] = 'john@d.oe' WHERE [Id] = 5";
			var actual = Sql
				.Update("User")
				.Set("Name", "John Doe")
				.Set("Email", "john@d.oe")
				.Where(SqlExpression.Equal("Id", 5))
				.Go()
				.ToSql();

			Assert.Equal(expected, actual);
		}
	}
}