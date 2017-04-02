using System;
using System.Collections;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Popsql.Visitors;
using Xunit;

namespace Popsql.Tests
{
	public class SqlGroupByTests
	{
		[Fact]
		public void Ctor_WithNullColumn_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlGroupBy(null));
		}

		[Fact]
		public void ExpressionType_ReturnsOrderBy()
		{
			var groupBy = new SqlGroupBy("Name");
			Assert.Equal(SqlExpressionType.GroupBy, groupBy.ExpressionType);
		}

		[Fact]
		public void GetEnumerator_ReturnsEnumerator()
		{
			var values = new SqlGroupBy("Age");

			var enumerator = ((IEnumerable)values).GetEnumerator();
			Assert.NotNull(enumerator);

			int count = 0;
			while (enumerator.MoveNext())
			{
				count++;
			}
			Assert.Equal(1, count);
		}

		[Fact]
		public void Accept_WithHaving_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = Sql
				.Select("Age")
				.From("User")
				.GroupBy("Age")
				.Having(SqlExpression.GreaterThanOrEqual("Age", 18))
				.OrderBy("Age", SqlSortOrder.Descending)
				.Go();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlSelect>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlFrom>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlGroupBy>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlHaving>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlOrderBy>()), Times.Once);
		}

		[Fact]
		public void Accept_WithoutHaving_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = Sql
				.Select("Age")
				.From("User")
				.GroupBy("Age")
				.OrderBy("Age", SqlSortOrder.Descending)
				.Go();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlSelect>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlFrom>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlGroupBy>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlHaving>()), Times.Never);
			mock.Verify(_ => _.Visit(It.IsAny<SqlOrderBy>()), Times.Once);
		}

		[Fact]
		public void Accept_WithCast_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = Sql
				.Select(SqlExpression.Cast((SqlColumn)"Age", SqlDataType.BigInt()))
				.From("User")
				.Go();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlCast>()), Times.Once);
		}
	}
}