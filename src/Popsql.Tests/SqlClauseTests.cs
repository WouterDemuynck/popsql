using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlClauseTests
	{
		[Fact]
		public void Ctor_WithNullParent_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new TestSqlClause(null));
		}

		private class TestSqlClause : SqlClause<SqlSelect>
		{
			public TestSqlClause(SqlSelect parent)
				: base(parent)
			{
			}

			public override SqlExpressionType ExpressionType 
				=> SqlExpressionType.From;
		}
	}
}