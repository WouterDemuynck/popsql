﻿using System;

namespace Popsql
{
	/// <summary>
	/// Represents a SQL data type.
	/// </summary>
	public class SqlDataType : SqlExpression
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDataType"/> class using the specified <paramref name="name"/>.
		/// </summary>
		/// <param name="name">
		/// The name of the data type.
		/// </param>
		public SqlDataType(SqlDataTypeName name)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));
			Name = name;
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Bit"/> data type.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Bit"/> data type.
		/// </returns>
		public static SqlDataType Bit()
		{
			return new SqlDataType(SqlDataTypeNames.Bit);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.TinyInt"/> data type.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.TinyInt"/> data type.
		/// </returns>
		public static SqlDataType TinyInt()
		{
			return new SqlDataType(SqlDataTypeNames.TinyInt);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.SmallInt"/> data type.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.SmallInt"/> data type.
		/// </returns>
		public static SqlDataType SmallInt()
		{
			return new SqlDataType(SqlDataTypeNames.SmallInt);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Int"/> data type.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Int"/> data type.
		/// </returns>
		public static SqlDataType Int()
		{
			return new SqlDataType(SqlDataTypeNames.Int);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.BigInt"/> data type.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.BigInt"/> data type.
		/// </returns>
		public static SqlDataType BigInt()
		{
			return new SqlDataType(SqlDataTypeNames.BigInt);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Binary"/> data type.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Binary"/> data type.
		/// </returns>
		public static SqlSizedDataType Binary(int size)
		{
			return new SqlSizedDataType(SqlDataTypeNames.Binary, size);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.VarBinary"/> data type.
		/// </summary>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.VarBinary"/> data type.
		/// </returns>
		public static SqlSizedDataType VarBinary(int size)
		{
			return new SqlSizedDataType(SqlDataTypeNames.VarBinary, size);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Char"/> data type
		/// of the specified <paramref name="size"/>.
		/// </summary>
		/// <param name="size">
		/// The size of the data type.
		/// </param>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Char"/> data type.
		/// </returns>
		public static SqlSizedDataType Char(int size)
		{
			return new SqlSizedDataType(SqlDataTypeNames.Char, size);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.VarChar"/> data type
		/// of the specified <paramref name="size"/>.
		/// </summary>
		/// <param name="size">
		/// The size of the data type.
		/// </param>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.VarChar"/> data type.
		/// </returns>
		public static SqlSizedDataType VarChar(int size)
		{
			return new SqlSizedDataType(SqlDataTypeNames.VarChar, size);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Float"/> data type
		/// of the specified <paramref name="precision"/>.
		/// </summary>
		/// <param name="precision">
		/// The precision of the data type.
		/// </param>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Float"/> data type.
		/// </returns>
		public static SqlPrecisionDataType Float(int? precision = null)
		{
			return new SqlPrecisionDataType(SqlDataTypeNames.Float, precision);
		}

		/// <summary>
		/// Returns a <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Decimal"/> data type
		/// of the specified <paramref name="precision"/> and <paramref name="scale"/>.
		/// </summary>
		/// <param name="precision">
		/// The precision of the data type.
		/// </param>
		/// <param name="scale">
		/// The scale of the data type.
		/// </param>
		/// <returns>
		/// A <see cref="SqlDataType"/> instance representing the <see cref="SqlDataTypeNames.Decimal"/> data type.
		/// </returns>
		public static SqlScaledDataType Decimal(int? precision = null, int? scale = null)
		{
			return new SqlScaledDataType(SqlDataTypeNames.Decimal, precision, scale);
		}

		/// <summary>
		/// Gets the name of the data type.
		/// </summary>
		public SqlDataTypeName Name
		{
			get;
		}

		/// <summary>
		/// Gets the expression type of this expression.
		/// </summary>
		public sealed override SqlExpressionType ExpressionType
			=> SqlExpressionType.DataType;

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the specified object  is equal to the current object; otherwise, <see langword="false"/>.
		/// </returns>
		/// <param name="obj">
		/// The object to compare with the current object.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals(Name, ((SqlDataType)obj).Name);
		}

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>
		/// A hash code for the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
	}
}