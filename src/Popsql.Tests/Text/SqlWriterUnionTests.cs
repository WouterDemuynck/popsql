using Microsoft.VisualStudio.TestTools.UnitTesting;
using Popsql.Tests.Utilities;
using Popsql.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Tests.Text
{
    [TestClass]
    public class SqlWriterUnionTests
    {
        [TestMethod]
        public void WriteUnion_WhenInvalidState_ThrowsInvalidOperation()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartUpdate();
                Assert.AreEqual(SqlWriterState.StartUpdate, writer.WriteState);
                AssertEx.Throws<InvalidOperationException>(() => writer.WriteUnion());
            }
        }

        [TestMethod]
        public void WriteUnion_WhenSelect_WritesUnionStatement()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("u", "Id", null);
                writer.WriteColumn("u", "Name", null);
                writer.WriteStartFrom();
                writer.WriteTable("Users", "u");
                writer.WriteEndSelect();

                Assert.AreEqual(SqlWriterState.EndSelect, writer.WriteState);
                writer.WriteUnion();
                Assert.AreEqual(SqlWriterState.Union, writer.WriteState);

                writer.WriteStartSelect();
                writer.WriteColumn("p", "Id", null);
                writer.WriteColumn("p", "Name", null);
                writer.WriteStartFrom();
                writer.WriteTable("Profiles", "p");
                writer.WriteEndSelect();
            }

            Assert.AreEqual("SELECT [u].[Id], [u].[Name] FROM [Users] [u] UNION SELECT [p].[Id], [p].[Name] FROM [Profiles] [p]", builder.ToString());
        }

        [TestMethod]
        public void WriteUnion_WhenSelectWithWhere_WritesUnionStatement()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("u", "Id", null);
                writer.WriteColumn("u", "Name", null);
                writer.WriteStartFrom();
                writer.WriteTable("Users", "u");
                writer.WriteStartWhere();
                writer.WriteColumn("u", "Age", null);
                writer.WriteOperator(SqlBinaryOperator.LessThanOrEqual);
                writer.WriteValue(18);
                writer.WriteEndSelect();

                Assert.AreEqual(SqlWriterState.EndSelect, writer.WriteState);
                writer.WriteUnion();
                Assert.AreEqual(SqlWriterState.Union, writer.WriteState);

                writer.WriteStartSelect();
                writer.WriteColumn("p", "Id", null);
                writer.WriteColumn("p", "Name", null);
                writer.WriteStartFrom();
                writer.WriteTable("Profiles", "p");
                writer.WriteStartWhere();
                writer.WriteColumn("p", "Age", null);
                writer.WriteOperator(SqlBinaryOperator.GreaterThanOrEqual);
                writer.WriteValue(30);
                writer.WriteEndSelect();
            }

            Assert.AreEqual("SELECT [u].[Id], [u].[Name] FROM [Users] [u] WHERE ([u].[Age] <= 18) UNION SELECT [p].[Id], [p].[Name] FROM [Profiles] [p] WHERE ([p].[Age] >= 30)", builder.ToString());
        }

        [TestMethod]
        public void WriteUnion_WhenSelectWithJoin_WritesUnionStatement()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("u", "Id", null);
                writer.WriteColumn("u", "Name", null);
                writer.WriteColumn("p", "Age", null);
                writer.WriteStartFrom();
                writer.WriteTable("Users", "u");
                writer.WriteStartJoin(SqlJoinType.Inner);
                writer.WriteTable("Profiles", "p");
                writer.WriteStartOn();
                writer.WriteColumn("u", "Id", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteColumn("p", "UserId", null);
                //writer.WriteStartWhere();
                //writer.WriteColumn("u", "Age", null);
                //writer.WriteOperator(SqlBinaryOperator.LessThanOrEqual);
                //writer.WriteValue(18);
                writer.WriteEndSelect();

                Assert.AreEqual(SqlWriterState.EndSelect, writer.WriteState);
                writer.WriteUnion();
                Assert.AreEqual(SqlWriterState.Union, writer.WriteState);

                writer.WriteStartSelect();
                writer.WriteColumn("u", "Id", null);
                writer.WriteColumn("u", "Name", null);
                writer.WriteColumn("p", "Age", null);
                writer.WriteStartFrom();
                writer.WriteTable("Users", "u");
                writer.WriteStartJoin(SqlJoinType.Inner);
                writer.WriteTable("Profiles", "p");
                writer.WriteStartOn();
                writer.WriteColumn("u", "Id", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteColumn("p", "UserId", null);
                writer.WriteStartWhere();
                writer.WriteColumn("p", "Age", null);
                writer.WriteOperator(SqlBinaryOperator.GreaterThanOrEqual);
                writer.WriteValue(30);
                writer.WriteEndSelect();
            }

            Assert.AreEqual("SELECT [u].[Id], [u].[Name], [p].[Age] FROM [Users] [u] INNER JOIN [Profiles] [p] ON ([u].[Id] = [p].[UserId]) UNION SELECT [u].[Id], [u].[Name], [p].[Age] FROM [Users] [u] INNER JOIN [Profiles] [p] ON ([u].[Id] = [p].[UserId]) WHERE ([p].[Age] >= 30)", builder.ToString());
        }

        [TestMethod]
        public void WriteUnion_WhenSelectWithOrderBy_WritesUnionStatement()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("u", "Id", null);
                writer.WriteColumn("u", "Name", null);
                writer.WriteColumn("p", "Age", null);
                writer.WriteStartFrom();
                writer.WriteTable("Users", "u");
                writer.WriteStartJoin(SqlJoinType.Inner);
                writer.WriteTable("Profiles", "p");
                writer.WriteStartOn();
                writer.WriteColumn("u", "Id", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteColumn("p", "UserId", null);
                writer.WriteStartWhere();
                writer.WriteColumn("p", "Age", null);
                writer.WriteOperator(SqlBinaryOperator.LessThanOrEqual);
                writer.WriteValue(18);
                writer.WriteStartOrderBy();
                writer.WriteColumn("p", "Age", null);
                writer.WriteSortOrder(SqlSortOrder.Descending);
                writer.WriteColumn("u", "Name", null);
                writer.WriteSortOrder(SqlSortOrder.Ascending);
                writer.WriteEndSelect();

                Assert.AreEqual(SqlWriterState.EndSelect, writer.WriteState);
                writer.WriteUnion();
                Assert.AreEqual(SqlWriterState.Union, writer.WriteState);

                writer.WriteStartSelect();
                writer.WriteColumn("u", "Id", null);
                writer.WriteColumn("u", "Name", null);
                writer.WriteColumn("p", "Age", null);
                writer.WriteStartFrom();
                writer.WriteTable("Users", "u");
                writer.WriteStartJoin(SqlJoinType.Inner);
                writer.WriteTable("Profiles", "p");
                writer.WriteStartOn();
                writer.WriteColumn("u", "Id", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteColumn("p", "UserId", null);
                writer.WriteStartWhere();
                writer.WriteColumn("p", "Age", null);
                writer.WriteOperator(SqlBinaryOperator.GreaterThanOrEqual);
                writer.WriteValue(30);
                writer.WriteStartOrderBy();
                writer.WriteColumn("p", "Age", null);
                writer.WriteSortOrder(SqlSortOrder.Descending);
                writer.WriteColumn("u", "Name", null);
                writer.WriteSortOrder(SqlSortOrder.Ascending);
                writer.WriteEndSelect();
            }

            Assert.AreEqual("SELECT [u].[Id], [u].[Name], [p].[Age] FROM [Users] [u] INNER JOIN [Profiles] [p] ON ([u].[Id] = [p].[UserId]) WHERE ([p].[Age] <= 18) ORDER BY [p].[Age] DESC, [u].[Name] ASC UNION SELECT [u].[Id], [u].[Name], [p].[Age] FROM [Users] [u] INNER JOIN [Profiles] [p] ON ([u].[Id] = [p].[UserId]) WHERE ([p].[Age] >= 30) ORDER BY [p].[Age] DESC, [u].[Name] ASC", builder.ToString());
        }
    }
}
