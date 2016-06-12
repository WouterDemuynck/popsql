using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlConstantTests
	{
		[Fact]
		public void Ctor_WithValue_SetsValueProperty()
		{
			var value = new object();
			var constant = new SqlConstant(value);

			Assert.Same(value, constant.Value);
		}

		[Fact]
		public void ExpressionType_ReturnsConstant()
		{
			var constant = new SqlConstant(null);

			Assert.Equal(SqlExpressionType.Constant, constant.ExpressionType);
		}

		[Fact]
		public void AdditionOperator_ReturnsParameter()
		{
			var constant = new SqlConstant(5);
			var parameter = "Id" + constant;

			Assert.NotNull(parameter);
			Assert.NotNull(parameter.ParameterName);
			Assert.Equal("Id", parameter.ParameterName);
			Assert.NotNull(parameter.Value);
			Assert.Equal(5, parameter.Value);
		}


		[Fact]
		public void AdditionOperator_WithNullConstant_CreatesNullValuedParameter()
		{
			var constant = (SqlConstant)null;
			Assert.Null(constant);
			Assert.Throws<ArgumentNullException>(() => "Id" + constant);
		}

		[Fact]
		public void GetHashCode_ReturnsHashCodeOfValue()
		{
			string value = "The rain in Spain falls mainly on the plain.";
			int expected = value.GetHashCode();
			int actual = ((SqlConstant) value).GetHashCode();
			
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetHashCode_WithNullValue_ReturnsMinusOne()
		{
			int expected = -1;
			int actual = (new SqlConstant(null)).GetHashCode();

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Equals_WithNullArgument_ReturnsFalse()
		{
			SqlConstant first = new SqlConstant(5);

			Assert.False(first.Equals((object)null));
			Assert.False(first.Equals((SqlConstant)null));
		}

		[Fact]
		public void Equals_WithNullValueAndNullArgument_ReturnsTrue()
		{
			SqlConstant first = new SqlConstant(null);
			SqlConstant second = new SqlConstant(null);

			Assert.True(first.Equals(second));
		}

		[Fact]
		public void Equals_WithArgumentOfDifferentType_ReturnsFalse()
		{
			SqlConstant first = new SqlConstant(5);
			string second = "The rain in Spain falls mainly on the plain.";

			Assert.False(first.Equals(second));
		}

		[Fact]
		public void Equals_WithNullValueAndNonNullArgument_ReturnsFalse()
		{
			SqlConstant first = new SqlConstant(null);
			SqlValue second = "The rain in Spain falls mainly on the plain.";

			Assert.False(first.Equals(second));
		}
	}
}
