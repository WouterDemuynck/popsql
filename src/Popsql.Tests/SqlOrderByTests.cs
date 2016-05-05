using System;
using System.Collections;
using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlOrderByTests
	{
		[Fact]
		public void Add_WithNullSortExpression_ThrowsArgumentNull()
		{
			var orderBy = new SqlOrderBy();
			Assert.Throws<ArgumentNullException>(() => orderBy.Add(null));
		}

		[Fact]
		public void ExpressionType_ReturnsOrderBy()
		{
			var orderBy = new SqlOrderBy();
			Assert.Equal(SqlExpressionType.OrderBy, orderBy.ExpressionType);
		}

		[Fact]
		public void GetEnumerator_ReturnsEnumerator()
		{
			var values = new SqlOrderBy
			{
				new SqlSort("Id", SqlSortOrder.Ascending),
				new SqlSort("CreatedOn", SqlSortOrder.Descending)
			};

			var enumerator = ((IEnumerable)values).GetEnumerator();
			Assert.NotNull(enumerator);

			int count = 0;
			while (enumerator.MoveNext())
			{
				count++;
			}
			Assert.Equal(2, count);
		}
	}
}