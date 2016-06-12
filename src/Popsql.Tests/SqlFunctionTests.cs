using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlFunctionTests
	{
		[Fact]
		public void Ctor_WithNullFunctionName_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlFunction(null, null));
		}

		[Fact]
		public void Ctor_WithNonNullFunctionName_UpdatesFunctionNameProperty()
		{
			var expected = "GETDATE";
			var function = new SqlFunction("GETDATE");

			Assert.Equal(expected, function.FunctionName);
		}

		[Fact]
		public void Ctor_WithArguments_UpdatesArgumentsProperty()
		{
			var expected = new[] { (SqlColumn)"Id" };
			var function = new SqlFunction("Count", expected);

			Assert.NotNull(function.Arguments);
			Assert.Same(expected, function.Arguments);
		}

		[Fact]
		public void ExpressionType_ReturnsFunction()
		{
			var function = new SqlFunction("GETDATE");
			
			Assert.Equal(SqlExpressionType.Function, function.ExpressionType);
		}
	}
}