using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlLimitTests
	{
		[Fact]
		public void Ctor_WithNegativeOffset_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlLimit(-1, 1));
		}

		[Fact]
		public void Ctor_WithCountLessThanOne_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlLimit(0, 0));
		}

		[Fact]
		public void Ctor_WithOffsetOnly_SetsOffsetProperty()
		{
			var fetchFirst = new SqlLimit(42, null);
			Assert.NotNull(fetchFirst);
			Assert.Equal(42, fetchFirst.Offset);
			Assert.Null(fetchFirst.Count);
		}

		[Fact]
		public void Ctor_WithCountProperty_SetsCountProperty()
		{
			var fetchFirst = new SqlLimit(null, 20);
			Assert.NotNull(fetchFirst);
			Assert.Null(fetchFirst.Offset);
			Assert.Equal(20, fetchFirst.Count);
		}

		[Fact]
		public void Ctor_WithOffsetAndCount_SetsOffsetAndCountProperties()
		{
			var fetchFirst = new SqlLimit(42, 25);
			Assert.NotNull(fetchFirst);
			Assert.Equal(42, fetchFirst.Offset);
			Assert.Equal(25, fetchFirst.Count);
		}

		[Fact]
		public void Ctor_WithNullOffsetAndCount_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlLimit(null, null));
		}

		[Fact]
		public void ExpressionType_ReturnsLimit()
		{
			var fetchFirst = new SqlLimit(42, null);
			Assert.Equal(SqlExpressionType.Limit, fetchFirst.ExpressionType);
		}
	}
}