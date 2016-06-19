using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlFetchFirstTests
	{
		[Fact]
		public void Ctor_WithNegativeOffset_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlFetchFirst(-1, 1));
		}

		[Fact]
		public void Ctor_WithCountLessThanOne_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlFetchFirst(0, 0));
		}

		[Fact]
		public void Ctor_WithOffsetOnly_SetsOffsetProperty()
		{
			var fetchFirst = new SqlFetchFirst(42);
			Assert.NotNull(fetchFirst);
			Assert.Equal(42, fetchFirst.Offset);
			Assert.Null(fetchFirst.Count);
		}

		[Fact]
		public void Ctor_WithCountProperty_SetsCountProperty()
		{
			var fetchFirst = new SqlFetchFirst(null, 20);
			Assert.NotNull(fetchFirst);
			Assert.Null(fetchFirst.Offset);
			Assert.Equal(20, fetchFirst.Count);
		}

		[Fact]
		public void Ctor_WithOffsetAndCount_SetsOffsetAndCountProperties()
		{
			var fetchFirst = new SqlFetchFirst(42, 25);
			Assert.NotNull(fetchFirst);
			Assert.Equal(42, fetchFirst.Offset);
			Assert.Equal(25, fetchFirst.Count);
		}

		[Fact]
		public void Ctor_WithNullOffsetAndCount_ThrowsArgument()
		{
			Assert.Throws<ArgumentException>(() => new SqlFetchFirst(null, null));
		}

		[Fact]
		public void Count_CanSetProperty()
		{
			var fetchFirst = new SqlFetchFirst(0);
			Assert.NotNull(fetchFirst);
			Assert.Equal(0, fetchFirst.Offset);
			Assert.Null(fetchFirst.Count);

			fetchFirst.Count = 42;
			Assert.Equal(42, fetchFirst.Count);
		}

		[Fact]
		public void ExpressionType_ReturnsFetchFirst()
		{
			var fetchFirst = new SqlFetchFirst(42);
			Assert.Equal(SqlExpressionType.FetchFirst, fetchFirst.ExpressionType);
		}
	}
}