using Popsql.Text;
using Xunit;

namespace Popsql.Tests.Text
{
	public class SqlWriterSettingsTests
	{
		[Fact]
		public void WriteKeywordsInLowerCase_DefaultValueIsFalse()
		{
			var settings = new SqlWriterSettings();
			Assert.False(settings.WriteKeywordsInLowerCase);
		}

		[Fact]
		public void WriteKeywordsInLowerCase_ValueCanBeSet()
		{
			var settings = new SqlWriterSettings
			{
				WriteKeywordsInLowerCase = true
			};
			Assert.True(settings.WriteKeywordsInLowerCase);
		}
	}
}
