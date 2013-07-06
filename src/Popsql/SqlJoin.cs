using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL JOIN expression.
    /// </summary>
    public class SqlJoin : SqlExpression
    {
        private SqlJoinType _type;
        private SqlTable _table;
        private SqlExpression _predicate;

        internal SqlJoin(SqlJoinType type, SqlTable table, SqlExpression predicate)
        {
            if (table == null) throw new ArgumentNullException("table");
            _type = type;
            _table = table;
            _predicate = predicate;
        }

        /// <summary>
        /// Gets the type of join used for joining rows.
        /// </summary>
        public SqlJoinType Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets the table used for joining rows.
        /// </summary>
        public SqlTable Table
        {
            get
            {
                return _table;
            }
        }

        /// <summary>
        /// Gets the predicate used for determining which rows are joined by this SQL JOIN expression.
        /// </summary>
        public SqlExpression Predicate
        {
            get
            {
                return _predicate;
            }
        }

        /// <summary>
        /// Returns the expression type of this expression.
        /// </summary>
        public override SqlExpressionType ExpressionType
        {
            get { return SqlExpressionType.Join; }
        }
    }
}
