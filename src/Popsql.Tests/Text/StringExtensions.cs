namespace Popsql.Tests.Text
{
	internal static class StringExtensions
	{
		public static string ToLowerIf(this string s, bool condition)
		{
			return condition ? s.ToLower() : s;
		}
	}
}