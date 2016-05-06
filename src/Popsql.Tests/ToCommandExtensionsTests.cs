using System;
using System.Data;
using Moq;
using Popsql.Grammar;
using Popsql.Tests.Mocking;
using Xunit;

namespace Popsql.Tests
{
	public class ToCommandExtensionsTests
	{
		[Fact]
		public void ToCommand_WithNullSqlGo_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var connection = CreateConnection();
				((ISqlGo<SqlSelect>) null).ToCommand(connection);
			});
		}

		[Fact]
		public void ToCommand_WithNullSqlSelect_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var connection = CreateConnection();
				((SqlSelect)null).ToCommand(connection);
			});
		}

		[Fact]
		public void ToCommand_WithNullSqlUpdate_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var connection = CreateConnection();
				((SqlUpdate)null).ToCommand(connection);
			});
		}

		[Fact]
		public void ToCommand_WithNullSqlInsert_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var connection = CreateConnection();
				((SqlInsert)null).ToCommand(connection);
			});
		}

		[Fact]
		public void ToCommand_WithNullSqlDelete_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var connection = CreateConnection();
				((SqlDelete)null).ToCommand(connection);
			});
		}

		[Fact]
		public void ToCommand_WithNullSqlUnion_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var connection = CreateConnection();
				((SqlUnion)null).ToCommand(connection);
			});
		}

		[Fact]
		public void ToCommand_WithSqlGoWithNullConnection_ThrowsArgumentNull()
		{
			var query = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", 42));
			Assert.Throws<ArgumentNullException>(() => query.ToCommand(null));
		}

		[Fact]
		public void ToCommand_WithSqlGo_SetsCommandText()
		{
			var connection = CreateConnection();
			
			var query = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", 42));
			var command = query.ToCommand(connection);

			const string expected = "SELECT [Id], [Name], [Email] FROM [Users] WHERE [Id] = 42";
			Assert.Equal(expected, command.CommandText);
		}

		[Fact]
		public void ToCommand_WithSqlGo_AddsParameters()
		{
			var connection = CreateConnection();

			var query = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", "Id" + (SqlConstant)42));
			var command = query.ToCommand(connection);

			const string expected = "SELECT [Id], [Name], [Email] FROM [Users] WHERE [Id] = @Id";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Id", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(42, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void ToCommand_WithSqlSelectWithNullConnection_ThrowsArgumentNull()
		{
			var query = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", 42))
				.Go();
			Assert.Throws<ArgumentNullException>(() => query.ToCommand(null));
		}

		[Fact]
		public void ToCommand_WithSqlSelect_SetsCommandText()
		{
			var connection = CreateConnection();

			var query = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", 42))
				.Go();
			var command = query.ToCommand(connection);

			const string expected = "SELECT [Id], [Name], [Email] FROM [Users] WHERE [Id] = 42";
			Assert.Equal(expected, command.CommandText);
		}

		[Fact]
		public void ToCommand_WithSqlSelect_AddsParameters()
		{
			var connection = CreateConnection();

			var query = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", "Id" + (SqlConstant)42))
				.Go();
			var command = query.ToCommand(connection);

			const string expected = "SELECT [Id], [Name], [Email] FROM [Users] WHERE [Id] = @Id";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Id", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(42, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void ToCommand_WithSqlUpdateWithNullConnection_ThrowsArgumentNull()
		{
			var query = Sql
				.Update("Users")
				.Set("FirstName", "John")
				.Set("LastName", "Foobar")
				.Where(SqlExpression.Equal("Id", 42))
				.Go();
			Assert.Throws<ArgumentNullException>(() => query.ToCommand(null));
		}

		[Fact]
		public void ToCommand_WithSqlUpdate_SetsCommandText()
		{
			var connection = CreateConnection();

			var query = Sql
				.Update("Users")
				.Set("FirstName", "John")
				.Set("LastName", "Foobar")
				.Where(SqlExpression.Equal("Id", 42))
				.Go();
			var command = query.ToCommand(connection);

			const string expected = "UPDATE [Users] SET [FirstName] = 'John', [LastName] = 'Foobar' WHERE [Id] = 42";
			Assert.Equal(expected, command.CommandText);
		}

		[Fact]
		public void ToCommand_WithSqlUpdate_AddsParameters()
		{
			var connection = CreateConnection();

			var query = Sql
				.Update("Users")
				.Set("FirstName", "John")
				.Set("LastName", "Foobar")
				.Where(SqlExpression.Equal("Id", "Id" + (SqlConstant)42))
				.Go();
			var command = query.ToCommand(connection);
			
			const string expected = "UPDATE [Users] SET [FirstName] = 'John', [LastName] = 'Foobar' WHERE [Id] = @Id";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Id", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(42, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void ToCommand_WithSqlInsertWithNullConnection_ThrowsArgumentNull()
		{
			var query = Sql
				.Insert()
				.Into("Users", "FirstName", "LastName")
				.Values("John", "Foobar")
				.Go();
			Assert.Throws<ArgumentNullException>(() => query.ToCommand(null));
		}

		[Fact]
		public void ToCommand_WithSqlInsert_SetsCommandText()
		{
			var connection = CreateConnection();

			var query = Sql
				.Insert()
				.Into("Users", "FirstName", "LastName")
				.Values("John", "Foobar")
				.Go();
			var command = query.ToCommand(connection);

			const string expected = "INSERT INTO [Users] ([FirstName], [LastName]) VALUES ('John', 'Foobar')";
			Assert.Equal(expected, command.CommandText);
		}

		[Fact]
		public void ToCommand_WithSqlInsert_AddsParameters()
		{
			var connection = CreateConnection();

			var query = Sql
				.Insert()
				.Into("Users", "FirstName", "LastName")
				.Values("FirstName" + (SqlConstant)"John", "LastName" + (SqlConstant)"Foobar")
				.Go();
			var command = query.ToCommand(connection);

			const string expected = "INSERT INTO [Users] ([FirstName], [LastName]) VALUES (@FirstName, @LastName)";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(2, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("FirstName", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal("John", ((IDbDataParameter)command.Parameters[0]).Value);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[1]);
			Assert.Equal("LastName", ((IDbDataParameter)command.Parameters[1]).ParameterName);
			Assert.Equal("Foobar", ((IDbDataParameter)command.Parameters[1]).Value);
		}

		[Fact]
		public void ToCommand_WithSqlDeleteWithNullConnection_ThrowsArgumentNull()
		{
			var query = Sql
				.Delete()
				.From("Users")
				.Where(SqlExpression.GreaterThanOrEqual("Age", 30))
				.Go();
			Assert.Throws<ArgumentNullException>(() => query.ToCommand(null));
		}

		[Fact]
		public void ToCommand_WithSqlDelete_SetsCommandText()
		{
			var connection = CreateConnection();

			var query = Sql
				.Delete()
				.From("Users")
				.Where(SqlExpression.GreaterThanOrEqual("Age", 30))
				.Go();
			var command = query.ToCommand(connection);

			const string expected = "DELETE FROM [Users] WHERE [Age] >= 30";
			Assert.Equal(expected, command.CommandText);
		}

		[Fact]
		public void ToCommand_WithSqlDelete_AddsParameters()
		{
			var connection = CreateConnection();

			var query = Sql
				.Delete()
				.From("Users")
				.Where(SqlExpression.GreaterThanOrEqual("Age", "Age" + (SqlConstant)30))
				.Go();
			var command = query.ToCommand(connection);

			const string expected = "DELETE FROM [Users] WHERE [Age] >= @Age";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Age", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(30, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void ToCommand_WithSqlUnionWithNullConnection_ThrowsArgumentNull()
		{
			var query = Sql
				.Union(
					Sql.Select("City").From("Suppliers").Where(SqlExpression.LessThanOrEqual("Distance", 42)).Go(),
					Sql.Select("City").From("Customers").Where(SqlExpression.LessThanOrEqual("Distance", 42)).Go()
				);

			Assert.Throws<ArgumentNullException>(() => query.ToCommand(null));
		}

		[Fact]
		public void ToCommand_WithSqlUnion_SetsCommandText()
		{
			var connection = CreateConnection();

			var query = Sql
				.Union(
					Sql.Select("City").From("Suppliers").Where(SqlExpression.LessThanOrEqual("Distance", 42)).Go(),
					Sql.Select("City").From("Customers").Where(SqlExpression.LessThanOrEqual("Distance", 42)).Go()
				);
			var command = query.ToCommand(connection);

			const string expected = "(SELECT [City] FROM [Suppliers] WHERE [Distance] <= 42) UNION (SELECT [City] FROM [Customers] WHERE [Distance] <= 42)";
			Assert.Equal(expected, command.CommandText);
		}

		[Fact]
		public void ToCommand_WithSqlUnion_AddsParameters()
		{
			var connection = CreateConnection();

			var parameter = "Distance" + (SqlConstant)42;
			var query = Sql
				.Union(
					Sql.Select("City").From("Suppliers").Where(SqlExpression.LessThanOrEqual("Distance", parameter)).Go(),
					Sql.Select("City").From("Customers").Where(SqlExpression.LessThanOrEqual("Distance", parameter)).Go()
				);
			var command = query.ToCommand(connection);

			const string expected = "(SELECT [City] FROM [Suppliers] WHERE [Distance] <= @Distance) UNION (SELECT [City] FROM [Customers] WHERE [Distance] <= @Distance)";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Distance", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(42, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void ToCommand_WithSqlUnionWithConflictingParameters_ThrowsInvalidOperation()
		{
			var connection = CreateConnection();

			var parameter1 = "Distance" + (SqlConstant)42;
			var parameter2 = "Distance" + (SqlConstant)30;
			var query = Sql
				.Union(
					Sql.Select("City").From("Suppliers").Where(SqlExpression.LessThanOrEqual("Distance", parameter1)).Go(),
					Sql.Select("City").From("Customers").Where(SqlExpression.LessThanOrEqual("Distance", parameter2)).Go()
				);
			Assert.Throws<InvalidOperationException>(() => query.ToCommand(connection));
		}

		[Fact]
		public void CreateCommand_WithSqlGoWithNullConnection_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IDbConnection)null).CreateCommand((ISqlGo<SqlSelect>)null));
		}

		[Fact]
		public void CreateCommand_WithSqlSelectWithNullConnection_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IDbConnection) null).CreateCommand((SqlSelect) null));
		}

		[Fact]
		public void CreateCommand_WithSqlInsertWithNullConnection_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IDbConnection)null).CreateCommand((SqlInsert)null));
		}

		[Fact]
		public void CreateCommand_WithSqlUpdateWithNullConnection_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IDbConnection)null).CreateCommand((SqlUpdate)null));
		}

		[Fact]
		public void CreateCommand_WithSqlDeleteWithNullConnection_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IDbConnection)null).CreateCommand((SqlDelete)null));
		}

		[Fact]
		public void CreateCommand_WithSqlUnionWithNullConnection_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IDbConnection)null).CreateCommand((SqlUnion)null));
		}

		[Fact]
		public void CreateCommand_WithNullSqlGo_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => (CreateConnection()).CreateCommand((ISqlGo<SqlSelect>)null));
		}

		[Fact]
		public void CreateCommand_WithNullSqlSelect_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => (CreateConnection()).CreateCommand((SqlSelect)null));
		}

		[Fact]
		public void CreateCommand_WithNullSqlInsert_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => (CreateConnection()).CreateCommand((SqlInsert)null));
		}

		[Fact]
		public void CreateCommand_WithNullSqlUpdate_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => (CreateConnection()).CreateCommand((SqlUpdate)null));
		}

		[Fact]
		public void CreateCommand_WithNullSqlDelete_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => (CreateConnection()).CreateCommand((SqlDelete)null));
		}

		[Fact]
		public void CreateCommand_WithNullSqlUnion_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => (CreateConnection()).CreateCommand((SqlUnion)null));
		}

		[Fact]
		public void CreateCommand_WithSqlGo_ReturnsCommand()
		{
			var connection = CreateConnection();

			var query = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", "Id" + (SqlConstant)42));
			var command = connection.CreateCommand(query);

			const string expected = "SELECT [Id], [Name], [Email] FROM [Users] WHERE [Id] = @Id";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Id", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(42, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void CreateCommand_WithSqlSelect_ReturnsCommand()
		{
			var connection = CreateConnection();

			var query = Sql
				.Select("Id", "Name", "Email")
				.From("Users")
				.Where(SqlExpression.Equal("Id", "Id" + (SqlConstant)42))
				.Go();
			var command = connection.CreateCommand(query);

			const string expected = "SELECT [Id], [Name], [Email] FROM [Users] WHERE [Id] = @Id";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Id", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(42, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void CreateCommand_WithSqlUpdate_ReturnsCommand()
		{
			var connection = CreateConnection();

			var query = Sql
				.Update("Users")
				.Set("FirstName", "John")
				.Set("LastName", "Foobar")
				.Where(SqlExpression.Equal("Id", "Id" + (SqlConstant)42))
				.Go();
			var command = connection.CreateCommand(query);

			const string expected = "UPDATE [Users] SET [FirstName] = 'John', [LastName] = 'Foobar' WHERE [Id] = @Id";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Id", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(42, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void CreateCommand_WithSqlInsert_ReturnsCommand()
		{
			var connection = CreateConnection();

			var query = Sql
				.Insert()
				.Into("Users", "FirstName", "LastName")
				.Values("FirstName" + (SqlConstant)"John", "LastName" + (SqlConstant)"Foobar")
				.Go();
			var command = connection.CreateCommand(query);

			const string expected = "INSERT INTO [Users] ([FirstName], [LastName]) VALUES (@FirstName, @LastName)";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(2, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("FirstName", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal("John", ((IDbDataParameter)command.Parameters[0]).Value);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[1]);
			Assert.Equal("LastName", ((IDbDataParameter)command.Parameters[1]).ParameterName);
			Assert.Equal("Foobar", ((IDbDataParameter)command.Parameters[1]).Value);
		}

		[Fact]
		public void CreateCommand_WithSqlDelete_ReturnsCommand()
		{
			var connection = CreateConnection();

			var query = Sql
				.Delete()
				.From("Users")
				.Where(SqlExpression.GreaterThanOrEqual("Age", "Age" + (SqlConstant)30))
				.Go();
			var command = connection.CreateCommand(query);

			const string expected = "DELETE FROM [Users] WHERE [Age] >= @Age";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Age", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(30, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		[Fact]
		public void CreateCommand_WithSqlUnion_ReturnsCommand()
		{
			var connection = CreateConnection();

			var parameter = "Distance" + (SqlConstant)42;
			var query = Sql
				.Union(
					Sql.Select("City").From("Suppliers").Where(SqlExpression.LessThanOrEqual("Distance", parameter)).Go(),
					Sql.Select("City").From("Customers").Where(SqlExpression.LessThanOrEqual("Distance", parameter)).Go()
				);
			var command = connection.CreateCommand(query);

			const string expected = "(SELECT [City] FROM [Suppliers] WHERE [Distance] <= @Distance) UNION (SELECT [City] FROM [Customers] WHERE [Distance] <= @Distance)";
			Assert.Equal(expected, command.CommandText);
			Assert.Equal(1, command.Parameters.Count);
			Assert.IsAssignableFrom<IDbDataParameter>(command.Parameters[0]);
			Assert.Equal("Distance", ((IDbDataParameter)command.Parameters[0]).ParameterName);
			Assert.Equal(42, ((IDbDataParameter)command.Parameters[0]).Value);
		}

		private static IDbConnection CreateConnection()
		{
			var repository = new MockRepository(MockBehavior.Default);
			return repository.CreateIDbConnection().Object;
		}
	}
}