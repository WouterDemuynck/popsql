using System.Collections.Generic;

namespace Popsql.Visitors
{
	public interface ISqlVisitor
	{
		void Visit(SqlExpression expression);
		void Visit(SqlAssign expression);
		void Visit(SqlBinaryExpression expression);
		void Visit(SqlColumn expression);
		void Visit(SqlConstant expression);
		void Visit(SqlDelete expression);
		void Visit(SqlFrom expression);
		void Visit(SqlFunction expression);
		void Visit(SqlIdentifier expression);
		void Visit(SqlInsert expression);
		void Visit(SqlJoin expression);
		void Visit(SqlOn expression);
		void Visit(SqlOrderBy expression);
		void Visit(SqlParameter expression);
		void Visit(SqlSelect expression);
		void Visit(SqlSort expression);
		void Visit(SqlSubquery expression);
		void Visit(SqlTable expression);
		void Visit(SqlTableExpression expression);
		void Visit(SqlUpdate expression);
		void Visit(SqlValue expression);
		void Visit(SqlValues expression);
		void Visit(SqlWhere expression);
		void Visit(SqlInto expression);

		void Visit(IEnumerable<SqlColumn> expressions);
		void Visit(IEnumerable<SqlValue> expressions);
		void Visit(IEnumerable<SqlSort> expressions);
		void Visit(IEnumerable<SqlAssign> expressions);
		void Visit(IEnumerable<IEnumerable<SqlValue>> expressions);

		void Visit(SqlBinaryOperator @operator);
		void Visit(SqlSortOrder sortOrder);
	}
}