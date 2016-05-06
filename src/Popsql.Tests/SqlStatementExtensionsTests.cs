using System;
using Popsql.Grammar;
using Xunit;

namespace Popsql.Tests
{
	public class SqlStatementExtensionsTests
	{
		[Fact]
		public void ToSql_WithNullGo_ThrowsArgumentNull()
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
			const string expected = "SELECT [Id], [Name] FROM (SELECT [Id], [Name] FROM [User]) [a] WHERE [Id] = 5";
			var actual = Sql
				.Select("Id", "Name")
				.From(new SqlSubquery(Sql.Select("Id", "Name").From("User").Go(), "a"))
				.Where(SqlExpression.Equal("Id", 5))
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
				.InnerJoin(orderItem, SqlExpression.Equal(order + "Id", orderItem + "OrderId"))
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
				.RightJoin(employee, SqlExpression.Equal(order + "EmployeeId", employee + "Id"))
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