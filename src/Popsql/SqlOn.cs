namespace Popsql
{
	/// <summary>
	/// Represents a SQL ON clause.
	/// </summary>
	public class SqlOn : SqlWhere
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlOn"/> class using the 
		/// specified <paramref name="predicate"/>.
		/// </summary>
		/// <param name="predicate">
		/// The predicate the SQL ON clause uses.
		/// </param>
		public SqlOn(SqlExpression predicate) 
			: base(predicate)
		{
		}

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.On;
	}
}