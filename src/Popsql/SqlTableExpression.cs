using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL table expression.
	/// </summary>
	public abstract class SqlTableExpression : SqlExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlTableExpression"/> class using
		/// the specified <paramref name="alias"/>.
		/// </summary>
		/// <param name="alias">
		/// The alias to use for the table expression.
		/// </param>
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

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Table;
	}
}