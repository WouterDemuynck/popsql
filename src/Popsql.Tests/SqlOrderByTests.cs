using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlOrderByTests
	{
		[Fact]
		public void ExpressionType_ReturnsOrderBy()
		{
			var orderBy = new SqlOrderBy();
			Assert.Equal(SqlExpressionType.OrderBy, orderBy.ExpressionType);
		}
	}
}