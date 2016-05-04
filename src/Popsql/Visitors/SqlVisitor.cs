using System;
using System.Collections.Generic;
using System.Reflection;

namespace Popsql.Visitors
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class SqlVisitor : ISqlVisitor
	{
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

				case SqlExpressionType.Column:
					Visit((SqlColumn)expression);
					break;

				case SqlExpressionType.Constant:
					Visit((SqlConstant)expression);
					break;

				case SqlExpressionType.Delete:
					Visit((SqlDelete)expression);
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
					// TODO: Union not supported yet.
					break;

				case SqlExpressionType.Update:
					Visit((SqlUpdate)expression);
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

		public virtual void Visit(SqlSet expression)
		{
		}

		public virtual void Visit(SqlAssign expression)
		{
		}

		public virtual void Visit(SqlBinaryExpression expression)
		{
		}

		public virtual void Visit(SqlColumn expression)
		{
		}

		public virtual void Visit(SqlConstant expression)
		{
		}

		public virtual void Visit(SqlDelete expression)
		{
		}

		public virtual void Visit(SqlFrom expression)
		{
		}

		public virtual void Visit(SqlFunction expression)
		{
		}

		public virtual void Visit(SqlIdentifier expression)
		{
		}

		public virtual void Visit(SqlInsert expression)
		{
		}

		public virtual void Visit(SqlJoin expression)
		{
		}

		public virtual void Visit(SqlOn expression)
		{
		}

		public virtual void Visit(SqlOrderBy expression)
		{
		}

		public virtual void Visit(SqlParameter expression)
		{
		}

		public virtual void Visit(SqlSelect expression)
		{
		}

		public virtual void Visit(SqlSort expression)
		{
		}

		public virtual void Visit(SqlSubquery expression)
		{
		}

		public virtual void Visit(SqlTable expression)
		{
		}

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

		public virtual void Visit(SqlUpdate expression)
		{
		}

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

				default:
					throw new InvalidOperationException("Unknown type of expression found.");
			}
		}

		public virtual void Visit(SqlValues expression)
		{
		}

		public virtual void Visit(SqlWhere expression)
		{
		}

		public virtual void Visit(IEnumerable<SqlColumn> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		public virtual void Visit(IEnumerable<SqlValue> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		public virtual void Visit(IEnumerable<SqlSort> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		public virtual void Visit(IEnumerable<SqlAssign> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		public virtual void Visit(IEnumerable<IEnumerable<SqlValue>> expressions)
		{
			expressions?.ForEach(_ => _.Accept(this));
		}

		public virtual void Visit(SqlBinaryOperator @operator)
		{
		}

		public virtual void Visit(SqlSortOrder sortOrder)
		{
		}

		public virtual void Visit(SqlInto expression)
		{
		}
	}
}