using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Xunit;
using Xunit.Sdk;

namespace Popsql.Tests
{
	public class EnumerableExtensionsTests
	{
		[Fact]
		public void ToReadOnly_WithNullSource_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((int[]) null).ToReadOnly());
		}

		[Fact]
		public void ToReadOnly_ReturnsReadOnlyCollection()
		{
			var expected = Enumerable.Range(0, 5000);
			var acutal = expected.ToReadOnly();

			Assert.Equal(expected, acutal);
		}

		[Fact]
		public void IfAny_WithNullSource_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((int[]) null).IfAny(_ => { }));
		}

		[Fact]
		public void IfAny_WithNullAction_DoesNotThrow()
		{
			Enumerable.Range(0, 10).IfAny(null);
		}

		[Fact]
		public void IfAny_WithAction_IsPassThrough()
		{
			var expected = Enumerable.Range(0, 10);
			expected.IfAny(actual => Assert.Same(expected, actual));
		}

		[Fact]
		public void For_WithNullSource_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((int[]) null).For((index, i) => { }));
		}

		[Fact]
		public void For_WithNullAction_DoesNotThrow()
		{
			Enumerable.Range(0, 10).For(null);
		}

		[Fact]
		public void For_IteratesAllItems()
		{
			var expected = Enumerable.Range(0, 5000);
			var expectedIndexes = Enumerable.Range(0, 5000);
			var actual = new List<int>(5000);
			var actualIndexes = new List<int>(5000);
			expected.For((index, item) => { actual.Add(item); actualIndexes.Add(index); });

			Assert.Equal(expected, actual);
			Assert.Equal(expectedIndexes, actualIndexes);
		}

		[Fact]
		public void ForEach_WithNullSource_ThrowsArgumentNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((int[])null).ForEach(i => { }));
		}

		[Fact]
		public void ForEach_WithNullAction_DoesNotThrow()
		{
			Enumerable.Range(0, 10).ForEach(null);
		}

		[Fact]
		public void ForEach_IteratesAllItems()
		{
			var expected = Enumerable.Range(0, 5000);
			var actual = new List<int>(5000);
			expected.ForEach(item => actual.Add(item));

			Assert.Equal(expected, actual);
		}
	}
}