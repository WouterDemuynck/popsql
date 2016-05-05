using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlFromTests
	{
		[Fact]
		public void Ctor_WithNullTable_ThrowArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlFrom(null));
		}

		[Fact]
		public void ExpressionType_ReturnsFrom()
		{
			var from = new SqlFrom((SqlTable)"Users");

			Assert.Equal(SqlExpressionType.From, from.ExpressionType);
		}
	}
}