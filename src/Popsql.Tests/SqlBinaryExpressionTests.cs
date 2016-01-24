using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlBinaryExpressionTests
	{
		[Fact]
		public void Ctor_WithNullLeftOperand_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlBinaryExpression(null, SqlBinaryOperator.Equal, (SqlConstant)5));
		}

		[Fact]
		public void Ctor_WithNullRightOperand_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlBinaryExpression((SqlConstant)5, SqlBinaryOperator.Equal, null));
		}

		[Fact]
		public void Ctor_WithLeftAndRightOperand_SetsPropertyValues()
		{
			var left = new SqlConstant(5);
			var @operator = SqlBinaryOperator.Equal;
			var right = new SqlConstant(1);
			var expression = new SqlBinaryExpression(left, @operator, right);

			Assert.NotNull(expression.Left);
			Assert.Same(left, expression.Left);
			Assert.Equal(@operator, expression.Operator);
			Assert.NotNull(expression.Right);
			Assert.Same(right, expression.Right);
		}

		[Fact]
		public void ExpressionType_ReturnsBinary()
		{
			var expression = new SqlBinaryExpression((SqlConstant)1, SqlBinaryOperator.Equal, (SqlConstant)1);
			Assert.Equal(SqlExpressionType.Binary, expression.ExpressionType);
		}
	}
}
