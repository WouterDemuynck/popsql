using System.Collections.Generic;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL function call.
	/// </summary>
	public class SqlFunction : SqlValue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlFunction"/> class using the specified
		/// function name and arguments.
		/// </summary>
		/// <param name="functionName">
		/// The name of the SQL function to call.
		/// </param>
		/// <param name="arguments">
		/// The arguments passed to the SQL function.
		/// </param>
		public SqlFunction(string functionName, IEnumerable<SqlValue> arguments = null)
		{
			FunctionName = functionName;
			Arguments = arguments;
		}

		/// <summary>
		/// Gets the name of the SQL function.
		/// </summary>
		public string FunctionName { get; private set; }

		/// <summary>
		/// Gets the collection of arguments of the SQL function.
		/// </summary>
		public IEnumerable<SqlValue> Arguments { get; private set; }

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
		{
			get
			{
				return SqlExpressionType.Function;
			}
		}
	}
}