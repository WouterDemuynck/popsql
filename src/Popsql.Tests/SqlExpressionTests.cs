using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Popsql.Visitors;
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

		[Fact]
		public void Accept_WithNullVisitor_ThrowsArgumentNull()
		{
			var expression = new Mock<SqlExpression> { CallBase = true }.Object;
			Assert.Throws<ArgumentNullException>(() => expression.Accept(null));
		}

		[Fact]
		public void And_WithNullLeftOperand_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.And(null, null));
		}

		[Fact]
		public void And_WithNullRightOperand_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.And(SqlExpression.Equal("Id", 5), null));
		}

		[Fact]
		public void And_WithLeftAndRightOperands_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.And(SqlExpression.GreaterThan("Id", 5), SqlExpression.Equal("Name", "Wouter"));

			Assert.NotNull(expression);
			Assert.Equal(SqlBinaryOperator.And, expression.Operator);

			Assert.IsType<SqlBinaryExpression>(expression.Left);
			Assert.Equal(SqlBinaryOperator.GreaterThan, ((SqlBinaryExpression)expression.Left).Operator);

			Assert.IsType<SqlBinaryExpression>(expression.Right);
			Assert.Equal(SqlBinaryOperator.Equal, ((SqlBinaryExpression)expression.Right).Operator);
		}

		[Fact]
		public void Or_WithNullLeftOperand_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.Or(null, null));
		}

		[Fact]
		public void Or_WithNullRightOperand_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.Or(SqlExpression.Equal("Id", 5), null));
		}

		[Fact]
		public void Or_WithLeftOrRightOperands_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.Or(SqlExpression.GreaterThan("Id", 5), SqlExpression.Equal("Name", "Wouter"));

			Assert.NotNull(expression);
			Assert.Equal(SqlBinaryOperator.Or, expression.Operator);

			Assert.IsType<SqlBinaryExpression>(expression.Left);
			Assert.Equal(SqlBinaryOperator.GreaterThan, ((SqlBinaryExpression)expression.Left).Operator);

			Assert.IsType<SqlBinaryExpression>(expression.Right);
			Assert.Equal(SqlBinaryOperator.Equal, ((SqlBinaryExpression)expression.Right).Operator);
		}

		[Fact]
		public void Equal_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.Equal(null, null));
		}

		[Fact]
		public void Equal_WithNullValue_DoesNotThrow()
		{
			SqlExpression.Equal("Id", null);
		}

		[Fact]
		public void Equal_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.Equal("Id", "Id" + (SqlConstant)5);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.Equal, expression.Operator);

			Assert.IsType<SqlParameter>(expression.Right);
			Assert.Equal("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.Equal(5, ((SqlParameter)expression.Right).Value);
		}

		[Fact]
		public void NotEqual_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.NotEqual(null, null));
		}

		[Fact]
		public void NotEqual_WithNullValue_DoesNotThrow()
		{
			SqlExpression.NotEqual("Id", null);
		}

		[Fact]
		public void NotEqual_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.NotEqual("Id", "Id" + (SqlConstant)5);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.NotEqual, expression.Operator);

			Assert.IsType<SqlParameter>(expression.Right);
			Assert.Equal("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.Equal(5, ((SqlParameter)expression.Right).Value);
		}

		[Fact]
		public void GreaterThan_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.GreaterThan(null, null));
		}

		[Fact]
		public void GreaterThan_WithNullValue_DoesNotThrow()
		{
			SqlExpression.GreaterThan("Id", null);
		}

		[Fact]
		public void GreaterThan_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.GreaterThan("Id", "Id" + (SqlConstant)5);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.GreaterThan, expression.Operator);

			Assert.IsType<SqlParameter>(expression.Right);
			Assert.Equal("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.Equal(5, ((SqlParameter)expression.Right).Value);
		}

		[Fact]
		public void GreaterThanOrEqual_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.GreaterThanOrEqual(null, null));
		}

		[Fact]
		public void GreaterThanOrEqual_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.GreaterThanOrEqual("Id", null);
		}

		[Fact]
		public void GreaterThanOrEqual_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.GreaterThanOrEqual("Id", "Id" + (SqlConstant)5);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.GreaterThanOrEqual, expression.Operator);

			Assert.IsType<SqlParameter>(expression.Right);
			Assert.Equal("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.Equal(5, ((SqlParameter)expression.Right).Value);
		}

		[Fact]
		public void LessThan_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.LessThan(null, null));
		}

		[Fact]
		public void LessThan_WithNullValue_DoesNotThrow()
		{
			SqlExpression.LessThan("Id", null);
		}

		[Fact]
		public void LessThan_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.LessThan("Id", "Id" + (SqlConstant)5);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.LessThan, expression.Operator);

			Assert.IsType<SqlParameter>(expression.Right);
			Assert.Equal("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.Equal(5, ((SqlParameter)expression.Right).Value);
		}

		[Fact]
		public void LessThanOrEqual_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.LessThanOrEqual(null, null));
		}

		[Fact]
		public void LessThanOrEqual_WithNullValue_DoesNotThrow()
		{
			SqlExpression.LessThanOrEqual("Id", null);
		}

		[Fact]
		public void LessThanOrEqual_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.LessThanOrEqual("Id", "Id" + (SqlConstant)5);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.LessThanOrEqual, expression.Operator);

			Assert.IsType<SqlParameter>(expression.Right);
			Assert.Equal("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.Equal(5, ((SqlParameter)expression.Right).Value);
		}

		[Fact]
		public void Like_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.Like(null, null));
		}

		[Fact]
		public void Like_WithNullValue_DoesNotThrow()
		{
			SqlExpression.Like("Id", null);
		}

		[Fact]
		public void Like_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.Like("Id", "Id" + (SqlConstant)5);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.Like, expression.Operator);

			Assert.IsType<SqlParameter>(expression.Right);
			Assert.Equal("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.Equal(5, ((SqlParameter)expression.Right).Value);
		}

		[Fact]
		public void In_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.In(null, (SqlValue[])null));
		}

		[Fact]
		public void In_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.In("Id", (SqlValue[])null);
		}

		[Fact]
		public void In_WithNullSubquery_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlExpression.In("Id", (SqlSubquery)null));
		}

		[Fact]
		public void In_WithColumnAndValues_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.In("Id", 1, 2, 3, 4, 5, 6);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.In, expression.Operator);

			Assert.IsType<SqlValueList>(expression.Right);
			Assert.IsAssignableFrom<IEnumerable<SqlValue>>(((SqlValueList)expression.Right).Values);

			var values = ((SqlValueList)expression.Right).Values.ToArray();
			Assert.Equal(6, values.Length);
			Assert.Equal(1, values[0]);
			Assert.Equal(2, values[1]);
			Assert.Equal(3, values[2]);
			Assert.Equal(4, values[3]);
			Assert.Equal(5, values[4]);
			Assert.Equal(6, values[5]);
		}

		[Fact]
		public void In_WithColumnAndSubquery_ReturnsCorrectExpression()
		{
			var subquery = new SqlSubquery(Sql.Select("Age").From("Profile").Go(), "a");
			var expression = SqlExpression.In("Id", subquery);

			Assert.IsType<SqlColumn>(expression.Left);
			Assert.Equal("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.Equal(SqlBinaryOperator.In, expression.Operator);

			Assert.IsType<SqlSubquery>(expression.Right);
			Assert.Same(subquery, expression.Right);
		}
	}
}
