namespace Popsql
{
	public class SqlOrderBy<TParent> : SqlClause<TParent> 
		where TParent : SqlStatement
	{
		public SqlOrderBy(TParent parent) : base(parent)
		{
		}

		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.OrderBy;
	}
}