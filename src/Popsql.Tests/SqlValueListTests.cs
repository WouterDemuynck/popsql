using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlValueListTests
	{
		[Fact]
		public void Ctor_WithNullValues_DoesNotThrow()
		{
			var list = new SqlValueList(null);
			Assert.NotNull(list.Values);
		}

		[Fact]
		public void Ctor_WithValues_SetsValuesProperty()
		{
			var list = new SqlValueList(Enumerable.Range(1, 6).Cast<SqlValue>());

			Assert.NotNull(list.Values);
			var values = list.Values.ToArray();
			Assert.Equal(6, values.Length);
			Assert.Equal(1, values[0]);
			Assert.Equal(2, values[1]);
			Assert.Equal(3, values[2]);
			Assert.Equal(4, values[3]);
			Assert.Equal(5, values[4]);
			Assert.Equal(6, values[5]);
		}

		[Fact]
		public void ExpressionType_ReturnsValueList()
		{
			Assert.Equal(SqlExpressionType.ValueList, new SqlValueList(null).ExpressionType);
		}
	}
}