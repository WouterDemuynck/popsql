using Xunit;

namespace Popsql.Tests
{
	public class SqlSelectTests
	{
		[Fact]
		public void ExpressionType_ReturnsSelect()
		{
			var query = new SqlSelect();

			Assert.Equal(SqlExpressionType.Select, query.ExpressionType);
		}
	}
}
