using Microsoft.VisualStudio.TestTools.UnitTesting;
using Popsql.Tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql.Tests
{
	[TestClass]
	public class SqlExpressionTests
	{
		[TestMethod]
		public void And_WithNullLeftOperand_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.And(null, null));
		}

		[TestMethod]
		public void And_WithNullRightOperand_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.And(SqlExpression.Equal("Id", 5), null));
		}

		[TestMethod]
		public void And_WithLeftAndRightOperands_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.And(SqlExpression.GreaterThan("Id", 5), SqlExpression.Equal("Name", "Wouter"));

			Assert.IsNotNull(expression);
			Assert.AreEqual(SqlBinaryOperator.And, expression.Operator);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlBinaryExpression));
			Assert.AreEqual(SqlBinaryOperator.GreaterThan, ((SqlBinaryExpression)expression.Left).Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlBinaryExpression));
			Assert.AreEqual(SqlBinaryOperator.Equal, ((SqlBinaryExpression)expression.Right).Operator);
		}

		[TestMethod]
		public void Or_WithNullLeftOperand_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.Or(null, null));
		}

		[TestMethod]
		public void Or_WithNullRightOperand_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.Or(SqlExpression.Equal("Id", 5), null));
		}

		[TestMethod]
		public void Or_WithLeftOrRightOperands_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.Or(SqlExpression.GreaterThan("Id", 5), SqlExpression.Equal("Name", "Wouter"));

			Assert.IsNotNull(expression);
			Assert.AreEqual(SqlBinaryOperator.Or, expression.Operator);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlBinaryExpression));
			Assert.AreEqual(SqlBinaryOperator.GreaterThan, ((SqlBinaryExpression)expression.Left).Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlBinaryExpression));
			Assert.AreEqual(SqlBinaryOperator.Equal, ((SqlBinaryExpression)expression.Right).Operator);
		}

		[TestMethod]
		public void Equal_WithNullColumn_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.Equal(null, null));
		}

		[TestMethod]
		public void Equal_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.Equal("Id", null);
		}

		[TestMethod]
		public void Equal_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.Equal("Id", "Id" + (SqlConstant)5);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlColumn));
			Assert.AreEqual("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.AreEqual(SqlBinaryOperator.Equal, expression.Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlParameter));
			Assert.AreEqual("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.AreEqual(5, ((SqlParameter)expression.Right).Value);
		}

		[TestMethod]
		public void NotEqual_WithNullColumn_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.NotEqual(null, null));
		}

		[TestMethod]
		public void NotEqual_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.NotEqual("Id", null);
		}

		[TestMethod]
		public void NotEqual_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.NotEqual("Id", "Id" + (SqlConstant)5);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlColumn));
			Assert.AreEqual("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.AreEqual(SqlBinaryOperator.NotEqual, expression.Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlParameter));
			Assert.AreEqual("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.AreEqual(5, ((SqlParameter)expression.Right).Value);
		}

		[TestMethod]
		public void GreaterThan_WithNullColumn_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.GreaterThan(null, null));
		}

		[TestMethod]
		public void GreaterThan_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.GreaterThan("Id", null);
		}

		[TestMethod]
		public void GreaterThan_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.GreaterThan("Id", "Id" + (SqlConstant)5);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlColumn));
			Assert.AreEqual("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.AreEqual(SqlBinaryOperator.GreaterThan, expression.Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlParameter));
			Assert.AreEqual("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.AreEqual(5, ((SqlParameter)expression.Right).Value);
		}

		[TestMethod]
		public void GreaterThanOrEqual_WithNullColumn_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.GreaterThanOrEqual(null, null));
		}

		[TestMethod]
		public void GreaterThanOrEqual_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.GreaterThanOrEqual("Id", null);
		}

		[TestMethod]
		public void GreaterThanOrEqual_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.GreaterThanOrEqual("Id", "Id" + (SqlConstant)5);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlColumn));
			Assert.AreEqual("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.AreEqual(SqlBinaryOperator.GreaterThanOrEqual, expression.Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlParameter));
			Assert.AreEqual("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.AreEqual(5, ((SqlParameter)expression.Right).Value);
		}

		[TestMethod]
		public void LessThan_WithNullColumn_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.LessThan(null, null));
		}

		[TestMethod]
		public void LessThan_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.LessThan("Id", null);
		}

		[TestMethod]
		public void LessThan_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.LessThan("Id", "Id" + (SqlConstant)5);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlColumn));
			Assert.AreEqual("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.AreEqual(SqlBinaryOperator.LessThan, expression.Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlParameter));
			Assert.AreEqual("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.AreEqual(5, ((SqlParameter)expression.Right).Value);
		}

		[TestMethod]
		public void LessThanOrEqual_WithNullColumn_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.LessThanOrEqual(null, null));
		}

		[TestMethod]
		public void LessThanOrEqual_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.LessThanOrEqual("Id", null);
		}

		[TestMethod]
		public void LessThanOrEqual_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.LessThanOrEqual("Id", "Id" + (SqlConstant)5);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlColumn));
			Assert.AreEqual("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.AreEqual(SqlBinaryOperator.LessThanOrEqual, expression.Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlParameter));
			Assert.AreEqual("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.AreEqual(5, ((SqlParameter)expression.Right).Value);
		}

		[TestMethod]
		public void Like_WithNullColumn_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.Like(null, null));
		}

		[TestMethod]
		public void Like_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.Like("Id", null);
		}

		[TestMethod]
		public void Like_WithColumnAndValue_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.Like("Id", "Id" + (SqlConstant)5);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlColumn));
			Assert.AreEqual("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.AreEqual(SqlBinaryOperator.Like, expression.Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlParameter));
			Assert.AreEqual("Id", ((SqlParameter)expression.Right).ParameterName);
			Assert.AreEqual(5, ((SqlParameter)expression.Right).Value);
		}

		[TestMethod]
		public void In_WithNullColumn_ThrowsArgumentNull()
		{
			AssertEx.Throws<ArgumentNullException>(() => SqlExpression.In(null, null));
		}

		[TestMethod]
		public void In_WithNullValue_DoesNotThrow()
		{
			var expression = SqlExpression.In("Id", null);
		}

		[TestMethod]
		public void In_WithColumnAndValues_ReturnsCorrectExpression()
		{
			var expression = SqlExpression.In("Id", 1, 2, 3, 4, 5, 6);

			Assert.IsInstanceOfType(expression.Left, typeof(SqlColumn));
			Assert.AreEqual("Id", ((SqlColumn)expression.Left).ColumnName);

			Assert.AreEqual(SqlBinaryOperator.In, expression.Operator);

			Assert.IsInstanceOfType(expression.Right, typeof(SqlConstant));
			Assert.IsInstanceOfType(((SqlConstant)expression.Right).Value, typeof(IEnumerable<SqlValue>));

			var values = ((IEnumerable<SqlValue>) ((SqlConstant) expression.Right).Value).ToArray();
			Assert.AreEqual(6, values.Length);
			Assert.AreEqual(1, values[0]);
			Assert.AreEqual(2, values[1]);
			Assert.AreEqual(3, values[2]);
			Assert.AreEqual(4, values[3]);
			Assert.AreEqual(5, values[4]);
			Assert.AreEqual(6, values[5]);
		}
	}
}
