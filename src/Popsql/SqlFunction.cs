using System;
using System.Collections.Generic;
using System.Reflection;

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
		/// <param name="alias">
		/// The alias to use for the function.
		/// </param>
		/// <param name="arguments">
		/// The arguments passed to the SQL function.
		/// </param>
		public SqlFunction(string functionName, IEnumerable<SqlValue> arguments = null, string alias = null)
		{
			if (functionName == null) throw new ArgumentNullException(nameof(functionName));

			FunctionName = functionName;
			Arguments = arguments;
			Alias = alias;
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
		/// Gets the alias used for the SQL function.
		/// </summary>
		public string Alias { get; private set; }

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Function;
	}
}