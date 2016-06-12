using System;
using System.Collections.Generic;
using System.Linq;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL UNION statement.
	/// </summary>
	public class SqlUnion : SqlStatement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlUnion"/> class using the specified
		/// SQL SELECT statements.
		/// </summary>
		/// <param name="statements">
		/// A collection of <see cref="SqlSelect"/> instances to combine.
		/// </param>
		public SqlUnion(params SqlSelect[] statements)
			: this((IEnumerable<SqlSelect>)statements)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlUnion"/> class using the specified
		/// SQL SELECT statements.
		/// </summary>
		/// <param name="statements">
		/// A collection of <see cref="SqlSelect"/> instances to unify.
		/// </param>
		public SqlUnion(IEnumerable<SqlSelect> statements)
		{
			if (statements == null) throw new ArgumentNullException(nameof(statements));
			if (!statements.Any()) throw new ArgumentException("At least one SELECT statement should be provided.", nameof(statements));

			Statements = statements;
		}

		/// <summary>
		/// Gets the collection of <see cref="SqlSelect"/> statements combined by this <see cref="SqlUnion"/>.
		/// </summary>
		public IEnumerable<SqlSelect> Statements
		{
			get;
		}

		/// <summary>
		/// Returns the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.Union;
	}
}
