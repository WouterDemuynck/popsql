using System;
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
		private readonly SqlDialect _dialect;
		private readonly SqlWriterSettings _settings;
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
			_dialect = dialect;
			_settings = settings;
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
			_dialect = dialect;
			_settings = settings;
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
			get
			{
				return _dialect;
			}
		}

		/// <summary>
		/// Gets the <see cref="SqlWriterSettings"/> used to create this <see cref="SqlWriter"/> instance.
		/// </summary>
		protected SqlWriterSettings Settings
		{
			get
			{
				return _settings;
			}
		}

		/// <summary>
		/// Writes the specified string to the output stream, including any pending formatting.
		/// </summary>
		/// <param name="value">
		/// The <see cref="String"/> to write to the output stream.
		/// </param>
		protected void Write(string value)
		{
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
		protected void WriteRaw(string value)
		{
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
