using System.IO;
using System.Text;
using Popsql.Dialects;
using Popsql.Text;

namespace Popsql.Tests.Mocking
{
	public class TestSqlWriter : SqlWriter
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