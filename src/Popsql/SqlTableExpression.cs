namespace Popsql
{
	public abstract class SqlTableExpression : SqlExpression
	{
		protected SqlTableExpression(string alias)
		{
			Alias = alias;
		}

		/// <summary>
		/// Gets the alias used for the table.
		/// </summary>
		public string Alias
		{
			get;
			private set;
		}

		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Table;
	}
}