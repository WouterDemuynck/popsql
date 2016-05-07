using System;
using System.Collections.Generic;
using System.Linq;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL SELECT statement.
	/// </summary>
	public partial class SqlSelect
	{
		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);

			Select.Accept(visitor);
			From.Accept(visitor);
			Joins.ForEach(join => join.Accept(visitor));
			Where?.Accept(visitor);
			GroupBy?.Accept(visitor);
			OrderBy.IfAny(orderBy => OrderBy.Accept(visitor));
		}
	}
}