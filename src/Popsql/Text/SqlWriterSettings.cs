﻿namespace Popsql.Text
{
	/// <summary>
	/// Specifies a set of options that controls the output of a <see cref="SqlWriter"/> object.
	/// </summary>
	public sealed class SqlWriterSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlWriterSettings"/> class using default settings.
		/// </summary>
		public SqlWriterSettings()
		{
		}

		/// <summary>
		/// Gets or sets a value indicating whether or not SQL keywords are written in lower-case instead of
		/// the default (upper-case).
		/// </summary>
		/// <value>
		/// <see langword="true"/> to write SQL keywords in lower-case; otherwise <see langword="false"/>
		/// </value>
		public bool WriteKeywordsInLowerCase
		{
			get;
			set;
		}
	}
}
