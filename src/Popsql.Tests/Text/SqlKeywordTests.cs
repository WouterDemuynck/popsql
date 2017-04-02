using System;
using Popsql.Text;
using Xunit;

namespace Popsql.Tests.Text
{
	public class SqlKeywordTests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\t")]
		public void Ctor_WithNullOrWhiteSpaceKeyword_ThrowsArgumentNull(string keyword)
		{
			var ex = Assert.Throws<ArgumentNullException>(() => new SqlKeyword(keyword));
			Assert.Equal(nameof(keyword), ex.ParamName);
		}

		[Fact]
		public void Ctor_WithKeyword_SetsKeywordProperty()
		{
			var expected = "FOOBAR";
			var keyword = new SqlKeyword(expected);

			Assert.Equal(expected, keyword.Keyword);
		}

		[Fact]
		public void ImplicitConversion_WithKeyword_SetsKeywordProperty()
		{
			var expected = "FOOBAR";
			SqlKeyword keyword = expected;

			Assert.Equal(expected, keyword.Keyword);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\t")]
		public void ImplicitConversion_WithNullOrWhiteSpaceKeyword_ThrowsArgumentNull(string keyword)
		{
			var ex = Assert.Throws<ArgumentNullException>(() => (SqlKeyword)keyword);
			Assert.Equal(nameof(keyword), ex.ParamName);
		}
	}
}
