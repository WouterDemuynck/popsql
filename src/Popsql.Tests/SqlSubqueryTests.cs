using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlSubqueryTests
	{
		[Fact]
		public void Ctor_WithNullQuery_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlSubquery(null, null));
		}

		[Fact]
		public void Ctor_WithNullAlias_ThrowsArgumentNull()
		{
			var query = Sql.Select("Id", "Name").From("Users").Go();
			Assert.Throws<ArgumentNullException>(() => new SqlSubquery(query, null));
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