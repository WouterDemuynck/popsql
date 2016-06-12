using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Popsql.Dialects;
using Popsql.Grammar;
using Popsql.Text;

namespace Popsql
{
	/// <summary>
	/// Provides extension methods for writing SQL expression trees to <see cref="IDbCommand"/> instances.
	/// </summary>
	public static class ToCommandExtensions
	{
		/// <summary>
		/// Converts the specified SQL expression tree builder to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The SQL expression tree builder to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the SQL expression tree builder are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand ToCommand<T>(this ISqlGo<T> sql, IDbConnection connection)
			where T : SqlStatement
		{
			if (sql == null) throw new ArgumentNullException(nameof(sql));
			return connection.CreateCommandInternal(sql.Go());
		}

		/// <summary>
		/// Converts the specified <see cref="SqlSelect"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlSelect"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlSelect"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand ToCommand(this SqlSelect sql, IDbConnection connection)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlUnion"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlSelect"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlSelect"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand ToCommand(this SqlUnion sql, IDbConnection connection)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlInsert"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlInsert"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlInsert"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand ToCommand(this SqlInsert sql, IDbConnection connection)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlUpdate"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlUpdate"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlUpdate"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand ToCommand(this SqlUpdate sql, IDbConnection connection)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlDelete"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlDelete"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlDelete"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand ToCommand(this SqlDelete sql, IDbConnection connection)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified SQL expression tree builder to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The SQL expression tree builder to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the SQL expression tree builder are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand CreateCommand<T>(this IDbConnection connection, ISqlGo<T> sql)
			where T : SqlStatement
		{
			if (sql == null) throw new ArgumentNullException(nameof(sql));
			return connection.CreateCommandInternal(sql.Go());
		}

		/// <summary>
		/// Converts the specified <see cref="SqlDelete"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlDelete"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlDelete"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand CreateCommand(this IDbConnection connection, SqlDelete sql)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlUpdate"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlUpdate"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlUpdate"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand CreateCommand(this IDbConnection connection, SqlUpdate sql)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlInsert"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlInsert"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlInsert"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand CreateCommand(this IDbConnection connection, SqlInsert sql)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlUnion"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlUnion"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlUnion"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand CreateCommand(this IDbConnection connection, SqlUnion sql)
		{
			return connection.CreateCommandInternal(sql);
		}

		/// <summary>
		/// Converts the specified <see cref="SqlSelect"/> expression tree to an <see cref="IDbCommand"/>
		/// instance for the specified <paramref name="connection" />.
		/// </summary>
		/// <param name="sql">
		/// The <see cref="SqlSelect"/> to convert to an <see cref="IDbCommand"/>.
		/// </param>
		/// <param name="connection">
		/// The <see cref="IDbConnection"/> for which to create an <see cref="IDbCommand"/>.
		/// </param>
		/// <returns>
		/// An <see cref="IDbCommand"/> for the specified <paramref name="connection"/> that reprents
		/// the specified SQL expression tree.
		/// </returns>
		/// <remarks>
		/// Any <see cref="SqlParameter"/> members of the <see cref="SqlSelect"/> expression are
		/// automatically converted to <see cref="IDbDataParameter"/> instances and attached to
		/// the returned <see cref="IDbCommand"/>.
		/// </remarks>
		public static IDbCommand CreateCommand(this IDbConnection connection, SqlSelect sql)
		{
			return connection.CreateCommandInternal(sql);
		}

		[SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Popsql uses paraeterized queries.")]
		private static IDbCommand CreateCommandInternal(this IDbConnection connection, SqlStatement sql, SqlDialect dialect = null)
		{
			if (sql == null) throw new ArgumentNullException(nameof(sql));
			if (connection == null) throw new ArgumentNullException(nameof(connection));

			var command = connection.CreateCommand();
			command.CommandText = sql.ToSqlInternal(
				dialect,
				p =>
					{
						if (command.Parameters.Contains(p.ParameterName))
						{
							var existingParameter = (IDbDataParameter)command.Parameters[p.ParameterName];
							if (existingParameter.Value != p.Value)
							{
								throw new InvalidOperationException(
									$"A parameter with the name '{p.ParameterName}' already exists with a different value.");
							}
						}
						else
						{
							IDbDataParameter parameter = command.CreateParameter();
							parameter.ParameterName = p.ParameterName;
							parameter.Value = p.Value;
							command.Parameters.Add(parameter);
						}
					});

			return command;
		}
	}
}
