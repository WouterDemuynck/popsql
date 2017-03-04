using System;
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
	}
}