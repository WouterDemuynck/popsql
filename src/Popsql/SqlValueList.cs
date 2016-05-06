using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Popsql
{
	/// <summary>
	/// Represents a list of <see cref="SqlValue"/> expressions.
	/// </summary>
	public class SqlValueList : SqlValue
	{
		private readonly List<SqlValue> _values;
		 
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlValueList"/> class.
		/// </summary>
		/// <param name="values">
		/// The values in this <see cref="SqlValueList"/>.
		/// </param>
		internal SqlValueList(IEnumerable<SqlValue> values)
		{
			_values = new List<SqlValue>(values ?? Enumerable.Empty<SqlValue>());
		}

		/// <summary>
		/// Gets the collection of values in this <see cref="SqlValueList"/>.
		/// </summary>
		public IEnumerable<SqlValue> Values
			=> _values;

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.ValueList;

	}
}