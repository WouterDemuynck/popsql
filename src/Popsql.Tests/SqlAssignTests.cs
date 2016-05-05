using System;
using System.Linq;
using Xunit;

namespace Popsql.Tests
{
	public class SqlAssignTests
	{
		[Fact]
		public void Ctor_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlAssign(null, null));
		}

		[Fact]
		public void Ctor_WithColumn_SetsColumnProperty()
		{
			var assign = new SqlAssign("Id", null);
			Assert.NotNull(assign.Column);
			Assert.Equal("Id", assign.Column.ColumnName.Segments.Single());
		}

		[Fact]
		public void Ctor_WithValue_SetsValueProperty()
		{
			var expected = new SqlConstant(5);
			var assign = new SqlAssign("Id", expected);

			Assert.NotNull(assign.Value);
			Assert.Equal(expected, assign.Value);
		}

		[Fact]
		public void Ctor_WithNullValue_SetsValuePropertyToNull()
		{
			var expected = SqlConstant.Null;
			var assign = new SqlAssign("Id", null);

			Assert.NotNull(assign.Value);
			Assert.Same(expected, assign.Value);
		}

		[Fact]
		public void ExpressionType_ReturnsAssign()
		{
			var constant = new SqlAssign("Id", 5);

			Assert.Equal(SqlExpressionType.Assign, constant.ExpressionType);
		}
	}
}