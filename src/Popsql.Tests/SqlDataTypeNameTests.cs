using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlDataTypeNameTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\t")]
		public void Ctor_WithNullOrWhiteSpaceName_ThrowsArgumentNull(string name)
		{
			var ex = Assert.Throws<ArgumentNullException>(() => new SqlDataTypeName(name));
			Assert.Equal(nameof(name), ex.ParamName);
		}

		[Fact]
		public void Ctor_WithKeyword_SetsKeywordProperty()
		{
			var expected = "FOOBAR";
			var keyword = new SqlDataTypeName(expected);

			Assert.Equal(expected, keyword.Name);
		}

		[Fact]
		public void ImplicitConversion_WithKeyword_SetsKeywordProperty()
		{
			var expected = "FOOBAR";
			SqlDataTypeName keyword = expected;

			Assert.Equal(expected, keyword.Name);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\t")]
		public void ImplicitConversion_WithNullOrWhiteSpaceName_ThrowsArgumentNull(string name)
		{
			var ex = Assert.Throws<ArgumentNullException>(() => (SqlDataTypeName)name);
			Assert.Equal(nameof(name), ex.ParamName);
		}
	}
}