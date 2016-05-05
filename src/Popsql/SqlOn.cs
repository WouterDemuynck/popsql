namespace Popsql
{
	public class SqlOn : SqlWhere
	{
		public SqlOn(SqlExpression predicate) 
			: base(predicate)
		{
		}

		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.On;
	}
}