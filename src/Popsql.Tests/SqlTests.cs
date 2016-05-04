using Xunit;

namespace Popsql.Tests
{
	public class SqlTests
	{
		[Fact]
		public void Select_WithRealLifeQuery_ReturnsQuery()
		{
			SqlTable users = new SqlTable("User", "u");
			SqlTable profiles = new SqlTable("Profile", "p");
			var actual = Sql
				.Select(users + "Id", users + "Name", users + "Email", profiles + "Avatar", profiles + "Birthday")
				.From(users)
				.LeftJoin(profiles, SqlExpression.Equal(users + "Id", profiles + "UserId"))
				.Where(SqlExpression.Equal(profiles + "Age", 18))
				.OrderBy(users + "Name")
				.ToSql();
			
			const string expected = "SELECT [u].[Id], [u].[Name], [u].[Email], [p].[Avatar], [p].[Birthday] FROM [User] [u] LEFT JOIN [Profile] [p] ON [u].[Id] = [p].[UserId] WHERE [p].[Age] = 18 ORDER BY [u].[Name]";
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Delete_WithRealLifeQuery_ReturnsQuery()
		{
			SqlTable users = "User";
			var actual = Sql
				.Delete()
				.From(users)
				.Where(SqlExpression.Equal(users + "Id", 5))
				.ToSql();
			
			const string expected = "DELETE FROM [User] WHERE [User].[Id] = 5";
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Insert_WithRealLifeQuery_ReturnsQuery()
		{
			SqlTable users = "User";
			var actual = Sql
				.Insert()
				.Into(users, users + "Name", users + "Email")
				.Values("John Doe", "john.doe@ac.edu")
				.ToSql();
			
			const string expected = "INSERT INTO [User] ([User].[Name], [User].[Email]) VALUES ('John Doe', 'john.doe@ac.edu')";
			Assert.Equal(
				expected,
				actual);
		}

		[Fact]
		public void Update_WithRealLifeQuery_ReturnsQuery()
		{
			SqlTable users = "User";
			var actual = Sql
				.Update(users)
				.Set(users + "Name", "John Doe")
				.Set(users + "Email", "john.doe@ac.edu")
				.Where(SqlExpression.Equal(users + "Id", 5))
				.ToSql();

			const string expected = "UPDATE [User] SET [User].[Name] = 'John Doe', [User].[Email] = 'john.doe@ac.edu' WHERE [User].[Id] = 5";
			Assert.Equal(expected, actual);
		}
	}
}