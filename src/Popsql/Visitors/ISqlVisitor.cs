using System.Collections.Generic;

namespace Popsql.Visitors
{
	/// <summary>
	/// Provides the interface for classes that can visit SQL expression trees.
	/// </summary>
	public interface ISqlVisitor
	{
		/// <summary>
		/// Visits the specified <see cref="SqlExpression"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlExpression expression);

		/// <summary>
		/// Visits the specified <see cref="SqlAssign"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlAssign expression);

		/// <summary>
		/// Visits the specified <see cref="SqlBinaryExpression"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlBinaryExpression expression);

		/// <summary>
		/// Visits the specified <see cref="SqlCast"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlCast expression);

		/// <summary>
		/// Visits the specified <see cref="SqlColumn"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlColumn expression);

		/// <summary>
		/// Visits the specified <see cref="SqlConstant"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlConstant expression);

		/// <summary>
		/// Visits the specified <see cref="SqlDataType"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlDataType expression);

		/// <summary>
		/// Visits the specified <see cref="SqlDelete"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlDelete expression);

		/// <summary>
		/// Visits the specified <see cref="SqlLimit"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlLimit expression);

		/// <summary>
		/// Visits the specified <see cref="SqlFrom"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlFrom expression);

		/// <summary>
		/// Visits the specified <see cref="SqlFunction"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlFunction expression);

		/// <summary>
		/// Visits the specified <see cref="SqlIdentifier"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlIdentifier expression);

		/// <summary>
		/// Visits the specified <see cref="SqlInsert"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlInsert expression);

		/// <summary>
		/// Visits the specified <see cref="SqlJoin"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlJoin expression);

		/// <summary>
		/// Visits the specified <see cref="SqlOn"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlOn expression);

		/// <summary>
		/// Visits the specified <see cref="SqlOrderBy"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlOrderBy expression);

		/// <summary>
		/// Visits the specified <see cref="SqlParameter"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlParameter expression);

		/// <summary>
		/// Visits the specified <see cref="SqlSelect"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlSelect expression);

		/// <summary>
		/// Visits the specified <see cref="SqlSort"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlSort expression);

		/// <summary>
		/// Visits the specified <see cref="SqlSubquery"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlSubquery expression);

		/// <summary>
		/// Visits the specified <see cref="SqlTable"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlTable expression);

		/// <summary>
		/// Visits the specified <see cref="SqlTableExpression"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlTableExpression expression);

		/// <summary>
		/// Visits the specified <see cref="SqlUpdate"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlUpdate expression);

		/// <summary>
		/// Visits the specified <see cref="SqlValue"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlValue expression);

		/// <summary>
		/// Visits the specified <see cref="SqlValues"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlValues expression);

		/// <summary>
		/// Visits the specified <see cref="SqlWhere"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlWhere expression);

		/// <summary>
		/// Visits the specified <see cref="SqlInto"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlInto expression);

		/// <summary>
		/// Visits the specified collection of <see cref="SqlColumn"/> expressions.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		void Visit(IEnumerable<SqlColumn> expressions);

		/// <summary>
		/// Visits the specified collection of <see cref="SqlValue"/> expressions.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		void Visit(IEnumerable<SqlValue> expressions);

		/// <summary>
		/// Visits the specified collection of <see cref="SqlSort"/> expressions.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		void Visit(IEnumerable<SqlSort> expressions);

		/// <summary>
		/// Visits the specified collection of <see cref="SqlAssign"/> expressions.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		void Visit(IEnumerable<SqlAssign> expressions);

		/// <summary>
		/// Visits the specified <see cref="SqlSet"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlSet expression);

		/// <summary>
		/// Visits the specified collection of <see cref="SqlValue"/> expression sets.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		void Visit(IEnumerable<IEnumerable<SqlValue>> expressions);

		/// <summary>
		/// Visits the specified <see cref="SqlBinaryOperator"/>.
		/// </summary>
		/// <param name="operator">
		/// The <see cref="SqlBinaryOperator"/> to visit.
		/// </param>
		void Visit(SqlBinaryOperator @operator);

		/// <summary>
		/// Visits the specified <see cref="SqlSortOrder"/>.
		/// </summary>
		/// <param name="sortOrder">
		/// The <see cref="SqlSortOrder"/> to visit.
		/// </param>
		void Visit(SqlSortOrder sortOrder);

		/// <summary>
		/// Visits the specified <see cref="SqlValueList"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlValueList expression);

		/// <summary>
		/// Visits the specified <see cref="SqlUnion"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlUnion expression);

		/// <summary>
		/// Visits the specified <see cref="SqlGroupBy"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlGroupBy expression);

		/// <summary>
		/// Visits the specified <see cref="SqlHaving"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		void Visit(SqlHaving expression);
	}
}