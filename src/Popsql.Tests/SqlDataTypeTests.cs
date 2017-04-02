using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Xunit;

namespace Popsql.Tests
{
	public class SqlDataTypeTests
	{
		[Fact]
		public void Ctor_WithNullName_ThrowsArgumentNull()
		{
			var ex = Assert.Throws<ArgumentNullException>(() => new SqlSizedDataType(null));
			Assert.Equal("name", ex.ParamName);
		}

		[Fact]
		public void Ctor_WithNullSize_DoesNotThrow()
		{
			var dataType = new SqlSizedDataType(SqlDataTypeNames.Binary);
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.Binary, dataType.Name);
			Assert.Null(dataType.Size);
		}

		[Fact]
		public void ExpressionType_ReturnsDataType()
		{
			var constant = SqlDataType.Int();
			Assert.Equal(SqlExpressionType.DataType, constant.ExpressionType);
		}

		[Fact]
		public void Bit_ReturnsBitDataType()
		{
			var dataType = SqlDataType.Bit();
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.Bit, dataType.Name);
		}

		[Fact]
		public void TinyInt_ReturnsTinyIntDataType()
		{
			var dataType = SqlDataType.TinyInt();
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.TinyInt, dataType.Name);
		}

		[Fact]
		public void SmallInt_ReturnsSmallIntDataType()
		{
			var dataType = SqlDataType.SmallInt();
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.SmallInt, dataType.Name);
		}

		[Fact]
		public void Int_ReturnsIntDataType()
		{
			var dataType = SqlDataType.Int();
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.Int, dataType.Name);
		}

		[Fact]
		public void BigInt_ReturnsBigIntDataType()
		{
			var dataType = SqlDataType.BigInt();
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.BigInt, dataType.Name);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		[InlineData(int.MinValue)]
		public void Char_WithInvalidSize_ThrowsArgumentOutOfRange(int size)
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>(() => SqlDataType.Char(size));
			Assert.Equal(nameof(size), ex.ParamName);
			Assert.Equal(size, ex.ActualValue);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(20)]
		[InlineData(30)]
		[InlineData(int.MaxValue)]
		public void Char_ReturnsCharDataTypeOfSpecifiedSize(int size)
		{
			var dataType = SqlDataType.Char(size);
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.Char, dataType.Name);
			Assert.NotNull(dataType.Size);
			Assert.Equal(size, dataType.Size);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		[InlineData(int.MinValue)]
		public void VarChar_WithInvalidSize_ThrowsArgumentOutOfRange(int size)
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>(() => SqlDataType.VarChar(size));
			Assert.Equal(nameof(size), ex.ParamName);
			Assert.Equal(size, ex.ActualValue);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(20)]
		[InlineData(30)]
		[InlineData(int.MaxValue)]
		public void VarChar_ReturnsVarCharDataTypeOfSpecifiedSize(int size)
		{
			var dataType = SqlDataType.VarChar(size);
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.VarChar, dataType.Name);
			Assert.NotNull(dataType.Size);
			Assert.Equal(size, dataType.Size);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		[InlineData(int.MinValue)]
		public void Binary_WithInvalidSize_ThrowsArgumentOutOfRange(int size)
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>(() => SqlDataType.Binary(size));
			Assert.Equal(nameof(size), ex.ParamName);
			Assert.Equal(size, ex.ActualValue);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(20)]
		[InlineData(30)]
		[InlineData(int.MaxValue)]
		public void Binary_ReturnsBinaryDataTypeOfSpecifiedSize(int size)
		{
			var dataType = SqlDataType.Binary(size);
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.Binary, dataType.Name);
			Assert.NotNull(dataType.Size);
			Assert.Equal(size, dataType.Size);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		[InlineData(int.MinValue)]
		public void VarBinary_WithInvalidSize_ThrowsArgumentOutOfRange(int size)
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>(() => SqlDataType.VarBinary(size));
			Assert.Equal(nameof(size), ex.ParamName);
			Assert.Equal(size, ex.ActualValue);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(20)]
		[InlineData(30)]
		[InlineData(int.MaxValue)]
		public void VarBinary_ReturnsVarBinaryDataTypeOfSpecifiedSize(int size)
		{
			var dataType = SqlDataType.VarBinary(size);
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.VarBinary, dataType.Name);
			Assert.NotNull(dataType.Size);
			Assert.Equal(size, dataType.Size);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(-10)]
		[InlineData(int.MinValue)]
		public void VarBinary_WithInvalidPrecision_ThrowsArgumentOutOfRange(int precision)
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>(() => SqlDataType.Float(precision));
			Assert.Equal(nameof(precision), ex.ParamName);
			Assert.Equal(precision, ex.ActualValue);
		}

		[Theory]
		[InlineData(10)]
		[InlineData(20)]
		[InlineData(null)]
		public void Float_ReturnsFloatDataTypeOfSpecifiedPrecision(int? precision)
		{
			var dataType = SqlDataType.Float(precision);
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.Float, dataType.Name);
			Assert.Equal(precision, dataType.Precision);
		}

		[Theory]
		[InlineData(null, 5)]
		[InlineData(5, 6)]
		[InlineData(5, -1)]
		[InlineData(5, -2)]
		public void Decimal_WithInvalidScale_ThrowsArgumentOutOfRange(int? precision, int? scale)
		{
			if (precision == null && scale != null)
			{
				var ex = Assert.Throws<ArgumentException>(() => SqlDataType.Decimal(precision, scale));
				Assert.Equal(nameof(scale), ex.ParamName);
			}
			else
			{
				var ex = Assert.Throws<ArgumentOutOfRangeException>(() => SqlDataType.Decimal(precision, scale));
				Assert.Equal(nameof(scale), ex.ParamName);
				Assert.Equal(scale, ex.ActualValue);
			}
		}

		[Theory]
		[InlineData(10, 0)]
		[InlineData(15, 15)]
		[InlineData(20, 10)]
		[InlineData(15, null)]
		[InlineData(null, null)]
		public void Decimal_ReturnsDecimalDataTypeOfSpecifiedPrecisionAndScale(int? precision, int? scale)
		{
			var dataType = SqlDataType.Decimal(precision, scale);
			Assert.NotNull(dataType);
			Assert.Equal(SqlDataTypeNames.Decimal, dataType.Name);
			Assert.Equal(precision, dataType.Precision);
			Assert.Equal(scale, dataType.Scale);
		}

		[Theory]
		[MemberData(nameof(EqualsData), false)]
		public void Equals_WithObject_ReturnsCorrectly(SqlDataType dataType, object other, bool expected)
		{
			Assert.Equal(expected, dataType.Equals(other));
		}

		[Theory]
		[InlineData("FOOBAR")]
		[InlineData("MARVIN")]
		[InlineData("DATA")]
		public void GetHashCode_ReturnsHashCodeOfNameProperty(string name)
		{
			var dataType = new SqlDataType(name);
			Assert.Equal(name.GetHashCode(), dataType.GetHashCode());
		}

		[Theory]
		[MemberData(nameof(EqualsData), true)]
		public void Equals_WithSqlDataType_ReturnsCorrectly(SqlDataType dataType, SqlDataType other, bool expected)
		{
			Assert.Equal(expected, dataType.Equals(other));
		}

		public static IEnumerable<object[]> EqualsData(bool exactTypeOnly)
		{
			var dataType = new SqlDataType("FOOBAR");
			if (!exactTypeOnly)
			{
				yield return new object[] { dataType, "FOOBAR", false };
			}
			yield return new object[] { dataType, (SqlDataType)null, false };
			yield return new object[] { dataType, new SqlDataType("NONE"), false };
			yield return new object[] { dataType, new SqlDataType("FOOBAR"), true };
			yield return new object[] { dataType, dataType, true };

			yield return new object[] { SqlDataType.Int(), SqlDataType.Int(), true };
			yield return new object[] { SqlDataType.Int(), SqlDataType.BigInt(), false };
			yield return new object[] { SqlDataType.VarChar(5), SqlDataType.VarChar(10), false };
			yield return new object[] { SqlDataType.VarChar(5), SqlDataType.Char(10), false };
			yield return new object[] { SqlDataType.VarChar(5), SqlDataType.VarChar(5), true };
			yield return new object[] { SqlDataType.VarChar(5), SqlDataType.Float(5), false };
			yield return new object[] { SqlDataType.VarChar(5), SqlDataType.Float(6), false };
			yield return new object[] { SqlDataType.VarChar(5), null, false };
			yield return new object[] { SqlDataType.Float(), SqlDataType.Float(10), false };
			yield return new object[] { SqlDataType.Float(), new SqlPrecisionDataType("DOUBLE"), false };
			yield return new object[] { SqlDataType.Float(), SqlDataType.Float(), true };
			yield return new object[] { SqlDataType.Float(5), SqlDataType.Float(10), false };
			yield return new object[] { SqlDataType.Float(5), SqlDataType.Float(5), true };
			yield return new object[] { SqlDataType.Float(5), SqlDataType.VarChar(42), false };
			yield return new object[] { SqlDataType.Float(5), null, false };
			yield return new object[] { SqlDataType.Decimal(), SqlDataType.Decimal(10, 3), false };
			yield return new object[] { SqlDataType.Decimal(), SqlDataType.Float(10), false };
			yield return new object[] { SqlDataType.Decimal(), SqlDataType.Decimal(), true };
			yield return new object[] { SqlDataType.Decimal(5, 3), SqlDataType.Decimal(10, 3), false };
			yield return new object[] { SqlDataType.Decimal(5, 3), SqlDataType.Decimal(5, 3), true };
			yield return new object[] { SqlDataType.Decimal(5, 3), SqlDataType.VarChar(42), false };
			yield return new object[] { SqlDataType.Decimal(5, 3), SqlDataType.Decimal(5, 4), false };
			yield return new object[] { SqlDataType.Decimal(5, 3), null, false };


			var self = (SqlDataType)SqlDataType.VarChar(5);
			yield return new object[] { self, self, true };

			self = SqlDataType.Float(5);
			yield return new object[] { self, self, true };

			self = SqlDataType.Decimal(10, 3);
			yield return new object[] { self, self, true };
		}

		[Theory]
		[MemberData(nameof(GetHashCodeData))]
		public void GetHashCode_ReturnsCorrectHashCode(SqlDataType dataType, SqlDataType other, bool expected)
		{
			Assert.Equal(expected, dataType.GetHashCode().Equals(other?.GetHashCode() ?? 0));
		}

		public static IEnumerable<object[]> GetHashCodeData()
		{
			yield return new object[] { new SqlDataType("FOOBAR"), new SqlDataType("FOOBAR"), true };
			yield return new object[] { new SqlDataType("FOOBAR"), new SqlDataType("BARFOO"), false };
			yield return new object[] { new SqlDataType("FOOBAR"), new SqlSizedDataType("FOOBAR", 5), false };
			yield return new object[] { new SqlDataType("FOOBAR"), new SqlScaledDataType("FOOBAR", 5, 3), false };
			yield return new object[] { new SqlDataType("FOOBAR"), new SqlPrecisionDataType("FOOBAR", 5), false };

			yield return new object[] { new SqlSizedDataType("FOOBAR"), new SqlSizedDataType("FOOBAR"), true };
			yield return new object[] { new SqlSizedDataType("FOOBAR", 5), new SqlSizedDataType("FOOBAR", 5), true };
			yield return new object[] { new SqlSizedDataType("FOOBAR", 5), new SqlSizedDataType("BARFOO", 5), false };
			yield return new object[] { new SqlSizedDataType("FOOBAR", 5), new SqlSizedDataType("FOOBAR", 6), false };
			yield return new object[] { new SqlSizedDataType("FOOBAR"), new SqlSizedDataType("FOOBAR", 5), false };
			yield return new object[] { new SqlSizedDataType("FOOBAR"), new SqlDataType("FOOBAR"), false };
			yield return new object[] { new SqlSizedDataType("FOOBAR"), new SqlScaledDataType("FOOBAR", 5, 3), false };

			yield return new object[] { new SqlScaledDataType("FOOBAR"), new SqlScaledDataType("FOOBAR"), true };
			yield return new object[] { new SqlScaledDataType("FOOBAR", 5), new SqlScaledDataType("FOOBAR", 5), true };
			yield return new object[] { new SqlScaledDataType("FOOBAR", 5, 3), new SqlScaledDataType("FOOBAR", 5, 3), true };
			yield return new object[] { new SqlScaledDataType("FOOBAR", 5, 3), new SqlScaledDataType("BARFOO", 5, 3), false };
			yield return new object[] { new SqlScaledDataType("FOOBAR"), new SqlDataType("BARFOO"), false };
			yield return new object[] { new SqlScaledDataType("FOOBAR"), new SqlDataType("FOOBAR"), false };
			yield return new object[] { new SqlScaledDataType("FOOBAR", 5), new SqlScaledDataType("BARFOO", 5), false };
			yield return new object[] { new SqlScaledDataType("FOOBAR", 5), new SqlScaledDataType("BARFOO", 6), false };
			yield return new object[] { new SqlScaledDataType("FOOBAR"), new SqlScaledDataType("FOOBAR", 5), false };
			yield return new object[] { new SqlScaledDataType("FOOBAR"), new SqlScaledDataType("FOOBAR", 5, 3), false };
			yield return new object[] { new SqlScaledDataType("FOOBAR", 5, 3), new SqlScaledDataType("FOOBAR", 6, 3), false };
			yield return new object[] { new SqlScaledDataType("FOOBAR", 5, 3), new SqlScaledDataType("FOOBAR", 5, 4), false };

			yield return new object[] { new SqlPrecisionDataType("FOOBAR"), new SqlPrecisionDataType("FOOBAR"), true };
			yield return new object[] { new SqlPrecisionDataType("FOOBAR", 5), new SqlPrecisionDataType("FOOBAR", 5), true };
			yield return new object[] { new SqlPrecisionDataType("FOOBAR", 5), new SqlPrecisionDataType("BARFOO", 5), false };
			yield return new object[] { new SqlPrecisionDataType("FOOBAR", 5), new SqlPrecisionDataType("FOOBAR", 6), false };
			yield return new object[] { new SqlPrecisionDataType("FOOBAR"), new SqlPrecisionDataType("FOOBAR", 5), false };
			yield return new object[] { new SqlPrecisionDataType("FOOBAR"), new SqlDataType("FOOBAR"), false };
			yield return new object[] { new SqlPrecisionDataType("FOOBAR"), new SqlSizedDataType("FOOBAR", 5), false };
			yield return new object[] { new SqlPrecisionDataType("FOOBAR"), new SqlScaledDataType("FOOBAR", 5), false };
			yield return new object[] { new SqlPrecisionDataType("FOOBAR"), new SqlScaledDataType("FOOBAR", 5, 3), false };
		}
	}
}