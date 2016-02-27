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
	}
}