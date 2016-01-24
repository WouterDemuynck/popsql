using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Popsql.Tests
{
	public class SqlExpressionTests
	{
		[Fact]
		public void Equal_WithNullLeftOperand_ThrowsArgumentNull()
		{
			var right = new SqlConstant(5);
			Assert.Throws<ArgumentNullException>(() => SqlExpression.Equal(null, right));
		}

		[Fact]
		public void Equal_WithNullRightOperand_ReturnsBinaryExpression()
		{
			var left = new SqlColumn("dbo.Users.Id");
			var expression = SqlExpression.Equal(left, null);

			Assert.NotNull(expression.Left);
			Assert.Same(left, expression.Left);
			Assert.Equal(SqlBinaryOperator.Equal, expression.Operator);
			Assert.NotNull(expression.Right);
			Assert.Same(SqlConstant.Null, expression.Right);
		}

		[Fact]
		public void Equal_WithLeftAndRightOperand_ReturnsBinaryExpression()
		{
			var left = new SqlColumn("dbo.Users.Id");
			var right = new SqlConstant(5);
			var expression = SqlExpression.Equal(left, right);

			Assert.NotNull(expression.Left);
			Assert.Same(left, expression.Left);
			Assert.Equal(SqlBinaryOperator.Equal, expression.Operator);
			Assert.NotNull(expression.Right);
			Assert.Same(right, expression.Right);
		}
	}
}
