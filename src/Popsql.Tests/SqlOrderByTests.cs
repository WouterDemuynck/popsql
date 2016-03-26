using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlOrderByTests
	{
		[Fact]
		public void ExpressionType_ReturnsOrderBy()
		{
			var orderBy = new SqlOrderBy<SqlSelect>(new SqlSelect(Enumerable.Empty<SqlColumn>()));
			Assert.Equal(SqlExpressionType.OrderBy, orderBy.ExpressionType);
		}
	}
}