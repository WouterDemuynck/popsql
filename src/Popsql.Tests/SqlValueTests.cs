using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlValueTests
	{
		[Fact]
		public void ImplicitConversion_FromShort_ReturnsConstant()
		{
			short expected = 5;
			SqlValue actual = expected;

			Assert.IsType<SqlConstant>(actual);
			Assert.Equal(expected, ((SqlConstant)actual).Value);
		}

		[Fact]
		public void ImplicitConversion_FromLong_ReturnsConstant()
		{
			long expected = 5;
			SqlValue actual = expected;

			Assert.IsType<SqlConstant>(actual);
			Assert.Equal(expected, ((SqlConstant)actual).Value);
		}

		[Fact]
		public void ImplicitConversion_FromString_ReturnsConstant()
		{
			string expected = "Hello, World!";
			SqlValue actual = expected;

			Assert.IsType<SqlConstant>(actual);
			Assert.Equal(expected, ((SqlConstant)actual).Value);
		}

		[Fact]
		public void ImplicitConversion_FromGuid_ReturnsConstant()
		{
			Guid expected = Guid.NewGuid();
			SqlValue actual = expected;

			Assert.IsType<SqlConstant>(actual);
			Assert.Equal(expected, ((SqlConstant)actual).Value);
		}

		[Fact]
		public void ImplicitConversion_FromFloat_ReturnsConstant()
		{
			float expected = 3.14159f;
			SqlValue actual = expected;

			Assert.IsType<SqlConstant>(actual);
			Assert.Equal(expected, ((SqlConstant)actual).Value);
		}

		[Fact]
		public void ImplicitConversion_FromDouble_ReturnsConstant()
		{
			double expected = Math.PI;
			SqlValue actual = expected;

			Assert.IsType<SqlConstant>(actual);
			Assert.Equal(expected, ((SqlConstant)actual).Value);
		}

		[Fact]
		public void ImplicitConversion_FromDateTime_ReturnsConstant()
		{
			DateTime expected = DateTime.Now;
			SqlValue actual = expected;

			Assert.IsType<SqlConstant>(actual);
			Assert.Equal(expected, ((SqlConstant)actual).Value);
		}

		[Fact]
		public void ImplicitConversion_FromDateTimeOffset_ReturnsConstant()
		{
			DateTimeOffset expected = DateTimeOffset.Now;
			SqlValue actual = expected;

			Assert.IsType<SqlConstant>(actual);
			Assert.Equal(expected, ((SqlConstant)actual).Value);
		}
	}
}
