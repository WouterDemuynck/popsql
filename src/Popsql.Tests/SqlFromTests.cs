using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlFromTests
	{
		[Fact]
		public void ExpressionType_ReturnsFrom()
		{
			var parent = new SqlSelect(Enumerable.Empty<SqlColumn>());
			var from = new TestSqlFrom(parent);

			Assert.Equal(SqlExpressionType.From, from.ExpressionType);
		}

		private class TestSqlFrom : SqlFrom<SqlSelect>
		{
			public TestSqlFrom(SqlSelect parent)
				: base(parent)
			{
			}
		}
	}
}