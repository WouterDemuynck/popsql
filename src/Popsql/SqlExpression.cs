using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
	/// <summary>
	/// Provides the base class for SQL expressions.
	/// </summary>
	public abstract class SqlExpression
	{
		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public abstract SqlExpressionType ExpressionType
		{
			get;
		}
	}
}
