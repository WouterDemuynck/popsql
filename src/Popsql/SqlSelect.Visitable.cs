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
		public override void Accept(ISqlVisitor visitor)
		{
			base.Accept(visitor);

			Select.Accept(visitor);
			From.Accept(visitor);
			Joins.ForEach(join => join.Accept(visitor));
			Where?.Accept(visitor);
			OrderBy.IfAny(orderBy => OrderBy.Accept(visitor));
		}
	}
}