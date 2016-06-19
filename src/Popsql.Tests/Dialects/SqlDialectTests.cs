using System.Text;
using Moq;
using Popsql.Dialects;
using Popsql.Tests.Mocking;
using Xunit;

namespace Popsql.Tests.Dialects
{
	public class SqlDialectTests
	{
		[Fact]
		public void Current_WithNullValue_SetsToDefault()
		{
			SqlDialect.Current = null;
			Assert.Same(SqlDialect.Default, SqlDialect.Current);
		}

		[Fact]
		public void Current_WithValue_SetsCurrentDialect()
		{
			var dialect = new Mock<SqlDialect>().Object;
			SqlDialect.Current = dialect;
			Assert.Same(dialect, SqlDialect.Current);

			SqlDialect.Current = SqlDialect.Default;
		}

		[Fact]
		public void FormatIdentifier_ReturnsFormattedTableName()
		{
			const string expected = "[Users]";
			var dialect = new SqlDialect();

			var actual = dialect.FormatIdentifier("Users");

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatParameterName_ReturnsFormattedParameterName()
		{
			const string expected = "@Parameter";
			var dialect = new SqlDialect();

			var actual = dialect.FormatParameterName("Parameter");

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void FormatString_EscapesApostrophe()
		{
			const string expected = "'This isn''t a test.'";
			var dialect = new SqlDialect();

			var actual = dialect.FormatString("This isn't a test.");
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void WriteFetchFirst_WritesOffsetFetch()
		{
			var expected = "OFFSET 42 ROWS FETCH FIRST 10 ROWS ONLY";
			var dialect = new SqlServerDialect();
			var builder = new StringBuilder();

			using (TestSqlWriter writer = new TestSqlWriter(builder, dialect))
			{
				dialect.WriteFetchFirst(writer, 42, 10);
			}

			var actual = builder.ToString();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void WriteFetchFirst_WithNullOffset_WritesNoOffset()
		{
			var expected = "FETCH FIRST 10 ROWS ONLY";
			var dialect = new SqlDialect();
			var builder = new StringBuilder();

			using (TestSqlWriter writer = new TestSqlWriter(builder, dialect))
			{
				dialect.WriteFetchFirst(writer, null, 10);
			}

			var actual = builder.ToString();
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void WriteFetchFirst_WithNullCount_WritesOffsetOnly()
		{
			var expected = "OFFSET 42 ROWS";
			var dialect = new SqlServerDialect();
			var builder = new StringBuilder();

			using (TestSqlWriter writer = new TestSqlWriter(builder, dialect))
			{
				dialect.WriteFetchFirst(writer, 42, null);
			}

			var actual = builder.ToString();
			Assert.Equal(expected, actual);
		}
	}
}
