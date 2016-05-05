using System;
using Xunit;

namespace Popsql.Tests
{
	public class OwnedByTests
	{
		private class TestOwnedBy : OwnedBy<TestOwnedBy>
		{
			public TestOwnedBy(TestOwnedBy parent) 
				: base(parent)
			{
			}
		}

		[Fact]
		public void Ctor_WithNullParent_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => new TestOwnedBy(null));
		}
	}
}