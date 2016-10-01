using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Popsql.Dialects;
using Popsql.Grammar;
using Popsql.Text;
using Popsql.Visitors;

namespace Popsql
{
	/// <summary>
	/// Provides extension methods for writing SQL expression trees to SQL text.
	/// </summary>
	public static class ToSqlExtensions
	{
		/// <summary>
		/// Converts the specified SQL expression tree builder to SQL text.
		/// </summary>
		/// <typeparam name="T">
		/// The type of SQL statement the SQL expression tree builder creates.
		/// </typeparam>
		/// <param name="sql">
		/// An expression tree builder representing a SQL statement.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree builder.
		/// </returns>
		public static string ToSql<T>(this ISqlGo<T> sql)
			where T : SqlStatement
		{
			return sql.ToSql(null);
		}

		/// <summary>
		/// Converts the specified SQL expression tree builder to SQL text.
		/// </summary>
		/// <typeparam name="T">
		/// The type of SQL statement the SQL expression tree builder creates.
		/// </typeparam>
		/// <param name="sql">
		/// An expression tree builder representing a SQL statement.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> used to format the SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree builder.
		/// </returns>
		public static string ToSql<T>(this ISqlGo<T> sql, SqlDialect dialect)
			where T : SqlStatement
		{
			if (sql == null) throw new ArgumentNullException(nameof(sql));
			return sql.Go().ToSqlInternal(dialect);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlUnion"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlUnion"/> to convert to SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlUnion sql)
		{
			return sql.ToSql(null);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlUnion"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlUnion"/> to convert to SQL text.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> used to format the SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlUnion sql, SqlDialect dialect)
		{
			return sql.ToSqlInternal(dialect);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlSelect"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlSelect"/> to convert to SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlSelect sql)
		{
			return sql.ToSql(null);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlSelect"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlSelect"/> to convert to SQL text.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> used to format the SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlSelect sql, SqlDialect dialect)
		{
			return sql.ToSqlInternal(dialect);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlDelete"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlDelete"/> to convert to SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlDelete sql)
		{
			return sql.ToSql(null);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlDelete"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlDelete"/> to convert to SQL text.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> used to format the SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlDelete sql, SqlDialect dialect)
		{
			return sql.ToSqlInternal(dialect);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlUpdate"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlUpdate"/> to convert to SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlUpdate sql)
		{
			return sql.ToSql(null);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlUpdate"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlUpdate"/> to convert to SQL text.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> used to format the SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlUpdate sql, SqlDialect dialect)
		{
			return sql.ToSqlInternal(dialect);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlInsert"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlInsert"/> to convert to SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlInsert sql)
		{
			return sql.ToSql(null);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlInsert"/> expression tree to SQL text.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlInsert"/> to convert to SQL text.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> used to format the SQL text.
		/// </param>
		/// <returns>
		/// The SQL text for the specified SQL expression tree.
		/// </returns>
		public static string ToSql(this SqlInsert sql, SqlDialect dialect)
		{
			return sql.ToSqlInternal(dialect);
		}

		internal static string ToSqlInternal(this SqlStatement sql, SqlDialect dialect = null, Action<SqlParameter> onParameterVisited = null)
		{
			if (sql == null) throw new ArgumentNullException(nameof(sql));
			dialect = dialect ?? SqlDialect.Current;

			StringBuilder builder = new StringBuilder();
			using (SqlWriter writer = new SqlWriter(builder, dialect))
			{
				var visitor = new SqlWriterVisitor(writer)
				{
					ParameterVisited = onParameterVisited
				};

				sql.Accept(visitor);
			}

			return builder.ToString();
		}

		private class SqlWriterVisitor : SqlVisitor
		{
			private readonly SqlWriter _writer;

			public SqlWriterVisitor(SqlWriter writer)
			{
				_writer = writer;
			}

			public Action<SqlParameter> ParameterVisited
			{
				get;
				set;
			}

			public override void Visit(SqlGroupBy expression)
			{
				_writer.WriteKeyword(SqlKeywords.GroupBy);
			}

			public override void Visit(SqlHaving expression)
			{
				_writer.WriteKeyword(SqlKeywords.Having);
			}

			public override void Visit(SqlParameter expression)
			{
				_writer.WriteParameter(expression.ParameterName);
				ParameterVisited?.Invoke(expression);
			}

			public override void Visit(SqlSelect expression)
			{
				_writer.WriteKeyword(SqlKeywords.Select);
			}

			public override void Visit(SqlDelete expression)
			{
				_writer.WriteKeyword(SqlKeywords.Delete);
			}

			public override void Visit(SqlLimit expression)
			{
				_writer.WriteLimit(expression.Offset, expression.Count);
			}

			public override void Visit(SqlSubquery expression)
			{
				// TODO: This is weird and should actually be handled in SqlSubquery.Accept().
				_writer.WriteOpenParenthesis();
				expression.Query.Accept(this);
				_writer.WriteCloseParenthesis();
				if (expression.Alias != null)
				{
					_writer.WriteIdentifier(expression.Alias);
				}
			}

			public override void Visit(SqlTable expression)
			{
				_writer.WriteIdentifier(expression.TableName);
				if (expression.Alias != null)
				{
					_writer.WriteIdentifier(expression.Alias);
				}
			}

			public override void Visit(SqlUpdate expression)
			{
				_writer.WriteKeyword(SqlKeywords.Update);
			}

			public override void Visit(SqlColumn expression)
			{
				_writer.WriteIdentifier(expression.ColumnName);
				if (expression.Alias != null)
				{
					_writer.WriteKeyword(SqlKeywords.As);
					_writer.WriteIdentifier(expression.Alias);
				}
			}

			public override void Visit(SqlJoin expression)
			{
				switch (expression.Type)
				{
					case SqlJoinType.Inner:
						_writer.WriteKeyword(SqlKeywords.Inner);
						break;

					case SqlJoinType.Left:
						_writer.WriteKeyword(SqlKeywords.Left);
						break;

					case SqlJoinType.Right:
						_writer.WriteKeyword(SqlKeywords.Right);
						break;
				}
				_writer.WriteKeyword(SqlKeywords.Join);
			}

			public override void Visit(SqlOn expression)
			{
				_writer.WriteKeyword(SqlKeywords.On);
			}

			public override void Visit(SqlFrom expression)
			{
				_writer.WriteKeyword(SqlKeywords.From);
			}

			public override void Visit(SqlFunction expression)
			{
				_writer.Write(expression.FunctionName);
				_writer.WriteRaw("(");
				_writer.ClearPendingSpace();
				expression.Arguments.Accept(this);
				_writer.WriteCloseParenthesis();
				if (expression.Alias != null)
				{
					_writer.WriteKeyword(SqlKeywords.As);
					_writer.WriteIdentifier(expression.Alias);
				}
			}

			public override void Visit(SqlValueList expression)
			{
				_writer.WriteOpenParenthesis();
				expression.Values.Accept(this);
				_writer.WriteCloseParenthesis();
			}

			public override void Visit(SqlUnion expression)
			{
				expression.Statements.For(
					(index, statement) =>
					{
						if (index > 0) _writer.WriteKeyword(SqlKeywords.Union);
						_writer.WriteOpenParenthesis();
						statement.Accept(this);
						_writer.WriteCloseParenthesis();
					});
			}

			public override void Visit(SqlValues expression)
			{
				_writer.WriteKeyword(SqlKeywords.Values);
			}

			public override void Visit(SqlWhere expression)
			{
				_writer.WriteKeyword(SqlKeywords.Where);
			}

			public override void Visit(IEnumerable<SqlColumn> expressions)
			{
				_writer.WriteOpenParenthesis();
				expressions.For(
					(index, expression) =>
					{
						if (index > 0) _writer.WriteRaw(",");
						expression.Accept(this);
					});
				_writer.WriteCloseParenthesis();
			}

			public override void Visit(SqlInto expression)
			{
				_writer.WriteKeyword(SqlKeywords.Into);
			}

			public override void Visit(IEnumerable<SqlValue> expressions)
			{
				expressions.For(
					(index, expression) =>
					{
						if (index > 0) _writer.WriteRaw(",");
						expression.Accept(this);
					});
			}

			public override void Visit(IEnumerable<SqlSort> expressions)
			{
				expressions.For(
					(index, expression) =>
					{
						if (index > 0) _writer.WriteRaw(",");
						expression.Accept(this);
					});
			}

			public override void Visit(IEnumerable<SqlAssign> expressions)
			{
				expressions.For(
					(index, expression) =>
					{
						if (index > 0) _writer.WriteRaw(",");
						expression.Accept(this);
					});
			}

			public override void Visit(IEnumerable<IEnumerable<SqlValue>> expressions)
			{
				expressions.For(
					(index, expression) =>
					{
						if (index > 0) _writer.WriteRaw(",");
						_writer.WriteOpenParenthesis();
						expression.Accept(this);
						_writer.WriteCloseParenthesis();
					});
			}

			public override void Visit(SqlOrderBy expression)
			{
				_writer.WriteKeyword(SqlKeywords.OrderBy);
			}

			public override void Visit(SqlBinaryOperator @operator)
			{
				_writer.WriteOperator(@operator);
			}

			public override void Visit(SqlSortOrder sortOrder)
			{
				_writer.WriteSortOrder(sortOrder);
			}

			public override void Visit(SqlSet expression)
			{
				_writer.WriteKeyword(SqlKeywords.Set);
			}

			public override void Visit(SqlBinaryExpression expression)
			{
				bool useParentheses = expression.Operator == SqlBinaryOperator.And ||
									  expression.Operator == SqlBinaryOperator.Or;

				// TODO: The accept code should be in the class itself.
				if (useParentheses) _writer.WriteOpenParenthesis();
				expression.Left.Accept(this);
				if (useParentheses) _writer.WriteCloseParenthesis();
				expression.Operator.Accept(this);
				if (useParentheses) _writer.WriteOpenParenthesis();
				expression.Right.Accept(this);
				if (useParentheses) _writer.WriteCloseParenthesis();
			}

			public override void Visit(SqlConstant expression)
			{
				_writer.WriteValue(expression.Value);
			}

			public override void Visit(SqlInsert expression)
			{
				_writer.WriteKeyword(SqlKeywords.Insert);
			}
		}
	}
}