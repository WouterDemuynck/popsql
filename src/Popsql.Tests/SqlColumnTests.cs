using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlColumnTests
	{
		[Fact]
		public void Ctor_WithNullColumnName_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlColumn(null));
		}

		[Fact]
		public void Ctor_WithEmptyColumnName_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlColumn(string.Empty));
		}

		[Fact]
		public void Ctor_WithColumnName_SetsColumnNameProperty()
		{
			var column = new SqlColumn("[dbo].[Users].[Id]");

			Assert.NotNull(column.ColumnName);
			Assert.Equal("dbo", column.ColumnName.Segments[0]);
			Assert.Equal("Users", column.ColumnName.Segments[1]);
			Assert.Equal("Id", column.ColumnName.Segments[2]);
		}

		[Fact]
		public void Ctor_WithColumnNameAnAlias_SetsAliasProperty()
		{
			var column = new SqlColumn("[dbo].[Users].[Id]", "u");

			Assert.NotNull(column.ColumnName);
			Assert.Equal("dbo", column.ColumnName.Segments[0]);
			Assert.Equal("Users", column.ColumnName.Segments[1]);
			Assert.Equal("Id", column.ColumnName.Segments[2]);

			Assert.NotNull(column.Alias);
			Assert.Equal("u", column.Alias);
		}

		[Fact]
		public void ExpressionType_ReturnsColumn()
		{
			var table = new SqlColumn("[dbo].[Users].[Id]");
			Assert.Equal(SqlExpressionType.Column, table.ExpressionType);
		}


		[Fact]
		public void ImplicitConversion_WithSortOrder_ReturnsSort()
		{
			var column = new SqlColumn("[dbo].[Users].[Id]");
			var sort = column + SqlSortOrder.Descending;

			Assert.NotNull(sort);
			Assert.Same(column, sort.Column);
			Assert.Equal(SqlSortOrder.Descending, sort.SortOrder);
		}
	}
}
