using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Tests
{
    [TestClass]
    public class SqlStatementExtensionsTests
	{
		[TestMethod]
		public void ToSql_WithSelectAndSimpleWhereClause_WritesCorrectSql()
		{
			var sql = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", 5))
				.ToSql();

			Assert.AreEqual("SELECT [Id], [Name], [Email] FROM [Users] WHERE ([Id] = 5)", sql);
		}

		[TestMethod]
        public void ToSql_WithSelect_WritesCorrectSql()
        {
            var sql = Sql
                .Select("Id", "Name", "Email")
                .From("Users")
                .Where(SqlExpression.Or(SqlExpression.Equal("Id", 5), SqlExpression.GreaterThan("Age", "Age" + (SqlConstant)30)))
                .ToSql();

            Assert.AreEqual("SELECT [Id], [Name], [Email] FROM [Users] WHERE (([Id] = 5) OR ([Age] > @Age))", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithJoin_WritesCorrectSql()
        {
            SqlTable users = "Users";
            SqlTable profiles = "Profiles";

            var sql = Sql
                .Select(users + "Id", "Name", "Email")
                .From(users)
                .Join(profiles, SqlExpression.Equal(users + "Id", profiles + "UserId"))
                .Where(SqlExpression.Or(SqlExpression.Equal(users + "Id", 5), SqlExpression.GreaterThan("Age", "Age" + (SqlConstant)30)))
                .ToSql();

            Assert.AreEqual("SELECT [Users].[Id], [Name], [Email] FROM [Users] JOIN [Profiles] ON ([Users].[Id] = [Profiles].[UserId]) WHERE (([Users].[Id] = 5) OR ([Age] > @Age))", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithInnerJoin_WritesCorrectSql()
        {
            SqlTable users = "Users";
            SqlTable profiles = "Profiles";

            var sql = Sql
                .Select(users + "Id", "Name", "Email")
                .From(users)
                .InnerJoin(profiles, SqlExpression.Equal(users + "Id", profiles + "UserId"))
                .Where(SqlExpression.Or(SqlExpression.Equal(users + "Id", 5), SqlExpression.GreaterThan("Age", "Age" + (SqlConstant)30)))
                .ToSql();

            Assert.AreEqual("SELECT [Users].[Id], [Name], [Email] FROM [Users] INNER JOIN [Profiles] ON ([Users].[Id] = [Profiles].[UserId]) WHERE (([Users].[Id] = 5) OR ([Age] > @Age))", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithLeftJoin_WritesCorrectSql()
        {
            SqlTable users = "Users";
            SqlTable profiles = "Profiles";

            var sql = Sql
                .Select(users + "Id", "Name", "Email")
                .From(users)
                .LeftJoin(profiles, SqlExpression.Equal(users + "Id", profiles + "UserId"))
                .Where(SqlExpression.Or(SqlExpression.Equal(users + "Id", 5), SqlExpression.GreaterThan("Age", "Age" + (SqlConstant)30)))
                .ToSql();

            Assert.AreEqual("SELECT [Users].[Id], [Name], [Email] FROM [Users] LEFT JOIN [Profiles] ON ([Users].[Id] = [Profiles].[UserId]) WHERE (([Users].[Id] = 5) OR ([Age] > @Age))", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithRightJoin_WritesCorrectSql()
        {
            SqlTable users = "Users";
            SqlTable profiles = "Profiles";

            var sql = Sql
                .Select(users + "Id", "Name", "Email")
                .From(users)
                .RightJoin(profiles, SqlExpression.Equal(users + "Id", profiles + "UserId"))
                .Where(SqlExpression.Or(SqlExpression.Equal(users + "Id", 5), SqlExpression.GreaterThan("Age", "Age" + (SqlConstant)30)))
                .ToSql();

            Assert.AreEqual("SELECT [Users].[Id], [Name], [Email] FROM [Users] RIGHT JOIN [Profiles] ON ([Users].[Id] = [Profiles].[UserId]) WHERE (([Users].[Id] = 5) OR ([Age] > @Age))", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithOrderByClause_WritesCorrectSql()
        {
            var sql = Sql
                .Select("Id", "Name", "Email")
                .From("Users")
                .Where(SqlExpression.Or(SqlExpression.Equal("Id", 5), SqlExpression.GreaterThan("Age", "Age" + (SqlConstant)30)))
                .OrderBy("Id", SqlSortOrder.Descending)
                .OrderBy("Name")
                .ToSql();

            Assert.AreEqual("SELECT [Id], [Name], [Email] FROM [Users] WHERE (([Id] = 5) OR ([Age] > @Age)) ORDER BY [Id] DESC, [Name] ASC", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithOrderByClauseWithoutWhereClause_WritesCorrectSql()
        {
            var sql = Sql
                .Select("Id", "Name", "Email")
                .From("Users")
                .OrderBy("Id", SqlSortOrder.Descending)
                .OrderBy("Name")
                .ToSql();

            Assert.AreEqual("SELECT [Id], [Name], [Email] FROM [Users] ORDER BY [Id] DESC, [Name] ASC", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithJoinWithOrderByClauseWithWhereClause_WritesCorrectSql()
        {
            var foo = new SqlTable("foo", "f");
            var bar = new SqlTable("bar", "b");

            var sql = Sql
                .Select(foo + "Id", foo + "Name", bar + "Email")
                .From(foo)
                .Join(bar, SqlExpression.Equal(foo + "Id", bar + "FooId"))
                .Where(SqlExpression.Equal(foo + "Id", 5))
                .OrderBy("Id", SqlSortOrder.Descending)
                .OrderBy("Name")
                .ToSql();

            Assert.AreEqual("SELECT [f].[Id], [f].[Name], [b].[Email] FROM [foo] [f] JOIN [bar] [b] ON ([f].[Id] = [b].[FooId]) WHERE ([f].[Id] = 5) ORDER BY [Id] DESC, [Name] ASC", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithInnerJoinWithOrderByClauseWithWhereClause_WritesCorrectSql()
        {
            var foo = new SqlTable("foo", "f");
            var bar = new SqlTable("bar", "b");

            var sql = Sql
                .Select(foo + "Id", foo + "Name", bar + "Email")
                .From(foo)
                .InnerJoin(bar, SqlExpression.Equal(foo + "Id", bar + "FooId"))
                .Where(SqlExpression.Equal(foo + "Id", 5))
                .OrderBy("Id", SqlSortOrder.Descending)
                .OrderBy("Name")
                .ToSql();

            Assert.AreEqual("SELECT [f].[Id], [f].[Name], [b].[Email] FROM [foo] [f] INNER JOIN [bar] [b] ON ([f].[Id] = [b].[FooId]) WHERE ([f].[Id] = 5) ORDER BY [Id] DESC, [Name] ASC", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithLeftJoinWithOrderByClauseWithWhereClause_WritesCorrectSql()
        {
            var foo = new SqlTable("foo", "f");
            var bar = new SqlTable("bar", "b");

            var sql = Sql
                .Select(foo + "Id", foo + "Name", bar + "Email")
                .From(foo)
                .LeftJoin(bar, SqlExpression.Equal(foo + "Id", bar + "FooId"))
                .Where(SqlExpression.Equal(foo + "Id", 5))
                .OrderBy("Id", SqlSortOrder.Descending)
                .OrderBy("Name")
                .ToSql();

            Assert.AreEqual("SELECT [f].[Id], [f].[Name], [b].[Email] FROM [foo] [f] LEFT JOIN [bar] [b] ON ([f].[Id] = [b].[FooId]) WHERE ([f].[Id] = 5) ORDER BY [Id] DESC, [Name] ASC", sql);
        }

        [TestMethod]
        public void ToSql_WithSelectWithRightJoinWithOrderByClauseWithWhereClause_WritesCorrectSql()
        {
            var foo = new SqlTable("foo", "f");
            var bar = new SqlTable("bar", "b");

            var sql = Sql
                .Select(foo + "Id", foo + "Name", bar + "Email")
                .From(foo)
                .RightJoin(bar, SqlExpression.Equal(foo + "Id", bar + "FooId"))
                .Where(SqlExpression.Equal(foo + "Id", 5))
                .OrderBy("Id", SqlSortOrder.Descending)
                .OrderBy("Name")
                .ToSql();

            Assert.AreEqual("SELECT [f].[Id], [f].[Name], [b].[Email] FROM [foo] [f] RIGHT JOIN [bar] [b] ON ([f].[Id] = [b].[FooId]) WHERE ([f].[Id] = 5) ORDER BY [Id] DESC, [Name] ASC", sql);
        }

		[TestMethod]
		public void ToSql_WithSelectWithInExpression_WritesCorrectSql()
		{
			var sql = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.In("Id", 1, 2, 3, 4, 5, 6, 7, 8, 9))
				.OrderBy("Id", SqlSortOrder.Descending)
				.OrderBy("Name")
				.ToSql();

			Assert.AreEqual("SELECT [Id], [Name], [Email] FROM [Users] WHERE ([Id] IN (1, 2, 3, 4, 5, 6, 7, 8, 9)) ORDER BY [Id] DESC, [Name] ASC", sql);
		}

		[TestMethod]
        public void ToSql_WithDelete_WritesCorrectSql()
        {
            var sql = Sql
                .Delete()
                .From("Users")
                .Where(SqlExpression.Or(SqlExpression.Equal("Id", 5), SqlExpression.GreaterThan("Age", "Age" + (SqlConstant)30)))
                .ToSql();

            Assert.AreEqual("DELETE FROM [Users] WHERE (([Id] = 5) OR ([Age] > @Age))", sql);
        }

        [TestMethod]
        public void ToSql_WithUpdate_WritesCorrectSql()
        {
            var sql = Sql
                .Update("Users")
                .Set("Age", 30)
                .Set("Name", "Name" + (SqlConstant)"Wouter")
                .Where(SqlExpression.Or(SqlExpression.Equal("Id", 5), SqlExpression.GreaterThan("Age", "Age" + (SqlConstant)30)))
                .ToSql();

            Assert.AreEqual("UPDATE [Users] SET [Age] = 30, [Name] = @Name WHERE (([Id] = 5) OR ([Age] > @Age))", sql);
        }

        [TestMethod]
        public void ToSql_WithInsert_WritesCorrectSql()
        {
            var sql = Sql
                .Insert()
                .Into("Users", "Email", "Age")
                .Values("Email" + (SqlConstant)"wouter.am.demuynck@gmail.com", 30)
                .ToSql();

            Assert.AreEqual("INSERT INTO [Users] ([Email], [Age]) VALUES (@Email, 30)", sql);
        }

        [TestMethod]
        public void ToSql_WithBulkInsert_WritesCorrectSql()
        {
            var sql = Sql
                .Insert()
                .Into("Users", "Email", "Age")
                .Values("Email" + (SqlConstant)"wouter.am.demuynck@gmail.com", 30)
                .Values("someone@foo.bar", 23)
                .ToSql();

            Assert.AreEqual("INSERT INTO [Users] ([Email], [Age]) VALUES ((@Email, 30), ('someone@foo.bar', 23))", sql);
        }

        [TestMethod]
        public void ToSql_WithUnion_WritesCorrectSql()
        {
            var query = Sql.Union(
                Sql.Select("Id", "Name").From("Users"),
                Sql.Select("Id", "Name").From("Users"))
                .ToSql();

            Assert.AreEqual("(SELECT [Id], [Name] FROM [Users]) UNION (SELECT [Id], [Name] FROM [Users])", query);
        }

        [TestMethod]
        public void ToSql_WithUnionWithWhereClause_WritesCorrectSql()
        {
            var query = Sql.Union(
                Sql.Select("Id", "Name").From("Users").Where(SqlExpression.LessThan("Age", 18)),
                Sql.Select("Id", "Name").From("Users").Where(SqlExpression.GreaterThanOrEqual("Age", 30)))
                .ToSql();

            Assert.AreEqual("(SELECT [Id], [Name] FROM [Users] WHERE ([Age] < 18)) UNION (SELECT [Id], [Name] FROM [Users] WHERE ([Age] >= 30))", query);
        }

        [TestMethod]
        public void ToSql_WithUnionWithJoinClauseWithoutOnClause_WritesCorrectSql()
        {
            // Okay, this is a far-off scenario I think (since the SQL won't really make sense without an ON), but I want to test it anyway.
            var u = new SqlTable("Users", "u");
            var p = new SqlTable("Profiles", "p");
            var query = Sql.Union(
                Sql.Select(u + "Id", u + "Name", p + "Age").From(u).Join(p),
                Sql.Select(u + "Id", u + "Name", p + "Age").From(u).InnerJoin(p, SqlExpression.Equal(u + "Id", p + "UserId")).Where(SqlExpression.GreaterThanOrEqual(p + "Age", 30)))
                .ToSql();

            Assert.AreEqual("(SELECT [u].[Id], [u].[Name], [p].[Age] FROM [Users] [u] JOIN [Profiles] [p]) UNION (SELECT [u].[Id], [u].[Name], [p].[Age] FROM [Users] [u] INNER JOIN [Profiles] [p] ON ([u].[Id] = [p].[UserId]) WHERE ([p].[Age] >= 30))", query);
        }

        [TestMethod]
        public void ToSql_WithUnionWithJoinClause_WritesCorrectSql()
        {
            var u = new SqlTable("Users", "u");
            var p = new SqlTable("Profiles", "p");
            var query = Sql.Union(
                Sql.Select(u + "Id", u + "Name", p + "Age").From(u).InnerJoin(p, SqlExpression.Equal(u + "Id", p + "UserId")),
                Sql.Select(u + "Id", u + "Name", p + "Age").From(u).InnerJoin(p, SqlExpression.Equal(u + "Id", p + "UserId")).Where(SqlExpression.GreaterThanOrEqual(p + "Age", 30)))
                .ToSql();

            Assert.AreEqual("(SELECT [u].[Id], [u].[Name], [p].[Age] FROM [Users] [u] INNER JOIN [Profiles] [p] ON ([u].[Id] = [p].[UserId])) UNION (SELECT [u].[Id], [u].[Name], [p].[Age] FROM [Users] [u] INNER JOIN [Profiles] [p] ON ([u].[Id] = [p].[UserId]) WHERE ([p].[Age] >= 30))", query);
        }

        [TestMethod]
        public void ToSql_WithUnionWithOrderByClause_WritesCorrectSql()
        {
            var query = Sql.Union(
                Sql.Select("Id", "Name").From("Users").Where(SqlExpression.LessThan("Age", 18)).OrderBy("Name").OrderBy("Id"),
                Sql.Select("Id", "Name").From("Users").Where(SqlExpression.GreaterThanOrEqual("Age", 30)).OrderBy("Age", SqlSortOrder.Descending))
                .ToSql();

            Assert.AreEqual("(SELECT [Id], [Name] FROM [Users] WHERE ([Age] < 18) ORDER BY [Name] ASC, [Id] ASC) UNION (SELECT [Id], [Name] FROM [Users] WHERE ([Age] >= 30) ORDER BY [Age] DESC)", query);
        }
    }
}
