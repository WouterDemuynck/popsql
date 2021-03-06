﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class SqlWriterSelectTests
    {
        [TestMethod]
        public void WriteStartSelect_WritesSelect()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                Assert.AreEqual(SqlWriterState.StartSelect, writer.WriteState);
            }

            Assert.AreEqual("SELECT", builder.ToString());
        }

        [TestMethod]
        public void WriteStartSelect_WhenDisposed_ThrowsObjectDisposed()
        {
            StringBuilder builder = new StringBuilder();
            SqlWriter writer = new SqlWriter(builder);
            writer.Dispose();

            AssertEx.Throws<ObjectDisposedException>(() => writer.WriteStartSelect());
        }

        [TestMethod]
        public void WriteColumn_WhenSelectStatement_WritesColumns()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                Assert.AreEqual(SqlWriterState.Select, writer.WriteState);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e]", builder.ToString());
        }

        [TestMethod]
        public void WriteStartFrom_WhenSelectStatement_WritesFrom()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                Assert.AreEqual(SqlWriterState.StartFrom, writer.WriteState);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM", builder.ToString());
        }

        [TestMethod]
        public void WriteTable_WhenSelectStatement_WritesTables()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteTable("Sessions", "s");
                Assert.AreEqual(SqlWriterState.From, writer.WriteState);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users], [Sessions] [s]", builder.ToString());
        }

        [TestMethod]
        public void WriteStartFrom_WhenSelectStatement_WritesWhere()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteStartWhere();
                Assert.AreEqual(SqlWriterState.StartWhere, writer.WriteState);
                writer.WriteColumn("Id");
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteParameter("Id");
                writer.WriteOperator(SqlBinaryOperator.Or);
                writer.WriteColumn("Id");
                writer.WriteOperator(SqlBinaryOperator.LessThan);
                writer.WriteValue(100);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users] WHERE ([Id] = @Id) OR ([Id] < 100)", builder.ToString());
        }

        [TestMethod]
        public void WriteOrderBy_WhenSelectStatementWithoutWhereClause_WritesOrderBy()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteStartOrderBy();
                writer.WriteColumn("Id");
                writer.WriteSortOrder(SqlSortOrder.Ascending);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users] ORDER BY [Id] ASC", builder.ToString());
        }

        [TestMethod]
        public void WriteOrderBy_WhenSelectStatementWithMultipleSortExpressions_WritesOrderBy()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteStartOrderBy();
                writer.WriteColumn("Id");
                writer.WriteSortOrder(SqlSortOrder.Ascending);
                writer.WriteColumn("Name");
                writer.WriteSortOrder(SqlSortOrder.Descending);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users] ORDER BY [Id] ASC, [Name] DESC", builder.ToString());
        }

        [TestMethod]
        public void WriteStartOrderBy_WhenSelectStatementWithWhereClause_WritesOrderBy()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteStartWhere();
                Assert.AreEqual(SqlWriterState.StartWhere, writer.WriteState);
                writer.WriteColumn("Id");
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteParameter("Id");
                writer.WriteOperator(SqlBinaryOperator.Or);
                writer.WriteColumn("Id");
                writer.WriteOperator(SqlBinaryOperator.LessThan);
                writer.WriteValue(100);
                writer.WriteStartOrderBy();
                writer.WriteColumn("Id");
                writer.WriteSortOrder(SqlSortOrder.Ascending);
                writer.WriteColumn("Name");
                writer.WriteSortOrder(SqlSortOrder.Descending);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users] WHERE ([Id] = @Id) OR ([Id] < 100) ORDER BY [Id] ASC, [Name] DESC", builder.ToString());
        }

        [TestMethod]
        public void WriteStartOn_WritesOnClause()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteStartJoin();
                writer.WriteTable("Profiles");
                writer.WriteStartOn();
                writer.WriteColumn("Users", "Id", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteColumn("Profiles", "UserId", null);
                writer.WriteOperator(SqlBinaryOperator.And);
                writer.WriteColumn("Users", "Name", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteParameter("Name");
                writer.WriteStartWhere();
                Assert.AreEqual(SqlWriterState.StartWhere, writer.WriteState);
                writer.WriteColumn("Id");
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteParameter("Id");
                writer.WriteOperator(SqlBinaryOperator.Or);
                writer.WriteColumn("Id");
                writer.WriteOperator(SqlBinaryOperator.LessThan);
                writer.WriteValue(100);
                writer.WriteStartOrderBy();
                writer.WriteColumn("Id");
                writer.WriteSortOrder(SqlSortOrder.Ascending);
                writer.WriteColumn("Name");
                writer.WriteSortOrder(SqlSortOrder.Descending);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users] JOIN [Profiles] ON ([Users].[Id] = [Profiles].[UserId]) AND ([Users].[Name] = @Name) WHERE ([Id] = @Id) OR ([Id] < 100) ORDER BY [Id] ASC, [Name] DESC", builder.ToString());
        }

        [TestMethod]
        public void WriteStartJoin_WritesInnerJoin()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteStartJoin(SqlJoinType.Inner);
                writer.WriteTable("Profiles");
                writer.WriteStartOn();
                writer.WriteColumn("Users", "Id", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteColumn("Profiles", "UserId", null);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users] INNER JOIN [Profiles] ON ([Users].[Id] = [Profiles].[UserId])", builder.ToString());
        }

        [TestMethod]
        public void WriteStartJoin_WritesLeftJoin()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteStartJoin(SqlJoinType.Left);
                writer.WriteTable("Profiles");
                writer.WriteStartOn();
                writer.WriteColumn("Users", "Id", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteColumn("Profiles", "UserId", null);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users] LEFT JOIN [Profiles] ON ([Users].[Id] = [Profiles].[UserId])", builder.ToString());
        }

        [TestMethod]
        public void WriteStartJoin_WritesRightJoin()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartSelect();
                writer.WriteColumn("Id");
                writer.WriteColumn("Name", "n");
                writer.WriteColumn("Users", "Email", "e");
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                writer.WriteStartJoin(SqlJoinType.Right);
                writer.WriteTable("Profiles");
                writer.WriteStartOn();
                writer.WriteColumn("Users", "Id", null);
                writer.WriteOperator(SqlBinaryOperator.Equal);
                writer.WriteColumn("Profiles", "UserId", null);
            }

            Assert.AreEqual("SELECT [Id], [Name] AS [n], [Users].[Email] AS [e] FROM [Users] RIGHT JOIN [Profiles] ON ([Users].[Id] = [Profiles].[UserId])", builder.ToString());
        }
    }
}
