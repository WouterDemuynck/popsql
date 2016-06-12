using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlIdentifierParserTests
	{
		[Fact]
		public void Parse_WithNullIdentifier_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlIdentifierParser.Parse(null));
		}

		[Fact]
		public void Parse_WithEmptyIdentifier_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => SqlIdentifierParser.Parse(string.Empty));
		}

		[Fact]
		public void Parse_WithSingleSegment_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("Users");

			Assert.NotNull(segments);
			Assert.Equal(1, segments.Length);
			Assert.Equal("Users", segments[0]);
		}

		[Fact]
		public void Parse_WithSchema_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("dbo.Users");

			Assert.NotNull(segments);
			Assert.Equal(2, segments.Length);
			Assert.Equal("dbo", segments[0]);
			Assert.Equal("Users", segments[1]);
		}

		[Fact]
		public void Parse_WithSchemaAndDatabase_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("Blog.dbo.Users");

			Assert.NotNull(segments);
			Assert.Equal(3, segments.Length);
			Assert.Equal("Blog", segments[0]);
			Assert.Equal("dbo", segments[1]);
			Assert.Equal("Users", segments[2]);
		}

		[Fact]
		public void Parse_WithQuotedIdentifier_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("\"Blog\".\"dbo\".\"Users\"");

			Assert.NotNull(segments);
			Assert.Equal(3, segments.Length);
			Assert.Equal("Blog", segments[0]);
			Assert.Equal("dbo", segments[1]);
			Assert.Equal("Users", segments[2]);
		}

		[Fact]
		public void Parse_WithQuotedIdentifierContainingDots_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("\"Blog.v1.3\".\"dbo\".\"Users.v1.0\"");

			Assert.NotNull(segments);
			Assert.Equal(3, segments.Length);
			Assert.Equal("Blog.v1.3", segments[0]);
			Assert.Equal("dbo", segments[1]);
			Assert.Equal("Users.v1.0", segments[2]);
		}

		[Fact]
		public void Parse_WithQuotedIdentifierContainingQuotes_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("\"\"\"Blog\"\"\".\"dbo\".\"Users-\"\"alpha\"\"\"");

			Assert.NotNull(segments);
			Assert.Equal(3, segments.Length);
			Assert.Equal("\"Blog\"", segments[0]);
			Assert.Equal("dbo", segments[1]);
			Assert.Equal("Users-\"alpha\"", segments[2]);
		}

		[Fact]
		public void Parse_WithDelimitedIdentifier_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("[Blog].[dbo].[Users]");

			Assert.NotNull(segments);
			Assert.Equal(3, segments.Length);
			Assert.Equal("Blog", segments[0]);
			Assert.Equal("dbo", segments[1]);
			Assert.Equal("Users", segments[2]);
		}

		[Fact]
		public void Parse_WithDelimitedIdentifierContainingDots_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("[Blog.v2.0].[dbo].[Users.v1.1]");

			Assert.NotNull(segments);
			Assert.Equal(3, segments.Length);
			Assert.Equal("Blog.v2.0", segments[0]);
			Assert.Equal("dbo", segments[1]);
			Assert.Equal("Users.v1.1", segments[2]);
		}

		[Fact]
		public void Parse_WithDelimitedIdentifierContainingBrackets_ParsesCorrecly()
		{
			var segments = SqlIdentifierParser.Parse("[Blog]]].[[[dbo].[[[Users]]]]]");

			Assert.NotNull(segments);
			Assert.Equal(3, segments.Length);
			Assert.Equal("Blog]", segments[0]);
			Assert.Equal("[[dbo", segments[1]);
			Assert.Equal("[[Users]]", segments[2]);
		}
	}
}
