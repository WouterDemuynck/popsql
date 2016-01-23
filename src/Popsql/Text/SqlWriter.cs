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
		{
			if (builder == null) throw new ArgumentNullException("builder");

			_writer = new StringWriter(builder, CultureInfo.InvariantCulture);
			_canDisposeWriter = true;
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
		{
			if (writer == null) throw new ArgumentNullException("writer");

			_writer = writer;
			_canDisposeWriter = false;
		}

		/// <summary>
		/// Finalizes the current instance of the <see cref="SqlWriter"/> class.
		/// </summary>
		~SqlWriter()
		{
			Dispose(false);
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
