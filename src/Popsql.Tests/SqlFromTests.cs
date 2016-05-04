using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlFromTests
	{
		[Fact]
		public void ExpressionType_ReturnsFrom()
		{
			var from = new SqlFrom((SqlTable)"Users");

			Assert.Equal(SqlExpressionType.From, from.ExpressionType);
		}

		private class TestSqlFrom : OwnedBy<SqlSelect>
		{
			public TestSqlFrom(SqlSelect parent)
				: base(parent)
			{
			}
		}
	}
}