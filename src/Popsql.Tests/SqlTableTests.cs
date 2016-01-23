using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlTableTests
	{
		[Fact]
		public void Ctor_WithNullTableName_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlTable(null));
		}

		[Fact]
		public void Ctor_WithEmptyTableName_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlTable(string.Empty));
		}

		[Fact]
		public void Ctor_WithTableName_SetsTableNameProperty()
		{
			var table = new SqlTable("[dbo].[Users]");

			Assert.NotNull(table.TableName);
			Assert.Equal("dbo", table.TableName.Segments[0]);
			Assert.Equal("Users", table.TableName.Segments[1]);
		}

		[Fact]
		public void Ctor_WithTableNameAnAlias_SetsAliasProperty()
		{
			var table = new SqlTable("[dbo].[Users]", "u");

			Assert.NotNull(table.TableName);
			Assert.Equal("dbo", table.TableName.Segments[0]);
			Assert.Equal("Users", table.TableName.Segments[1]);

			Assert.NotNull(table.Alias);
			Assert.Equal("u", table.Alias);
		}

		[Fact]
		public void ExpressionType_ReturnsTable()
		{
			var table = new SqlTable("[dbo].[Users]");
			Assert.Equal(SqlExpressionType.Table, table.ExpressionType);
		}
	}
}
