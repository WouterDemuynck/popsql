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
    }
}
