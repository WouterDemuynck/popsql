using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlIdentifierTests
	{
		[Fact]
		public void Ctor_WithNullSegments_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlIdentifier((string[])null));
		}

		[Fact]
		public void Ctor_WithNullIdentifier_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlIdentifier((string)null));
		}

		[Fact]
		public void Ctor_WithEmptyIdentifier_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlIdentifier(string.Empty));
		}

		[Fact]
		public void ExpressionType_ReturnsSqlIdentifier()
		{
			var identifier = new SqlIdentifier(new[] { "Blog", "dbo", "Users" });

			Assert.Equal(SqlExpressionType.Identifier, identifier.ExpressionType);
		}

		[Fact]
		public void Ctor_WithSegments_SetsSegmentsProperty()
		{
			var identifier = new SqlIdentifier(new[] { "Blog", "dbo", "Users" });

			Assert.NotNull(identifier.Segments);
			Assert.Equal(3, identifier.Segments.Length);
			Assert.Equal("Blog", identifier.Segments[0]);
			Assert.Equal("dbo", identifier.Segments[1]);
			Assert.Equal("Users", identifier.Segments[2]);
		}

		[Fact]
		public void Ctor_WithIdentifier_SetsSegmentsProperty()
		{
			var identifier = new SqlIdentifier("Blog.dbo.Users");

			Assert.NotNull(identifier.Segments);
			Assert.Equal(3, identifier.Segments.Length);
			Assert.Equal("Blog", identifier.Segments[0]);
			Assert.Equal("dbo", identifier.Segments[1]);
			Assert.Equal("Users", identifier.Segments[2]);
		}

		[Fact]
		public void Ctor_WithQuotedIdentifier_SetsSegmentsProperty()
		{
			var identifier = new SqlIdentifier("\"Blog\".\"dbo\".\"Users\"");

			Assert.NotNull(identifier.Segments);
			Assert.Equal(3, identifier.Segments.Length);
			Assert.Equal("Blog", identifier.Segments[0]);
			Assert.Equal("dbo", identifier.Segments[1]);
			Assert.Equal("Users", identifier.Segments[2]);
		}

		[Fact]
		public void Ctor_WithDelimitedIdentifier_SetsSegmentsProperty()
		{
			var identifier = new SqlIdentifier("[Blog].[dbo].[Users]");

			Assert.NotNull(identifier.Segments);
			Assert.Equal(3, identifier.Segments.Length);
			Assert.Equal("Blog", identifier.Segments[0]);
			Assert.Equal("dbo", identifier.Segments[1]);
			Assert.Equal("Users", identifier.Segments[2]);
		}

		[Fact]
		public void Ctor_WithEscapedIdentifier_SetsSegmentsProperty()
		{
			var identifier = new SqlIdentifier("[Bl[og].\"db\"\"o\".[Us]]ers].[Em\"ail]");

			Assert.NotNull(identifier.Segments);
			Assert.Equal(4, identifier.Segments.Length);
			Assert.Equal("Bl[og", identifier.Segments[0]);
			Assert.Equal("db\"o", identifier.Segments[1]);
			Assert.Equal("Us]ers", identifier.Segments[2]);
			Assert.Equal("Em\"ail", identifier.Segments[3]);
		}

		[Fact]
		public void Ctor_WithEdgeCasesIdentifier_SetsSegmentsProperty()
		{
			var identifier = new SqlIdentifier("[[Blog]]].\"[dbo]\".[\"Users\"]");

			Assert.NotNull(identifier.Segments);
			Assert.Equal(3, identifier.Segments.Length);
			Assert.Equal("[Blog]", identifier.Segments[0]);
			Assert.Equal("[dbo]", identifier.Segments[1]);
			Assert.Equal("\"Users\"", identifier.Segments[2]);
		}

		[Fact]
		public void ImplicitConversion_ReturnsCorrectIdentifier()
		{
			var identifier = (SqlIdentifier)("[[Blog.5.5]]].\"[dbo]\".[\"Users\"]");

			Assert.NotNull(identifier.Segments);
			Assert.Equal(3, identifier.Segments.Length);
			Assert.Equal("[Blog.5.5]", identifier.Segments[0]);
			Assert.Equal("[dbo]", identifier.Segments[1]);
			Assert.Equal("\"Users\"", identifier.Segments[2]);
		}

		[Fact]
		public void GetHashCode_WithEqualArgument_ReturnsSameHashCode()
		{
			int expected = new SqlIdentifier("[a].[ab].[abc]").GetHashCode();
			int actual = new SqlIdentifier("a.ab.abc").GetHashCode();
			
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GetHashCode_WithDifferentArgument_ReturnsDifferentHashCode()
		{
			int expected = new SqlIdentifier("[a].[ab]").GetHashCode();
			int actual = new SqlIdentifier("a.ab.abc").GetHashCode();

			Assert.NotEqual(expected, actual);
		}

		[Fact]
		public void Equals_WithNullArgument_ReturnsFalse()
		{
			SqlIdentifier first = new SqlIdentifier("a.ab");

			Assert.False(first.Equals((object)null));
			Assert.False(first.Equals((SqlIdentifier)null));
		}

		[Fact]
		public void Equals_WithDifferentArgument_ReturnsFalse()
		{
			SqlIdentifier first = new SqlIdentifier("a.ab");
			SqlIdentifier second = new SqlIdentifier("b.ab");

			Assert.False(first.Equals(second));
			Assert.False(first.Equals((object)second));
		}

		[Fact]
		public void Equals_WithEqualArgument_ReturnsTrue()
		{
			int expected = new SqlIdentifier("[a].[ab].[abc]").GetHashCode();
			int actual = new SqlIdentifier("a.ab.abc").GetHashCode();

			Assert.True(expected.Equals(actual));
			Assert.True(expected.Equals((object)actual));
		}

		[Fact]
		public void Equals_WithArgumentOfDifferentType_ReturnsFalse()
		{
			SqlIdentifier first = new SqlIdentifier("a.ab");
			string second = "The rain in Spain falls mainly on the plain.";

			Assert.False(first.Equals(second));
			Assert.False(first.Equals((object)second));
		}
	}
}
