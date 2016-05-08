using Moq;
using Popsql.Dialects;
using Xunit;

namespace Popsql.Tests.Dialects
{
	public class MySqlDialectTests
	{
		[Fact]
		public void FormatTableName_ReturnsFormattedTableName()
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
	}
}