using System;
using System.Collections.Generic;

namespace Popsql.Visitors
{
	/// <summary>
	/// Represents a visitor for SQL expression trees.
	/// </summary>
	public abstract class SqlVisitor : ISqlVisitor
	{
		/// <summary>
		/// Visits the specified <see cref="SqlExpression"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlExpression expression)
		{
			switch (expression.ExpressionType)
			{
				case SqlExpressionType.Assign:
					Visit((SqlAssign)expression);
					break;

				case SqlExpressionType.Binary:
					Visit((SqlBinaryExpression)expression);
					break;

				case SqlExpressionType.Cast:
					Visit((SqlCast)expression);
					break;

				case SqlExpressionType.Column:
					Visit((SqlColumn)expression);
					break;

				case SqlExpressionType.Constant:
					Visit((SqlConstant)expression);
					break;

				case SqlExpressionType.DataType:
					Visit((SqlDataType)expression);
					break;

				case SqlExpressionType.Delete:
					Visit((SqlDelete)expression);
					break;

				case SqlExpressionType.Limit:
					Visit((SqlLimit)expression);
					break;

				case SqlExpressionType.From:
					Visit((SqlFrom)expression);
					break;

				case SqlExpressionType.Function:
					Visit((SqlFunction)expression);
					break;

				case SqlExpressionType.Identifier:
					Visit((SqlIdentifier)expression);
					break;

				case SqlExpressionType.Insert:
					Visit((SqlInsert)expression);
					break;

				case SqlExpressionType.Join:
					Visit((SqlJoin)expression);
					break;

				case SqlExpressionType.On:
					Visit((SqlOn)expression);
					break;

				case SqlExpressionType.OrderBy:
					Visit((SqlOrderBy)expression);
					break;

				case SqlExpressionType.GroupBy:
					Visit((SqlGroupBy)expression);
					break;

				case SqlExpressionType.Having:
					Visit((SqlHaving)expression);
					break;

				case SqlExpressionType.Parameter:
					Visit((SqlParameter)expression);
					break;

				case SqlExpressionType.Select:
					Visit((SqlSelect)expression);
					break;

				case SqlExpressionType.Sort:
					Visit((SqlSort)expression);
					break;

				case SqlExpressionType.Subquery:
					Visit((SqlSubquery)expression);
					break;

				case SqlExpressionType.Table:
					Visit((SqlTable)expression);
					break;

				case SqlExpressionType.Union:
					Visit((SqlUnion)expression);
					break;

				case SqlExpressionType.Update:
					Visit((SqlUpdate)expression);
					break;

				case SqlExpressionType.ValueList:
					Visit((SqlValueList)expression);
					break;

				case SqlExpressionType.Values:
					Visit((SqlValues)expression);
					break;

				case SqlExpressionType.Where:
					Visit((SqlWhere)expression);
					break;

				case SqlExpressionType.Into:
					Visit((SqlInto)expression);
					break;

				case SqlExpressionType.Set:
					Visit((SqlSet)expression);
					break;

				default:
					throw new InvalidOperationException("Unknown type of expression found.");
			}
		}

		/// <summary>
		/// Visits the specified <see cref="SqlUnion"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlUnion expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlSet"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlSet expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlAssign"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlAssign expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlBinaryExpression"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlBinaryExpression expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlCast"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlCast expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlColumn"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlColumn expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlConstant"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlConstant expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlDataType"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlDataType expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlDelete"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlDelete expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlLimit"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlLimit expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlFrom"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlFrom expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlFunction"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlFunction expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlIdentifier"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlIdentifier expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlInsert"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlInsert expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlJoin"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlJoin expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlOn"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlOn expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlOrderBy"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlOrderBy expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlGroupBy"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlGroupBy expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlHaving"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlHaving expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlParameter"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlParameter expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlSelect"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlSelect expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlSort"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlSort expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlSubquery"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlSubquery expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlTable"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlTable expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlTableExpression"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public void Visit(SqlTableExpression expression)
		{
			switch (expression.ExpressionType)
			{
				case SqlExpressionType.Subquery:
					Visit((SqlSubquery)expression);
					break;

				case SqlExpressionType.Table:
					Visit((SqlTable)expression);
					break;

				default:
					throw new InvalidOperationException("Unknown type of expression found.");
			}
		}

		/// <summary>
		/// Visits the specified <see cref="SqlUpdate"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlUpdate expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlValue"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public void Visit(SqlValue expression)
		{
			switch (expression.ExpressionType)
			{
				case SqlExpressionType.Column:
					Visit((SqlColumn)expression);
					break;

				case SqlExpressionType.Constant:
					Visit((SqlConstant)expression);
					break;

				case SqlExpressionType.Function:
					Visit((SqlFunction)expression);
					break;

				case SqlExpressionType.Parameter:
					Visit((SqlParameter)expression);
					break;

				case SqlExpressionType.ValueList:
					Visit((SqlValueList)expression);
					break;

				default:
					throw new InvalidOperationException("Unknown type of expression found.");
			}
		}

		/// <summary>
		/// Visits the specified <see cref="SqlValueList"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlValueList expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlValues"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlValues expression)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlWhere"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlWhere expression)
		{
		}

		/// <summary>
		/// Visits the specified collection of <see cref="SqlColumn"/> expressions.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		public virtual void Visit(IEnumerable<SqlColumn> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		/// <summary>
		/// Visits the specified collection of <see cref="SqlValue"/> expressions.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		public virtual void Visit(IEnumerable<SqlValue> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		/// <summary>
		/// Visits the specified collection of <see cref="SqlSort"/> expressions.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		public virtual void Visit(IEnumerable<SqlSort> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		/// <summary>
		/// Visits the specified collection of <see cref="SqlAssign"/> expressions.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		public virtual void Visit(IEnumerable<SqlAssign> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		/// <summary>
		/// Visits the specified collection of <see cref="SqlValue"/> expression sets.
		/// </summary>
		/// <param name="expressions">
		/// The expressions to visit.
		/// </param>
		public virtual void Visit(IEnumerable<IEnumerable<SqlValue>> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		/// <summary>
		/// Visits the specified <see cref="SqlBinaryOperator"/>.
		/// </summary>
		/// <param name="operator">
		/// The <see cref="SqlBinaryOperator"/> to visit.
		/// </param>
		public virtual void Visit(SqlBinaryOperator @operator)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlSortOrder"/>.
		/// </summary>
		/// <param name="sortOrder">
		/// The <see cref="SqlSortOrder"/> to visit.
		/// </param>
		public virtual void Visit(SqlSortOrder sortOrder)
		{
		}

		/// <summary>
		/// Visits the specified <see cref="SqlInto"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression to visit.
		/// </param>
		public virtual void Visit(SqlInto expression)
		{
		}
	}
}