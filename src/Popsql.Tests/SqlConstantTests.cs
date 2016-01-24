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
	}
}
