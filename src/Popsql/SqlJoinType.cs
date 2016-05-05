namespace Popsql
{
    /// <summary>
    /// Describes the join types for a SQL JOIN clause.
    /// </summary>
    public enum SqlJoinType
    {
        /// <summary>
        /// Represents a SQL JOIN expression.
        /// </summary>
        Default,
        /// <summary>
        /// Represents a SQL INNER JOIN expression.
        /// </summary>
        Inner,
        /// <summary>
        /// Represents a SQL LEFT JOIN expression.
        /// </summary>
        Left,
        /// <summary>
        /// Represents a SQL RIGHT JOIN expression.
        /// </summary>
        Right
    }
}
