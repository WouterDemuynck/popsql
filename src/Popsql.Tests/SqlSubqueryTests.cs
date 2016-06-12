using System;
using Popsql.Grammar;
using Xunit;

namespace Popsql.Tests
{
	public class SqlSubqueryTests
	{
		[Fact]
		public void Ctor_WithNullQuery_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlSubquery((SqlSelect) null, null));
			Assert.Throws<ArgumentNullException>(() => new SqlSubquery((ISqlGo<SqlSelect>) null, null));
			Assert.Throws<ArgumentNullException>(() => new SqlSubquery(null));
		}

		[Fact]
		public void Ctor_WithQueryAndAlias_SetsQueryProperty()
		{
			var query = Sql.Select("Id", "Name").From("Users").Go();
			var subquery = new SqlSubquery(query, "u");

			Assert.NotNull(subquery.Query);
			Assert.Same(query, subquery.Query);
		}
	}
}