using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Popsql.Visitors;
using Xunit;

namespace Popsql.Tests
{
	public class SqlDeleteTests
	{
		[Fact]
		public void Ctor_DoesNotThrow()
		{
			new SqlDelete();
		}

		[Fact]
		public void From_WithNullTable_ThrowsArgumentNull()
		{
			var delete = Sql.Delete();
			Assert.Throws<ArgumentNullException>(() => delete.From(null));
		}


		[Fact]
		public void From_WithTable_SetsFromProperty()
		{
			var delete = Sql
				.Delete()
				.From("Users")
				.Go();

			Assert.NotNull(delete.From);
			Assert.Equal("Users", ((SqlTable)delete.From.Table).TableName.Segments.First());
		}

		[Fact]
		public void Where_WithExpression_SetsWhereProperty()
		{
			var delete = Sql
				.Delete()
				.From("User")
				.Where(SqlExpression.Equal("Id", 5))
				.Go();

			Assert.NotNull(delete.Where);
			Assert.NotNull(delete.Where.Predicate);
			Assert.IsType<SqlBinaryExpression>(delete.Where.Predicate);
			Assert.IsType<SqlColumn>(((SqlBinaryExpression)delete.Where.Predicate).Left);
			Assert.Equal("Id", ((SqlColumn)((SqlBinaryExpression)delete.Where.Predicate).Left).ColumnName.Segments.First());

			Assert.Equal(SqlBinaryOperator.Equal, ((SqlBinaryExpression)delete.Where.Predicate).Operator);

			Assert.IsType<SqlConstant>(((SqlBinaryExpression)delete.Where.Predicate).Right);
			Assert.Equal(5, ((SqlConstant)((SqlBinaryExpression)delete.Where.Predicate).Right).Value);
		}
		
		[Fact]
		public void ExpressionType_ReturnsDelete()
		{
			var query = new SqlDelete();

			Assert.Equal(SqlExpressionType.Delete, query.ExpressionType);
		}

		[Fact]
		public void Accept_WithoutFrom_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = new SqlDelete();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlDelete>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlFrom>()), Times.Never);
			mock.Verify(_ => _.Visit(It.IsAny<SqlWhere>()), Times.Never);
		}

		[Fact]
		public void Accept_WithoutWhere_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = Sql
				.Delete()
				.From("User")
				.Go();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlDelete>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlFrom>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlTable>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlWhere>()), Times.Never);
		}

		[Fact]
		public void Accept_WithWhere_VisitsEverything()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var mock = fixture.Freeze<Mock<SqlVisitor>>();

			var query = Sql
				.Delete()
				.From("User")
				.Where(SqlExpression.Equal("Id", 5))
				.Go();

			query.Accept(mock.Object);

			mock.Verify(_ => _.Visit(It.IsAny<SqlDelete>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlFrom>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlTable>()), Times.Once);
			mock.Verify(_ => _.Visit(It.IsAny<SqlWhere>()), Times.Once);
		}
	}
}