namespace Popsql
{
	public abstract class SqlFrom<TParent> : SqlClause<TParent> 
		where TParent : SqlStatement
	{
		protected SqlFrom(TParent parent) 
			: base(parent)
		{
		}

		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.From;
	}
}