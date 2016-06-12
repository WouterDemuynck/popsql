using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlOnTests
	{
		[Fact]
		public void Ctor_WithNullPredicate_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlOn(null));
		}

		[Fact]
		public void ExpressionType_ReturnsOn()
		{
			var on = new SqlOn(SqlExpression.Equal("Id", 5));
			Assert.Equal(SqlExpressionType.On, on.ExpressionType);
		}
	}
}