namespace Popsql
{
	/// <summary>
	/// Represents a SQL HAVING clause.
	/// </summary>
	public class SqlHaving : SqlWhere
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlHaving"/> class using the 
		/// specified <paramref name="predicate"/>.
		/// </summary>
		/// <param name="predicate">
		/// The predicate the SQL HAVING clause uses.
		/// </param>
		public SqlHaving(SqlExpression predicate) 
			: base(predicate)
		{
		}

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.Having;
	}
}