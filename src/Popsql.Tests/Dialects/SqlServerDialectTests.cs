using System.Text;
using Popsql.Dialects;
using Popsql.Tests.Mocking;
using Xunit;

namespace Popsql.Tests.Dialects
{
	public class SqlServerDialectTests
	{
		[Fact]
		public void WriteFetchFirst_WritesOffsetFetch()
		{
			var expected = "OFFSET 42 ROWS FETCH FIRST 10 ROWS ONLY";
			var dialect = new SqlServerDialect();
			var builder = new StringBuilder();

			using (TestSqlWriter writer = new TestSqlWriter(builder, dialect))
			{
				dialect.WriteFetchFirst(writer, 42, 10);
			}

			var actual = builder.ToString();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void WriteFetchFirst_WithNullOffset_WritesZeroOffset()
		{
			var expected = "OFFSET 0 ROWS FETCH FIRST 10 ROWS ONLY";
			var dialect = new SqlServerDialect();
			var builder = new StringBuilder();

			using (TestSqlWriter writer = new TestSqlWriter(builder, dialect))
			{
				dialect.WriteFetchFirst(writer, null, 10);
			}

			var actual = builder.ToString();
			Assert.Equal(expected, actual);
		}
	}
}