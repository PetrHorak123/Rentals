namespace Rentals.Common.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullOrEmpty(this string s)
		{
			bool result = (s == null || s == string.Empty);

			return result;
		}

		public static string RemoveSpaces(this string s)
		{
			return s.Replace(" ", string.Empty);
		}
	}
}
