using System;
using System.Collections.Generic;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Popsql.Visitors;
using Xunit;

namespace Popsql.Tests.Visitors
{
	public class SqlVisitorExtensionsTests
	{
		[Fact]
		public void AcceptSqlColumn_WithNullSource_ThrowsArgumentNull()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var visitor = fixture.Freeze<ISqlVisitor>();
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<SqlColumn>)null).Accept(visitor));
		}

		[Fact]
		public void AcceptSqlColumn_WithNullVisitor_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new[] { new SqlColumn("Id") }.Accept(null));
		}

		[Fact]
		public void AcceptSqlValues_WithNullSource_ThrowsArgumentNull()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var visitor = fixture.Freeze<ISqlVisitor>();
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<IEnumerable<SqlValue>>)null).Accept(visitor));
		}

		[Fact]
		public void AcceptSqlValues_WithNullVisitor_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new[] { new SqlValue[] { new SqlConstant(5) } }.Accept(null));
		}

		[Fact]
		public void AcceptSqlValue_WithNullSource_ThrowsArgumentNull()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var visitor = fixture.Freeze<ISqlVisitor>();
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<SqlValue>)null).Accept(visitor));
		}

		[Fact]
		public void AcceptSqlValue_WithNullVisitor_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new[] { new SqlConstant(42) }.Accept(null));
		}

		[Fact]
		public void AcceptSqlSort_WithNullSource_ThrowsArgumentNull()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var visitor = fixture.Freeze<ISqlVisitor>();
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<SqlSort>)null).Accept(visitor));
		}

		[Fact]
		public void AcceptSqlSort_WithNullVisitor_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new[] { new SqlSort("Id", SqlSortOrder.Ascending) }.Accept(null));
		}

		[Fact]
		public void AcceptSqlAssign_WithNullSource_ThrowsArgumentNull()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			var visitor = fixture.Freeze<ISqlVisitor>();
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<SqlAssign>)null).Accept(visitor));
		}

		[Fact]
		public void AcceptSqlAssign_WithNullVisitor_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new[] { new SqlAssign("Id", 42) }.Accept(null));
		}

		[Fact]
		public void AcceptSqlBinaryOperator_WithNullVisitor_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlBinaryOperator.And.Accept(null));
		}

		[Fact]
		public void AcceptSqlSortOrder_WithNullVisitor_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => SqlSortOrder.Ascending.Accept(null));
		}
	}
}