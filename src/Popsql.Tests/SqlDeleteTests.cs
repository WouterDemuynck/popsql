using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlDeleteTests
	{
		[Fact]
		public void Ctor_DoesNotThrow()
		{
			new SqlDelete();
		}

		[Fact]
		public void From_WithNullTable_ThrowsArgumentNull()
		{
			var delete = Sql.Delete();
			Assert.Throws<ArgumentNullException>(() => delete.From(null));
		}


		[Fact]
		public void From_WithTable_SetsFromProperty()
		{
			var delete = Sql
				.Delete()
				.From("Users")
				.Go();

			Assert.NotNull(delete.From);
			Assert.Equal("Users", ((SqlTable)delete.From.Table).TableName.Segments.First());
		}

		[Fact]
		public void Where_WithExpression_SetsWhereProperty()
		{
			var delete = Sql
				.Delete()
				.From("User")
				.Where(SqlExpression.Equal("Id", 5))
				.Go();

			Assert.NotNull(delete.Where);
			Assert.NotNull(delete.Where.Predicate);
			Assert.IsType<SqlBinaryExpression>(delete.Where.Predicate);
			Assert.IsType<SqlColumn>(((SqlBinaryExpression)delete.Where.Predicate).Left);
			Assert.Equal("Id", ((SqlColumn)((SqlBinaryExpression)delete.Where.Predicate).Left).ColumnName.Segments.First());

			Assert.Equal(SqlBinaryOperator.Equal, ((SqlBinaryExpression)delete.Where.Predicate).Operator);

			Assert.IsType<SqlConstant>(((SqlBinaryExpression)delete.Where.Predicate).Right);
			Assert.Equal(5, ((SqlConstant)((SqlBinaryExpression)delete.Where.Predicate).Right).Value);
		}
		
		[Fact]
		public void ExpressionType_ReturnsDelete()
		{
			var query = new SqlDelete();

			Assert.Equal(SqlExpressionType.Delete, query.ExpressionType);
		}
	}
}