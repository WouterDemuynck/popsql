using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlWhereTests
	{
		[Fact]
		public void ExpressionType_ReturnsWhere()
		{
			var where = new SqlWhere(SqlExpression.Equal("Id", 5));
			Assert.Equal(SqlExpressionType.Where, where.ExpressionType);
		} 
	}
}