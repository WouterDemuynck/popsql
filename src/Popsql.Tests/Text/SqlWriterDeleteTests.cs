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
    public class SqlWriterDeleteTests
    {

        [TestMethod]
        public void WriteStartDelete_WritesDelete()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartDelete();
                Assert.AreEqual(SqlWriterState.StartDelete, writer.WriteState);
            }

            Assert.AreEqual("DELETE", builder.ToString());
        }

        [TestMethod]
        public void WriteStartDelete_WhenDisposed_ThrowsObjectDisposed()
        {
            StringBuilder builder = new StringBuilder();
            SqlWriter writer = new SqlWriter(builder);
            writer.Dispose();

            AssertEx.Throws<ObjectDisposedException>(() => writer.WriteStartDelete());
        }

        [TestMethod]
        public void WriteStartFrom_WhenDeleteStatement_WritesFrom()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartDelete();
                writer.WriteStartFrom();
                Assert.AreEqual(SqlWriterState.StartFrom, writer.WriteState);
            }

            Assert.AreEqual("DELETE FROM", builder.ToString());
        }

        [TestMethod]
        public void WriteTable_WhenDeleteStatement_WritesTable()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartDelete();
                writer.WriteStartFrom();
                writer.WriteTable("Users");
                Assert.AreEqual(SqlWriterState.From, writer.WriteState);
            }

            Assert.AreEqual("DELETE FROM [Users]", builder.ToString());
        }
    }
}
