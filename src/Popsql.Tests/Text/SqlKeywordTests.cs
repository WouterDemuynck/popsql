using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Popsql.Text;
using Xunit;

namespace Popsql.Tests.Text
{
	public class SqlKeywordTests
	{
		[Fact]
		public void Ctor_WithNullKeyword_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlKeyword(null));
		}

		[Fact]
		public void Ctor_WithEmptyKeyword_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlKeyword(string.Empty));
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

		[Fact]
		public void ImplicitConversion_WithNullKeyword_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => (SqlKeyword) (string)null);
		}
	}
}
