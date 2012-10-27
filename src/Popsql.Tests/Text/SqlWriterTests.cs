using Microsoft.VisualStudio.TestTools.UnitTesting;
using Popsql.Tests.Utilities;
using Popsql.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Tests.Text
{
    [TestClass]
    public class SqlWriterTests
    {
        [TestMethod]
        public void Ctor_WithNullStringBuilder_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlWriter((StringBuilder)null));
        }

        [TestMethod]
        public void Ctor_WithNullTextWriter_ThrowsArgumentNull()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SqlWriter((TextWriter)null));
        }

        [TestMethod]
        public void WriteState_WhenDisposed_IsClosed()
        {
            StringBuilder builder = new StringBuilder();
            SqlWriter writer = new SqlWriter(builder);
            writer.Dispose();

            Assert.AreEqual(SqlWriterState.Closed, writer.WriteState);
        }

        [TestMethod]
        public void WriteState_AfterCtor_IsStart()
        {
            StringBuilder builder = new StringBuilder();
            SqlWriter writer = new SqlWriter(builder);

            Assert.AreEqual(SqlWriterState.Start, writer.WriteState);
            writer.Dispose();
        }
    }
}
