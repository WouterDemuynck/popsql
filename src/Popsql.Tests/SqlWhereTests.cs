using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlWhereTests
	{
		[Fact]
		public void ExpressionType_ReturnsWhere()
		{
			var where = new SqlWhere<SqlSelect>(new SqlSelect(Enumerable.Empty<SqlColumn>()));
			Assert.Equal(SqlExpressionType.Where, where.ExpressionType);
		} 
	}
}