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
		[Fact]
		public void Ctor_WithStringBuilderAndWriterSettings_UsesSpecifiedSettings()
		{
			var settings = new SqlWriterSettings();
			TestSqlWriter writer = new TestSqlWriter(new StringBuilder(), settings);
			Assert.Same(settings, writer.SettingsTest);
		}

		[Fact]
		public void Ctor_WithTextWriterAndWriterSettings_UsesSpecifiedSettings()
		{
			var settings = new SqlWriterSettings();
			TestSqlWriter writer = new TestSqlWriter(new StringWriter(new StringBuilder()), settings);
			Assert.Same(settings, writer.SettingsTest);
		}

		private class TestSqlWriter : SqlWriter
		{
			public TestSqlWriter(StringBuilder builder) 
				: base(builder)
			{
			}

			public TestSqlWriter(StringBuilder builder, SqlDialect dialect) 
				: base(builder, dialect)
			{
			}

			public TestSqlWriter(StringBuilder builder, SqlWriterSettings settings) 
				: base(builder, settings)
			{
			}

			public TestSqlWriter(TextWriter writer, SqlWriterSettings settings) 
				: base(writer, settings)
			{
			}

			public SqlDialect DialectTest
			{
				get
				{
					return Dialect;
				}
			}

			public SqlWriterSettings SettingsTest
			{
				get
				{
					return Settings;
				}
			}

			public void WriteKeywordTest(SqlKeyword keyword)
			{
				WriteKeyword(keyword);
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
		public void Ctor_WithTextWriterAndNullDialect_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlWriter(new TestTextWriter(), (SqlDialect)null));
		}

		[Fact]
		public void Ctor_WithStringBuilderAndNullDialect_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlWriter(new StringBuilder(), (SqlDialect)null));
		}

		[Fact]
		public void Ctor_WithDialect_ReturnsDialect()
		{
			var builder = new StringBuilder();
			var writer = new TestSqlWriter(builder, SqlDialect.Default);

			Assert.NotNull(writer.DialectTest);
			Assert.Same(SqlDialect.Default, writer.DialectTest);
		}

		[Fact]
		public void WriteKeyword_WritesKeyword()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteKeywordTest(SqlKeywords.Select);
			}
			Assert.Equal("SELECT", builder.ToString());
		}

		[Fact]
		public void WriteKeyword_WithWriteKeywordsInLowerCase_WritesKeywordsInLowerCase()
		{
			var builder = new StringBuilder();
			var settings = new SqlWriterSettings
			{
				WriteKeywordsInLowerCase = true
			};

			using (var writer = new TestSqlWriter(builder, settings)) 
			{
				writer.WriteKeywordTest(SqlKeywords.Select);
			}

			Assert.Equal("select", builder.ToString());
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

		private class TestTextWriter : TextWriter, IDisposable
		{
			public override Encoding Encoding
			{
				get
				{
					return Encoding.UTF8;
				}
			}

			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				IsDisposed = true;
			}

			public bool IsDisposed
			{
				get;
				private set;
			}
		}

		[Fact]
		public void Dispose_WithTextWriter_DoesNotDisposeTextWriter()
		{
			var textWriter = new TestTextWriter();
			using (var writer = new SqlWriter(textWriter))
			{
			}

			Assert.False(textWriter.IsDisposed);
		}

		[Fact]
		public void Dispose_WhenCalledTwice_DoesNotThrow()
		{
			var writer = new SqlWriter(new StringBuilder());
			writer.Dispose();
			writer.Dispose();
		}
	}
}
