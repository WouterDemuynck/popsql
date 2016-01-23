using Popsql.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Popsql.Tests.Text
{
	public class SqlWriterTests
	{
		[Fact]
		public void Ctor_WithNullStringBuilder_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlWriter((StringBuilder)null));
		}

		[Fact]
		public void Ctor_WithNullTextWriter_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlWriter((TextWriter)null));
		}

		private class TestSqlWriter : SqlWriter
		{
			public TestSqlWriter(StringBuilder builder) 
				: base(builder)
			{
			}

			public void WriteTest(string value)
			{
				Write(value);
			}

			public void WriteRawTest(string value)
			{
				WriteRaw(value);
			}

			public void EnsureNotDisposedTest()
			{
				EnsureNotDisposed();
			}
		}

		[Fact]
		public void WriteRaw_WritesRawText()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteRawTest("WriteRaw");
				writer.WriteRawTest("() ");
				writer.WriteRawTest("does n");
				writer.WriteRawTest("ot wri");
				writer.WriteRawTest("te spaces between");
				writer.WriteRawTest(" calls.");
			}

			Assert.Equal("WriteRaw() does not write spaces between calls.", builder.ToString());
		}

		[Fact]
		public void Write_WritesText()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteTest("Write()");
				writer.WriteTest("does");
				writer.WriteTest("write");
				writer.WriteTest("spaces");
				writer.WriteTest("between");
				writer.WriteTest("calls.");
			}

			Assert.Equal("Write() does write spaces between calls.", builder.ToString());
		}

		[Fact]
		public void EnsureNotDisposed_WhenNotDisposed_DoesNotThrow()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.EnsureNotDisposedTest();
			}
		}

		[Fact]
		public void EnsureNotDisposed_WhenDisposed_ThrowsObjectDisposed()
		{
			var builder = new StringBuilder();
			TestSqlWriter writer;
			using (writer = new TestSqlWriter(builder))
			{
			}

			Assert.Throws<ObjectDisposedException>(() => writer.EnsureNotDisposedTest());
		}
	}
}
