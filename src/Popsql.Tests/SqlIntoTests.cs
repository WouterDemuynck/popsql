using System;
using Xunit;

namespace Popsql.Tests
{
	public class SqlIntoTests
	{
		[Fact]
		public void Ctor_WithNullTable_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new SqlInto(null));
		} 
	}
}