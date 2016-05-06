using Popsql.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
		public void WriteSortOrder_WithInvalidSortOrder_ThrowsInvalidEnumArgument()
		{
			Assert.Throws<InvalidEnumArgumentException>(() =>
			{
				var builder = new StringBuilder();
				using (var writer = new SqlWriter(builder))
				{
					writer.WriteSortOrder((SqlSortOrder)int.MaxValue);
				}
			});
		}

		[Fact]
		public void WriteSortOrder_WithWriteAscendingSortOrder_WritesSortOrder()
		{
			var sortOrders = new[] {SqlSortOrder.Ascending, SqlSortOrder.Descending };
			var expected = new[] { "ASC", "DESC" };

			for (int index = 0; index < sortOrders.Length; index++)
			{
				var builder = new StringBuilder();
				using (var writer = new SqlWriter(builder, new SqlWriterSettings { WriteAscendingSortOrder = true }))
				{
					writer.WriteSortOrder(sortOrders[index]);
				}

				Assert.Equal(expected[index], builder.ToString());
			}
		}

		[Fact]
		public void WriteSortOrder_WithoutWriteAscendingSortOrder_WritesSortOrder()
		{
			var sortOrders = new[] { SqlSortOrder.Ascending, SqlSortOrder.Descending };
			var expected = new[] { "", "DESC" };

			for (int index = 0; index < sortOrders.Length; index++)
			{
				var builder = new StringBuilder();
				using (var writer = new SqlWriter(builder))
				{
					writer.WriteSortOrder(sortOrders[index]);
				}

				Assert.Equal(expected[index], builder.ToString());
			}
		}

		[Fact]
		public void WriteValue_WithNullValue_WritesNull()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteValue(null);
			}
			Assert.Equal("NULL", builder.ToString());
		}

		[Fact]
		public void WriteValue_WithInteger_WritesInteger()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteValue(long.MaxValue);
			}
			Assert.Equal(long.MaxValue.ToString(CultureInfo.InvariantCulture), builder.ToString());
		}

		[Fact]
		public void WriteValue_WithFloatingPoint_WritesFloatingPoint()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteValue(Math.PI);
			}
			Assert.Equal(Math.PI.ToString(CultureInfo.InvariantCulture), builder.ToString());
		}

		[Fact]
		public void WriteValue_WithString_WritesString()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteValue("The rain in spain falls mainly on the plain.");
			}
			Assert.Equal("'The rain in spain falls mainly on the plain.'", builder.ToString());
		}

		[Fact]
		public void WriteValue_WithOther_WritesDouble()
		{
			var value = new { id = 5 };
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteValue(value);
			}
			Assert.Equal($"'{Convert.ToString(value, CultureInfo.InvariantCulture)}'", builder.ToString());
		}

		[Fact]
		public void WriteKeyword_WritesKeyword()
		{
			var builder = new StringBuilder();
			using (var writer = new TestSqlWriter(builder))
			{
				writer.WriteKeyword(SqlKeywords.Select);
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
				writer.WriteKeyword(SqlKeywords.Select);
			}

			Assert.Equal("select", builder.ToString());
		}

		[Fact]
		public void WriteIdentifier_WithNullIdentifier_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var builder = new StringBuilder();
				using (var writer = new SqlWriter(builder))
				{
					writer.WriteIdentifier(null);
				}
			});
		}

		[Fact]
		public void WriteIdentifier_WritesIdentifier()
		{
			var builder = new StringBuilder();
			using (var writer = new SqlWriter(builder))
			{
				writer.WriteIdentifier("Blog.dbo.User");
			}

			Assert.Equal("[Blog].[dbo].[User]", builder.ToString());
		}

		[Fact]
		public void WriteOperator_WritesOperator()
		{
			var operators = new[] { SqlBinaryOperator.And, SqlBinaryOperator.Equal, SqlBinaryOperator.GreaterThan, SqlBinaryOperator.GreaterThanOrEqual, SqlBinaryOperator.LessThan, SqlBinaryOperator.LessThanOrEqual, SqlBinaryOperator.Like, SqlBinaryOperator.NotEqual, SqlBinaryOperator.Or, SqlBinaryOperator.In };
			var expected = new[] { "AND", "=", ">", ">=", "<", "<=", "LIKE", "<>", "OR", "IN" };

			for (int index = 0; index < operators.Length; index++)
			{
				var builder = new StringBuilder();
				using (var writer = new SqlWriter(builder))
				{
					writer.WriteOperator(operators[index]);
				}

				Assert.Equal(expected[index], builder.ToString());
			}
		}

		[Fact]
		public void WriteOperator_WithInvalidOperator_ThrowsInvalidEnumArgument()
		{
			Assert.Throws<InvalidEnumArgumentException>(() =>
			{
				var builder = new StringBuilder();
				using (var writer = new SqlWriter(builder))
				{
					writer.WriteOperator((SqlBinaryOperator)int.MaxValue);
				}
			});
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
				=> Encoding.UTF8;

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
				=> Dialect;

			public SqlWriterSettings SettingsTest
				=> Settings;

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
	}
}
