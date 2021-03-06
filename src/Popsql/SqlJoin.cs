﻿using System;
using Popsql.Visitors;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL JOIN expression.
    /// </summary>
    public class SqlJoin : SqlExpression
    {
	    internal SqlJoin(SqlJoinType type, SqlTable table, SqlExpression predicate = null)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            Type = type;
            Table = table;
            On = predicate == null ? null : new SqlOn(predicate);
        }

	    /// <summary>
	    /// Gets the type of join used for joining rows.
	    /// </summary>
	    public SqlJoinType Type
	    {
		    get;
	    }

	    /// <summary>
	    /// Gets the table used for joining rows.
	    /// </summary>
	    public SqlTable Table
	    {
		    get;
	    }

	    /// <summary>
	    /// Gets the predicate used for determining which rows are joined by this SQL JOIN expression.
	    /// </summary>
	    public SqlOn On
	    {
		    get;
			internal set;
		}

	    /// <summary>
        /// Returns the expression type of this expression.
        /// </summary>
        public override SqlExpressionType ExpressionType 
			=> SqlExpressionType.Join;

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

			Table.Accept(visitor);
			On.Accept(visitor);
	    }
    }
}
