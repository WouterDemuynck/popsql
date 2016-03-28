using Xunit;

namespace Popsql.Tests
{
	public class SqlTests
	{
		public void Select_WithRealLifeQuery_ReturnsQuery()
		{
			SqlTable users = "User";
			SqlTable profiles = "Profile";
			var query = Sql
				.Select(users + "Id", users + "Name", users + "Email", profiles + "Avatar", profiles + "Birthday")
				.From(users)
				.LeftJoin(profiles, SqlExpression.Equal(users + "Id", profiles + "UserId"))
				.Where(SqlExpression.Equal(profiles + "Age", 18))
				.OrderBy(users + "Name")
				.Go();
		}

		public void Delete_WithRealLifeQuery_ReturnsQuery()
		{
			SqlTable users = "User";

			var query = Sql
				.Delete()
				.From(users)
				.Where(SqlExpression.Equal(users + "Id", 5))
				.Go();
		}

		public void Insert_WithRealLifeQuery_ReturnsQuery()
		{
			SqlTable users = "User";

			var query = Sql
				.Insert()
				.Into(users, users + "Name", users + "Email")
				.Values("John Doe", "john.doe@ac.edu")
				.Go();
		}
		public void Update_WithRealLifeQuery_ReturnsQuery()
		{
			SqlTable users = "User";

			var query = Sql
				.Update(users)
				.Set(users + "Name", "John Doe")
				.Set(users + "Email", "john.doe@ac.edu")
				.Where(SqlExpression.Equal(users + "Id", 5))
				.Go();
		}
	}
}