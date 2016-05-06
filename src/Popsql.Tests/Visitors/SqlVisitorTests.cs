using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Popsql.Visitors;
using Xunit;

namespace Popsql.Tests.Visitors
{
	public class SqlVisitorTests
	{
		[Fact]
		public void VisitExpression_WithUnknownExpression_ThrowsInvalidOperation()
		{
			var mock = new Mock<SqlExpression>();
			mock.SetupGet(_ => _.ExpressionType).Returns((SqlExpressionType)int.MaxValue);
			var expression = mock.Object;

			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var visitor = fixture.Freeze<Mock<SqlVisitor>>().Object;

			Assert.Throws<InvalidOperationException>(() => visitor.Visit(expression));
		}

		[Fact]
		public void VisitExpression_WithSqlFunction_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression) new SqlFunction("SUM", Enumerable.Range(0, 5).Cast<SqlValue>()));
			mock.Verify(_ => _.Visit(It.IsAny<SqlExpression>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlFunction>()), Times.Once);
		}

		[Fact]
		public void VisitExpression_WithSqlIdentifier_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression)new SqlIdentifier("Blog.dbo.User"));
			mock.Verify(_ => _.Visit(It.IsAny<SqlExpression>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlIdentifier>()), Times.Once);
		}

		[Fact]
		public void VisitExpression_WithSqlJoin_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression)new SqlJoin(SqlJoinType.Left, "User", SqlExpression.Equal("Id", 5)));
			mock.Verify(_ => _.Visit(It.IsAny<SqlExpression>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlJoin>()), Times.Once);
		}

		[Fact]
		public void VisitExpression_WithSqlOn_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression)new SqlOn(SqlExpression.Equal("Id", 5)));
			mock.Verify(_ => _.Visit(It.IsAny<SqlExpression>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlOn>()), Times.Once);
		}

		[Fact]
		public void VisitExpression_WithSqlOrderBy_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression)new SqlOrderBy());
			mock.Verify(_ => _.Visit(It.IsAny<SqlExpression>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlOrderBy>()), Times.Once);
		}

		[Fact]
		public void VisitExpression_WithSqlParameter_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression)new SqlParameter("Id", 5));
			mock.Verify(_ => _.Visit(It.IsAny<SqlExpression>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlParameter>()), Times.Once);
		}

		[Fact]
		public void VisitExpression_WithSqlSubquery_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression)new SqlSubquery(Sql.Select("Id").From("User").Go(), "a"));
			mock.Verify(_ => _.Visit(It.IsAny<SqlExpression>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlSubquery>()), Times.Once);
		}

		[Fact]
		public void VisitExpression_WithSqlTable_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression)new SqlTable("User"));
			mock.Verify(_ => _.Visit(It.IsAny<SqlExpression>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlTable>()), Times.Once);
		}

		[Fact]
		public void VisitTableExpression_WithSqlSubquery_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlTableExpression)new SqlSubquery(Sql.Select("Id").From("User").Go(), "a"));
			mock.Verify(_ => _.Visit(It.IsAny<SqlSubquery>()), Times.Once);
		}

		[Fact]
		public void VisitTableExpression_WithUnknownTableExpression_ThrowsInvalidOperation()
		{
			var mock = new Mock<SqlTableExpression>("a");
			mock.SetupGet(_ => _.ExpressionType).Returns((SqlExpressionType)int.MaxValue);
			var expression = mock.Object;

			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var visitor = fixture.Freeze<Mock<SqlVisitor>>().Object;

			Assert.Throws<InvalidOperationException>(() => visitor.Visit(expression));
		}

		[Fact]
		public void VisitTableExpression_WithSqlTable_DispatchesVisit()
		{

			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlTableExpression)new SqlTable("User"));
			mock.Verify(_ => _.Visit(It.IsAny<SqlTable>()), Times.Once);
		}

		[Fact]
		public void VisitValue_WithUnknownSqlValue_ThrowsInvalidOperation()
		{
			var mock = new Mock<SqlValue>();
			mock.SetupGet(_ => _.ExpressionType).Returns((SqlExpressionType)int.MaxValue);
			var expression = mock.Object;

			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var visitor = fixture.Freeze<Mock<SqlVisitor>>().Object;

			Assert.Throws<InvalidOperationException>(() => visitor.Visit(expression));
		}

		[Fact]
		public void VisitValue_WithSqlFunction_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlValue)new SqlFunction("SUM", Enumerable.Range(0, 5).Cast<SqlValue>()));
			mock.Verify(_ => _.Visit(It.IsAny<SqlFunction>()), Times.Once);
		}

		[Fact]
		public void VisitValue_WithSqlColumn_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlValue)new SqlColumn("Id"));
			mock.Verify(_ => _.Visit(It.IsAny<SqlColumn>()), Times.Once);
		}

		[Fact]
		public void VisitExpression_WithSqlUnion_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlExpression)new SqlUnion(new SqlSelect(new[] { new SqlColumn("City") })));
			mock.Verify(_ => _.Visit(It.IsAny<SqlUnion>()), Times.Once);
		}

		[Fact]
		public void VisitValue_WithSqlConstant_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlValue)5);
			mock.Verify(_ => _.Visit(It.IsAny<SqlConstant>()), Times.Once);
		}

		[Fact]
		public void VisitValue_WithSqlParameter_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlValue)new SqlParameter("Id", 5));
			mock.Verify(_ => _.Visit(It.IsAny<SqlParameter>()), Times.Once);
		}

		[Fact]
		public void VisitValue_WithSqlValueList_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((SqlValue)new SqlValueList(Enumerable.Range(1, 6).Select(i => new SqlConstant(i))));
			mock.Verify(_ => _.Visit(It.IsAny<SqlValueList>()), Times.Once);
		}

		[Fact]
		public void Visit_WithSqlSortOrder_DispatchesVisit()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit(SqlSortOrder.Ascending);
			mock.Verify(_ => _.Visit(It.IsAny<SqlSortOrder>()), Times.Once);
		}

		[Fact]
		public void Visit_WithNullSqlValueCollection_DoesNotThrow()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((IEnumerable<SqlValue>)null);
		}

		[Fact]
		public void Visit_WithNullSqlColumnCollection_DoesNotThrow()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((IEnumerable<SqlColumn>)null);
		}

		[Fact]
		public void Visit_WithSqlColumnCollection_VisitsAllItems()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit(Enumerable.Range(0, 3).Select(i => new SqlColumn("Column" + i)));
			mock.Verify(_ => _.Visit(It.IsAny<IEnumerable<SqlColumn>>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlColumn>()), Times.Exactly(3));
		}

		[Fact]
		public void Visit_WithNullSqlValuesCollection_DoesNotThrow()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((IEnumerable<IEnumerable<SqlValue>>)null);
		}

		[Fact]
		public void Visit_WithSqlValuesCollection_VisitsAllItems()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit(Enumerable.Range(1, 3).Select(_ => Enumerable.Range(1, _).Select(i => new SqlConstant(i))));
			mock.Verify(_ => _.Visit(It.IsAny<IEnumerable<IEnumerable<SqlValue>>>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<IEnumerable<SqlValue>>()), Times.Exactly(3));
			mock.Verify(_ => _.Visit(It.IsAny<SqlConstant>()), Times.Exactly(6));
		}

		[Fact]
		public void Visit_WithNullSqlSortCollection_DoesNotThrow()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((IEnumerable<SqlSort>)null);
		}

		[Fact]
		public void Visit_WithSqlSortCollection_VisitsAllItems()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit(Enumerable.Range(0, 3).Select(i => new SqlSort("Column" + i, SqlSortOrder.Ascending)));
			mock.Verify(_ => _.Visit(It.IsAny<IEnumerable<SqlSort>>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlSort>()), Times.Exactly(3));
		}

		[Fact]
		public void Visit_WithNullSqlAssignCollection_DoesNotThrow()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit((IEnumerable<SqlAssign>)null);
		}

		[Fact]
		public void Visit_WithSqlAssignCollection_VisitsAllItems()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();
			var visitor = mock.Object;

			visitor.Visit(Enumerable.Range(0, 3).Select(i => new SqlAssign("Column" + i, i)));
			mock.Verify(_ => _.Visit(It.IsAny<IEnumerable<SqlAssign>>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlAssign>()), Times.Exactly(3));
		}
	}
}