using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlJoinTests
	{
		[Fact]
		public void Ctor_WithNullTable_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlJoin(SqlJoinType.Right, null));
		}

		[Fact]
		public void Ctor_WithTableAndPredicate_SetsPropertyValues()
		{
			var table = new SqlTable("dbo.Users", "u");
			var predicate = SqlExpression.Equal("u.Id", 5);
			var expression = new SqlJoin(SqlJoinType.Left, table, predicate);

			Assert.NotNull(expression.Table);
			Assert.Same(table, expression.Table);
			Assert.NotNull(expression.On);
			Assert.NotNull(expression.On.Predicate);
			Assert.Same(predicate, expression.On.Predicate);
			Assert.Equal(SqlJoinType.Left, expression.Type);
		}

		[Fact]
		public void ExpressionType_ReturnsJoin()
		{
			var table = new SqlTable("dbo.Users", "u");
			var predicate = SqlExpression.Equal("u.Id", 5);
			var expression = new SqlJoin(SqlJoinType.Left, table);

			Assert.Equal(SqlExpressionType.Join, expression.ExpressionType);
		}
	}
}
