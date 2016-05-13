using Moq;
using Popsql.Dialects;
using Xunit;

namespace Popsql.Tests.Dialects
{
	public class SqlDialectTests
	{
		[Fact]
		public void Current_WithNullValue_SetsToDefault()
		{
			SqlDialect.Current = null;
			Assert.Same(SqlDialect.Default, SqlDialect.Current);
		}

		[Fact]
		public void Current_WithValue_SetsCurrentDialect()
		{
			var dialect = new Mock<SqlDialect>().Object;
			SqlDialect.Current = dialect;
			Assert.Same(dialect, SqlDialect.Current);

			SqlDialect.Current = SqlDialect.Default;
		}

		[Fact]
		public void FormatTableName_ReturnsFormattedTableName()
		{
			var expected = "[Users]";
			var dialect = new SqlDialect();

			var actual = dialect.FormatIdentifier("Users");

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatParameterName_ReturnsFormattedParameterName()
		{
			var expected = "@Parameter";
			var dialect = new SqlDialect();

			var actual = dialect.FormatParameterName("Parameter");

			Assert.Equal(expected, actual);
		}
	}
}