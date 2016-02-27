﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlUpdateTests
	{
		[Fact]
		public void Ctor_WithNullTable_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlUpdate(null));
		}

		[Fact]
		public void Ctor_WithTable_SetsTableProperty()
		{
			var update = Sql
				.Update("Users")
				.Set("Name", "John Doe")
				.Go();

			Assert.NotNull(update.Table);
			Assert.Equal("Users", update.Table.TableName.Segments.First());
		}

		[Fact]
		public void Set_WithNullColumn_ThrowsArgumentNull()
		{
			var update = Sql
				.Update("Users");
			Assert.Throws<ArgumentNullException>(() => update.Set(null, null));
		}

		[Fact]
		public void Set_WithNullValue_SetsValueToNull()
		{
			var update = Sql
				.Update("Users")
				.Set("Name", null)
				.Go();

			Assert.NotNull(update.Values);
			Assert.Equal(1, update.Values.Count());
			Assert.Same(SqlConstant.Null, update.Values.Single().Value);
			
		}

		[Fact]
		public void Set_WithColumnAndValue_SetsValuesProperty()
		{
			var update = Sql
				.Update("Users")
				.Set("Name", "Joan Doe")
				.Go();

			Assert.Equal(1, update.Values.Count());
			Assert.Equal("Name", update.Values.First().Column.ColumnName.Segments.Single());
			Assert.Equal(new SqlConstant("Joan Doe"), update.Values.First().Value);
		}

		[Fact]
		public void Set_SetsValuesPropertyRepeatedly()
		{
			var insert = Sql
				.Insert()
				.Into("User")
				.Values(1, "John Doe")
				.Values(2, "Jane Doe")
				.Values(3, "Junior Doe")
				.Go();

			Assert.NotNull(insert.Values);
			Assert.Equal(3, insert.Values.Count());
			Assert.Equal(2, insert.Values.First().Count());
			Assert.Equal(2, insert.Values.Skip(1).First().Count());
			Assert.Equal(2, insert.Values.Last().Count());

			Assert.Equal(1, insert.Values.First().First());
			Assert.Equal("John Doe", insert.Values.First().Last());

			Assert.Equal(2, insert.Values.Skip(1).First().First());
			Assert.Equal("Jane Doe", insert.Values.Skip(1).First().Last());

			Assert.Equal(3, insert.Values.Last().First());
			Assert.Equal("Junior Doe", insert.Values.Last().Last());
		}

		[Fact]
		public void Where_WithExpression_SetsWhereProperty()
		{
			var update = Sql
				.Update("Users")
				.Set("Name", "John Foobar")
				.Where(SqlExpression.Equal("Id", 5))
				.Go();

			Assert.NotNull(update.Where);
			Assert.IsType<SqlBinaryExpression>(update.Where);
			Assert.IsType<SqlColumn>(((SqlBinaryExpression)update.Where).Left);
			Assert.Equal("Id", ((SqlColumn)((SqlBinaryExpression)update.Where).Left).ColumnName.Segments.First());

			Assert.Equal(SqlBinaryOperator.Equal, ((SqlBinaryExpression)update.Where).Operator);

			Assert.IsType<SqlConstant>(((SqlBinaryExpression)update.Where).Right);
			Assert.Equal(5, ((SqlConstant)((SqlBinaryExpression)update.Where).Right).Value);
		}

		[Fact]
		public void Values_IsNotNullWhenFirstCalled()
		{
			var update = new SqlUpdate("Users");
			Assert.NotNull(update.Values);
		}

		[Fact]
		public void ExpressionType_ReturnsUpdate()
		{
			var query = new SqlUpdate("User");

			Assert.Equal(SqlExpressionType.Update, query.ExpressionType);
		}
	}
}