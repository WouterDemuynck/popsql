using System.Text;
using Moq;
using Popsql.Dialects;
using Popsql.Tests.Mocking;
using Xunit;

namespace Popsql.Tests.Dialects
{
	public class MySqlDialectTests
	{
		[Fact]
		public void FormatIdentifier_ReturnsFormattedTableName()
		{
			var expected = "`Users`";
			var dialect = new MySqlDialect();

			var actual = dialect.FormatIdentifier("Users");

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatParameterName_ReturnsFormattedParameterName()
		{
			var expected = "?Parameter";
			var dialect = new MySqlDialect();

			var actual = dialect.FormatParameterName("Parameter");

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void WriteFetchFirst_WritesLimit()
		{
			var expected = "LIMIT 42, 10";
			var dialect = new MySqlDialect();
			var builder = new StringBuilder();

			using (TestSqlWriter writer = new TestSqlWriter(builder, dialect))
			{
				dialect.WriteFetchFirst(writer, 42, 10);
			}

			var actual = builder.ToString();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void WriteFetchFirst_WithNullOffset_WritesLimitWithCountOnly()
		{
			var expected = "LIMIT 10";
			var dialect = new MySqlDialect();
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