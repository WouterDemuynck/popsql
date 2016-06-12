using System;
using System.Collections;
using System.Data;
using System.Linq;
using Moq;

namespace Popsql.Tests.Mocking
{
	public static class MockRepositoryExtensions
	{
		public static Mock<IDbConnection> CreateIDbConnection(this MockRepository repository)
		{
			var connection = repository.Create<IDbConnection>();

			connection.SetupAllProperties();
			connection.Setup(c => c.CreateCommand()).Returns(() => repository.CreateIDbCommand().Object);

			return connection;
		}

		public static Mock<IDbCommand> CreateIDbCommand(this MockRepository repository)
		{
			var command = repository.Create<IDbCommand>();

			command.SetupAllProperties();
			command.Setup(c => c.CreateParameter()).Returns(() => repository.CreateIDbDataParameter().Object);
			command.Setup(c => c.Parameters).Returns(repository.CreateIDataParameterCollection().Object);

			return command;
		}

		public static Mock<IDataParameterCollection> CreateIDataParameterCollection(this MockRepository repository)
		{
			var list = new ArrayList(); // ArrayList more closely matches IDataParameterCollection.
			var parameters = repository.Create<IDataParameterCollection>();

			parameters.Setup(p => p.Add(It.IsAny<IDataParameter>())).Returns((IDataParameter p) => list.Add(p));
			parameters.Setup(p => p.Contains(It.IsAny<string>())).Returns((string n) => list.OfType<IDataParameter>().Any(p => p.ParameterName == n));
			parameters.Setup(p => p[It.IsAny<int>()]).Returns((int i) => list[i]);
			parameters.Setup(p => p[It.IsAny<string>()]).Returns((string n) => list.OfType<IDataParameter>().FirstOrDefault(p => p.ParameterName == n));
			parameters.Setup(p => p.Count).Returns(() => list.Count);

			return parameters;
		}

		public static Mock<IDbDataParameter> CreateIDbDataParameter(this MockRepository repository)
		{
			var parameter = repository.Create<IDbDataParameter>();
			parameter.SetupAllProperties();

			return parameter;
		}

		public static Mock<IDataRecord> CreateIDataRecord(this MockRepository repository, params object[] fields)
		{
			var record = repository.Create<IDataRecord>();

			for (var index = 0; index < fields.Length; index++)
			{
				var column = fields[index];
				var type = column.GetType();
				var name = (string)type.GetProperty("Name").GetValue(column, null);
				var value = type.GetProperty("Value").GetValue(column, null);

				record.Setup(r => r.IsDBNull(index)).Returns(value == DBNull.Value);
				record.Setup(r => r.GetOrdinal(name)).Returns(index);
				record.Setup(r => r[index]).Returns(value);
			}

			return record;
		}
	}
}