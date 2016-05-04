﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace Popsql.Text
{
	/// <summary>
	/// Represents a writer that provides a means of generating streams of SQL statement text.
	/// </summary>
	public class SqlWriter : IDisposable
	{
		private TextWriter _writer;
		private readonly bool _canDisposeWriter;
		private bool _isDisposed;
		private bool _hasPendingSpace;

		/// <summary>
		/// Represents the SQL NULL value.
		/// </summary>
		public const string SqlNull = "NULL";

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
		/// specified <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> to write to.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="builder"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWriter(StringBuilder builder)
			: this(builder, SqlDialect.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
		/// specified <see cref="StringBuilder"/> using the specified <paramref name="settings"/>.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> to write to.
		/// </param>
		/// <param name="settings">
		/// The <see cref="SqlWriterSettings"/> object used to configure the new <see cref="SqlWriter"/>
		/// instance. If this <see langword="null"/>, a <see cref="SqlWriterSettings"/> object with default
		/// settings is used.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="builder"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWriter(StringBuilder builder, SqlWriterSettings settings)
			: this(builder, SqlDialect.Default, settings)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
		/// specified <see cref="StringBuilder"/> using the specified <see cref="SqlDialect"/>.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> to write to.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> to use while writing.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="builder"/> or <paramref name="dialect"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWriter(StringBuilder builder, SqlDialect dialect)
			: this(builder, dialect, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
		/// specified <see cref="StringBuilder"/> using the specified <see cref="SqlDialect" /> and
		/// <paramref name="settings"/>.
		/// </summary>
		/// <param name="builder">
		/// The <see cref="StringBuilder"/> to write to.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> to use while writing.
		/// </param>
		/// <param name="settings">
		/// The <see cref="SqlWriterSettings"/> object used to configure the new <see cref="SqlWriter"/>
		/// instance. If this <see langword="null"/>, a <see cref="SqlWriterSettings"/> object with default
		/// settings is used.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="builder"/> or <paramref name="dialect"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWriter(StringBuilder builder, SqlDialect dialect, SqlWriterSettings settings)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (dialect == null) throw new ArgumentNullException(nameof(dialect));
			if (settings == null) settings = new SqlWriterSettings();

			_writer = new StringWriter(builder, CultureInfo.InvariantCulture);
			_canDisposeWriter = true;
			Dialect = dialect;
			Settings = settings;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
		/// specified <see cref="TextWriter"/>.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="TextWriter"/> to write to.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="writer"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWriter(TextWriter writer)
			: this(writer, SqlDialect.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
		/// specified <see cref="TextWriter"/> using the specified <paramref name="settings"/>.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="TextWriter"/> to write to.
		/// </param>
		/// <param name="settings">
		/// The <see cref="SqlWriterSettings"/> object used to configure the new <see cref="SqlWriter"/>
		/// instance. If this <see langword="null"/>, a <see cref="SqlWriterSettings"/> object with default
		/// settings is used.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="writer"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWriter(TextWriter writer, SqlWriterSettings settings)
			: this(writer, SqlDialect.Default, settings)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
		/// specified <see cref="TextWriter"/> using the specified <see cref="SqlDialect" />.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="TextWriter"/> to write to.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> to use while writing.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="writer"/> or <paramref name="dialect"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWriter(TextWriter writer, SqlDialect dialect)
			: this(writer, dialect, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriter"/> class that writes to the
		/// specified <see cref="TextWriter"/> using the specified <see cref="SqlDialect" /> and
		/// <paramref name="settings"/>.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="TextWriter"/> to write to.
		/// </param>
		/// <param name="dialect">
		/// The <see cref="SqlDialect"/> to use while writing.
		/// </param>
		/// <param name="settings">
		/// The <see cref="SqlWriterSettings"/> object used to configure the new <see cref="SqlWriter"/>
		/// instance. If this <see langword="null"/>, a <see cref="SqlWriterSettings"/> object with default
		/// settings is used.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="writer"/> or <paramref name="dialect"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlWriter(TextWriter writer, SqlDialect dialect, SqlWriterSettings settings)
		{
			if (writer == null) throw new ArgumentNullException(nameof(writer));
			if (dialect == null) throw new ArgumentNullException(nameof(dialect));
			if (settings == null) settings = new SqlWriterSettings();

			_writer = writer;
			_canDisposeWriter = false;
			Dialect = dialect;
			Settings = settings;
		}

		/// <summary>
		/// Finalizes the current instance of the <see cref="SqlWriter"/> class.
		/// </summary>
		~SqlWriter()
		{
			Dispose(false);
		}

		/// <summary>
		/// Gets the currently used SQL dialect.
		/// </summary>
		protected SqlDialect Dialect
		{
			get;
		}

		/// <summary>
		/// Gets the <see cref="SqlWriterSettings"/> used to create this <see cref="SqlWriter"/> instance.
		/// </summary>
		protected SqlWriterSettings Settings
		{
			get;
		}

		/// <summary>
		/// Writes the specified <see cref="SqlKeyword"/> to the output stream.
		/// </summary>
		/// <param name="keyword">
		/// The <see cref="SqlKeyword"/> to write to the output stream.
		/// </param>
		public void WriteKeyword(SqlKeyword keyword)
		{
			EnsureNotDisposed();
			Write(Settings.WriteKeywordsInLowerCase 
				? keyword.Keyword.ToLowerInvariant() 
				: keyword.Keyword.ToUpperInvariant());
		}

		/// <summary>
		/// Writes the specified <paramref name="identifier"/> to the output stream.
		/// </summary>
		/// <param name="identifier">
		/// The <see cref="SqlIdentifier"/> to write to the output stream.
		/// </param>
		public void WriteIdentifier(SqlIdentifier identifier)
		{
			EnsureNotDisposed();
			if (identifier == null) throw new ArgumentNullException(nameof(identifier));

			for (int index = 0; index < identifier.Segments.Length; index++)
			{
				var segment = identifier.Segments[index];
				if (index > 0)
				{
					WriteRaw(".");
					WriteRaw(Dialect.FormatTableName(segment));
				}
				else
				{
					Write(Dialect.FormatTableName(segment));
				}
			}
		}

		/// <summary>
		/// Writes the specified operator to the output stream.
		/// </summary>
		/// <param name="operator">
		/// The operator to write to the output stream.
		/// </param>
		public void WriteOperator(SqlBinaryOperator @operator)
		{
			EnsureNotDisposed();

			switch (@operator)
			{
				case SqlBinaryOperator.And:
					Write("AND");
					break;

				case SqlBinaryOperator.Equal:
					Write("=");
					break;

				case SqlBinaryOperator.GreaterThan:
					Write(">");
					break;

				case SqlBinaryOperator.GreaterThanOrEqual:
					Write(">=");
					break;

				case SqlBinaryOperator.LessThan:
					Write("<");
					break;

				case SqlBinaryOperator.LessThanOrEqual:
					Write("<=");
					break;

				case SqlBinaryOperator.Like:
					Write("LIKE");
					break;

				case SqlBinaryOperator.NotEqual:
					Write("<>");
					break;

				case SqlBinaryOperator.Or:
					Write("OR");
					break;

				default:
					throw new InvalidEnumArgumentException(nameof(@operator), (int)@operator, typeof(SqlBinaryOperator));
			}
		}

		/// <summary>
		/// Writes the specified sort order to the output stream.
		/// </summary>
		/// <param name="sortOrder">
		/// The sort order to write to the output stream.
		/// </param>
		public void WriteSortOrder(SqlSortOrder sortOrder)
		{
			EnsureNotDisposed();
			switch (sortOrder)
			{
				case SqlSortOrder.Ascending:
					if (Settings.WriteAscendingSortOrder)
					{
						WriteKeyword(SqlKeywords.Ascending);
					}
					break;

				case SqlSortOrder.Descending:
					WriteKeyword(SqlKeywords.Descending);
					break;

				default:
					throw new InvalidEnumArgumentException(nameof(sortOrder), (int)sortOrder, typeof(SqlBinaryOperator));
			}
		}

		/// <summary>
		/// Writes the specified SQL value to the output stream.
		/// </summary>
		/// <param name="value">
		/// The value to write to the output stream.
		/// </param>
		public void WriteValue(object value)
		{
			EnsureNotDisposed();

			if (value == null)
			{
				Write(SqlNull);
				return;
			}

			switch (Type.GetTypeCode(value.GetType()))
			{
				case TypeCode.String:
				case TypeCode.Char:
					Write(Dialect.FormatString(Convert.ToString(value, CultureInfo.InvariantCulture)));
					break;

				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					Write(Convert.ToString(value, CultureInfo.InvariantCulture));
					break;

				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					Write(Convert.ToString(value, CultureInfo.InvariantCulture));
					break;

				default:
					Write(Dialect.FormatString(Convert.ToString(value, CultureInfo.InvariantCulture)));
					break;
			}
		}

		/// <summary>
		/// Writes an opening parenthesis to the output stream.
		/// </summary>
		public void WriteOpenParenthesis()
		{
			EnsureNotDisposed();
			Write("(");
			_hasPendingSpace = false;
		}

		/// <summary>
		/// Writes an closing parenthesis to the output stream.
		/// </summary>
		public void WriteCloseParenthesis()
		{
			EnsureNotDisposed();
			WriteRaw(")");
		}

		/// <summary>
		/// Writes the specified string to the output stream, including any pending formatting.
		/// </summary>
		/// <param name="value">
		/// The <see cref="String"/> to write to the output stream.
		/// </param>
		protected void Write(string value)
		{
			EnsureNotDisposed();
			if (_hasPendingSpace)
			{
				WriteRaw(" ");
				_hasPendingSpace = false;
			}

			WriteRaw(value);
			_hasPendingSpace = true;
		}

		/// <summary>
		/// Writes the specified string to the output stream, ignoring any pending formatting.
		/// </summary>
		/// <param name="value">
		/// The <see cref="String"/> to write to the output stream.
		/// </param>
		public void WriteRaw(string value)
		{
			EnsureNotDisposed();
			_writer.Write(value);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases all unmanaged resources used by the current instance of the <see cref="SqlWriter"/> class and, 
		/// optionally, releases all managed resources as well.
		/// </summary>
		/// <param name="isDisposing">
		/// <see langword="true"/> to release managed resources as well as unmanaged resources; otherwise, <see langword="false"/>.
		/// </param>
		protected virtual void Dispose(bool isDisposing)
		{
			if (_isDisposed) return;

			if (isDisposing)
			{
				//_stateManager.Close();
			}

			if (_canDisposeWriter && _writer != null)
			{
				_writer.Dispose();
				_writer = null;
			}

			_isDisposed = true;
		}

		/// <summary>
		/// Throws an <see cref="ObjectDisposedException"/> when the current instance of the
		/// <see cref="SqlWriter"/> class has already been disposed.
		/// </summary>
		protected void EnsureNotDisposed()
		{
			if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
		}
	}
}
