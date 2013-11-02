using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Popsql
{
    /// <summary>
    /// Represents a SQL UNION statement.
    /// </summary>
    public class SqlUnion : SqlStatement
    {
        private readonly IEnumerable<SqlSelect> _statements;

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
            if (statements == null || !statements.Any())
            {
                throw new ArgumentException("At least one select statement should be provided.", "statements");
            }

            _statements = statements;
        }

        /// <summary>
        /// Gets the collection of <see cref="SqlSelect"/> statements combined by this <see cref="SqlUnion"/>.
        /// </summary>
        public IEnumerable<SqlSelect> Statements
        {
            get
            {
                return _statements;
            }
        }

        /// <summary>
        /// Returns the expression type of this expression.
        /// </summary>
        public override SqlExpressionType ExpressionType
        {
            get
            {
                return SqlExpressionType.Union;
            }
        }
    }
}
