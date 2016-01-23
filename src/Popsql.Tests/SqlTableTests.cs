using System;
using System.Linq;
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
			Assert.Equal(SqlExpressionType.Column, table.ExpressionType);
		}

		[Fact]
		public void ImplicitConversion_WithAlias_ReturnsColumn()
		{
			var table = new SqlTable("[dbo].[Users]", "u");
			var column = table + "Id";

			Assert.NotNull(column);
			Assert.Equal(2, column.ColumnName.Segments.Length);
			Assert.Equal("u", column.ColumnName.Segments.First());
			Assert.Equal("Id", column.ColumnName.Segments.Last());
		}

		[Fact]
		public void ImplicitConversion_WithoutAlias_ReturnsColumn()
		{
			var table = new SqlTable("[dbo].[Users]");
			var column = table + "Id";

			Assert.NotNull(column);
			Assert.Equal(3, column.ColumnName.Segments.Length);
			Assert.Equal("dbo", column.ColumnName.Segments.First());
			Assert.Equal("Users", column.ColumnName.Segments.Skip(1).First());
			Assert.Equal("Id", column.ColumnName.Segments.Last());
		}
	}
}
