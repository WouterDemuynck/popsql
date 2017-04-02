using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlParameterTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\t")]
		public void Ctor_WithNullOrWhiteSpaceParameterName_ThrowsArgumentNull(string parameterName)
		{
			var ex = Assert.Throws<ArgumentNullException>(() => new SqlParameter(null, null));
			Assert.Equal(nameof(parameterName), ex.ParamName);
		}

		[Fact]
		public void Ctor_WithParameterNameAndValue_SetsPropertyValues()
		{
			var value = Guid.NewGuid();
			var parameter = new SqlParameter("Id", value);

			Assert.NotNull(parameter.ParameterName);
			Assert.Equal("Id", parameter.ParameterName);
			Assert.NotNull(parameter.Value);
			Assert.Equal(value, parameter.Value);
		}

		[Fact]
		public void ExpressionType_ReturnsParameter()
		{
			var parameter = new SqlParameter("Id", 5);
			Assert.Equal(SqlExpressionType.Parameter, parameter.ExpressionType);
		}
	}
}
