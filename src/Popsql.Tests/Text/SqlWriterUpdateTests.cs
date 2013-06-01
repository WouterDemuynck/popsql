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
    public class SqlWriterUpdateTests
    {
        [TestMethod]
        public void WriteStartUpdate_WritesUpdate()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartUpdate();
                Assert.AreEqual(SqlWriterState.StartUpdate, writer.WriteState);
            }

            Assert.AreEqual("UPDATE", builder.ToString());
        }

        [TestMethod]
        public void WriteStartUpdate_WhenDisposed_ThrowsObjectDisposed()
        {
            StringBuilder builder = new StringBuilder();
            SqlWriter writer = new SqlWriter(builder);
            writer.Dispose();

            AssertEx.Throws<ObjectDisposedException>(() => writer.WriteStartUpdate());
        }

        [TestMethod]
        public void WriteTable_WhenUpdateStatement_WritesTable()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartUpdate();
                writer.WriteTable("Users");
                Assert.AreEqual(SqlWriterState.Update, writer.WriteState);
            }

            Assert.AreEqual("UPDATE [Users]", builder.ToString());
        }

        [TestMethod]
        public void WriteStartSet_WhenUpdateStatement_WritesParametersAndValues()
        {
            StringBuilder builder = new StringBuilder();
            using (SqlWriter writer = new SqlWriter(builder))
            {
                writer.WriteStartUpdate();
                writer.WriteTable("Users");
                writer.WriteStartSet();
                writer.WriteColumn("Id");
                writer.WriteValue(15);
                writer.WriteColumn("UserName");
                writer.WriteParameter("UserName");
                writer.WriteColumn("Email");
                writer.WriteParameter("Email");
                Assert.AreEqual(SqlWriterState.Set, writer.WriteState);
            }

            Assert.AreEqual("UPDATE [Users] SET [Id] = 15, [UserName] = @UserName, [Email] = @Email", builder.ToString());
        }
    }
}
