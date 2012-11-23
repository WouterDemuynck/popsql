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
                Assert.AreEqual(SqlWriterState.StartInto, writer.WriteState);
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
                Assert.AreEqual(SqlWriterState.StartInto, writer.WriteState);
            }

            Assert.AreEqual("INSERT INTO [Users]", builder.ToString());
        }

        [TestMethod]
        public void WriteColumn_WhenInsertStatement_WritesColumns()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartInsert();
                writer.WriteStartInto();
                writer.WriteTable("Users");
                writer.WriteColumn("Id");
                writer.WriteColumn("UserName");
                writer.WriteColumn("Email");
                writer.WriteCloseParenthesis();
                Assert.AreEqual(SqlWriterState.Into, writer.WriteState);
            }

            Assert.AreEqual("INSERT INTO [Users] ([Id], [UserName], [Email])", builder.ToString());
        }

        [TestMethod]
        public void WriteValue_WhenInsertStatement_WritesValues()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartInsert();
                writer.WriteStartInto();
                writer.WriteTable("Users");
                writer.WriteColumn("Id");
                writer.WriteColumn("UserName");
                writer.WriteColumn("Email");
                writer.WriteStartValues();
                Assert.AreEqual(SqlWriterState.StartValues, writer.WriteState);
                writer.WriteValue(15);
                writer.WriteValue("My user name.");
                writer.WriteValue("myemail@mydomain.local");
                writer.WriteCloseParenthesis();
                Assert.AreEqual(SqlWriterState.Values, writer.WriteState);
            }

            Assert.AreEqual("INSERT INTO [Users] ([Id], [UserName], [Email]) VALUES (15, 'My user name.', 'myemail@mydomain.local')", builder.ToString());
        }
    }
}
