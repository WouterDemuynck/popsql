using Popsql.Text;
using Xunit;

namespace Popsql.Tests.Text
{
	public class SqlDialectTests
	{
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
