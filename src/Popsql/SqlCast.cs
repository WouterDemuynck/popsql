using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL CAST expression.
	/// </summary>
	public class SqlCast : SqlValue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlCast"/> class using the 
		/// specified <paramref name="value"/> and <paramref name="dataType"/>.
		/// </summary>
		/// <param name="value">
		/// The <see cref="SqlValue"/> to convert.
		/// </param>
		/// <param name="dataType">
		/// The <see cref="SqlDataType"/> to convert to.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the <paramref name="dataType"/> argument is <see langword="null"/>.
		/// </exception>
		public SqlCast(SqlValue value, SqlDataType dataType)
		{
			if (value == null) value = SqlConstant.Null;
			if (dataType == null) throw new ArgumentNullException(nameof(dataType));

			Value = value;
			DataType = dataType;
		}

		/// <summary>
		/// Gets the value to be converted.
		/// </summary>
		public SqlValue Value
		{
			get;
		}

		/// <summary>
		/// Gets the data type to convert to.
		/// </summary>
		public SqlDataType DataType
		{
			get;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public override SqlExpressionType ExpressionType
			=> SqlExpressionType.Cast;
	}
}