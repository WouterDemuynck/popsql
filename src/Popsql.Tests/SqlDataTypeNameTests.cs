using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

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

		private static readonly SqlDataTypeName self = new SqlDataTypeName("FOOBAR");

		[Theory]
		[MemberData(nameof(GetEqualityComparisonData), false)]
		public void Equals_WithObject_ReturnsCorrectly(object other, bool expected)
		{
			Assert.Equal(expected, self.Equals(other));
		}

		[Theory]
		[MemberData(nameof(GetEqualityComparisonData), true)]
		public void Equals_WithSqlDataTypeName_ReturnsCorrectly(SqlDataTypeName other, bool expected)
		{
			Assert.Equal(expected, self.Equals(other));
		}

		[Theory]
		[InlineData("FOOBAR")]
		[InlineData("MARVIN")]
		[InlineData("DATA")]
		public void GetHashCode_ReturnsHashCodeOfNameProperty(string name)
		{
			var dataType = new SqlDataTypeName(name);
			Assert.Equal(name.GetHashCode(), dataType.GetHashCode());
		}

		public static IEnumerable<object[]> GetEqualityComparisonData(bool exactTypeOnly)
		{
			if (!exactTypeOnly)
			{
				yield return new object[] { "FOOBAR", false };
			}
			yield return new object[] { (SqlDataTypeName)null, false };
			yield return new object[] { new SqlDataTypeName("NONE"), false };
			yield return new object[] { new SqlDataTypeName("FOOBAR"), true };
			yield return new object[] { self, true };
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