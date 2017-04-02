using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlCastTests
	{
		[Fact]
		public void Ctor_WithNullValue_ConvertsToNullConstant()
		{
			var cast = new SqlCast(null, SqlDataType.BigInt());
			Assert.Same(SqlConstant.Null, cast.Value);
		}

		[Fact]
		public void Ctor_WithNullDataType_ThrowsArgumentNull()
		{
			var ex = Assert.Throws<ArgumentNullException>(() => new SqlCast(null, null));
			Assert.Equal("dataType", ex.ParamName);
		}

		[Fact]
		public void Ctor_WithValueAndDataType_SetsValueAndDataTypeProperties()
		{
			var cast = new SqlCast(5, SqlDataType.BigInt());
			Assert.Equal(5, cast.Value);
			Assert.Equal(SqlDataType.BigInt(), cast.DataType);
		}

		[Fact]
		public void ExpressionType_ReturnsCast()
		{
			var cast = new SqlCast(42, SqlDataType.Int());
			Assert.Equal(SqlExpressionType.Cast, cast.ExpressionType);
		}
	}
}