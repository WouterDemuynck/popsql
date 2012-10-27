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
    }
}
