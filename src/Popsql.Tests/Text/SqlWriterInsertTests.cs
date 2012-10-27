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
    public class SqlWriterInsertTests
    {
        [TestMethod]
        public void WriteStartInsert_WritesInsert()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartInsert();
                Assert.AreEqual(SqlWriterState.StartInsert, writer.WriteState);
            }

            Assert.AreEqual("INSERT", builder.ToString());
        }

        [TestMethod]
        public void WriteStartInsert_WhenDisposed_ThrowsObjectDisposed()
        {
            StringBuilder builder = new StringBuilder();
            SqlWriter writer = new SqlWriter(builder);
            writer.Dispose();

            AssertEx.Throws<ObjectDisposedException>(() => writer.WriteStartInsert());
        }

        [TestMethod]
        public void WriteStartInto_WhenInsertStatement_WritesInto()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartInsert();
                writer.WriteStartInto();
                Assert.AreEqual(SqlWriterState.Into, writer.WriteState);
            }

            Assert.AreEqual("INSERT INTO", builder.ToString());
        }

        [TestMethod]
        public void WriteTable_WhenInsertStatement_WritesTable()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartInsert();
                writer.WriteStartInto();
                writer.WriteTable("Users");
                Assert.AreEqual(SqlWriterState.Into, writer.WriteState);
            }

            Assert.AreEqual("INSERT INTO [Users]", builder.ToString());
        }
    }
}
