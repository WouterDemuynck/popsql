﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlSelectTests
	{
		[Fact]
		public void Ctor_WithNullColumns_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlSelect(null));
		}

		[Fact]
		public void Ctor_WithColumns_SetsColumnsProperty()
		{
			var columns = new List<SqlColumn>();
			columns.Add("Id");
			columns.Add("Name");
			columns.Add("Email");
			var query = new SqlSelect(columns);

			Assert.NotNull(query.Columns);
			Assert.Equal(3, query.Columns.Count());
			Assert.Equal("Id", query.Columns.First().ColumnName.Segments[0]);
			Assert.Equal("Name", query.Columns.Skip(1).First().ColumnName.Segments[0]);
			Assert.Equal("Email", query.Columns.Last().ColumnName.Segments[0]);
		}

		[Fact]
		public void From_WithNullSqlTable_ThrowsArgumentNull()
		{
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
			Assert.Throws<ArgumentNullException>(() => select.From(null));
		}


		[Fact]
		public void From_WithSqlTable_SetsTableProperty()
		{
			SqlSelect select = new SqlSelect(new SqlColumn[] { "Id", "Name" });
			select.From("Users");

			Assert.NotNull(select.Table);
			Assert.Equal("Users", select.Table.TableName.Segments.First());
		}

		[Fact]
		public void ExpressionType_ReturnsSelect()
		{
			var query = new SqlSelect(new SqlColumn[] { "Id" });

			Assert.Equal(SqlExpressionType.Select, query.ExpressionType);
		}
	}
}
