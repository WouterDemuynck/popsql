namespace Popsql
{
	public class SqlWhere<TParent> : SqlClause<TParent> 
		where TParent : SqlStatement
	{
		public SqlWhere(TParent parent) : base(parent)
		{
		}

		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Where;
	}
}