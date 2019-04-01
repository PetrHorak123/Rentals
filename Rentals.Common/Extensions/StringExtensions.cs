using System;
using System.Security.Cryptography;

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

		public static string GetRandomString(int length)
		{

			var rng = new RNGCryptoServiceProvider();
			byte[] randomBytes = new byte[length];

			// vygenerování náhodného textu
			rng.GetBytes(randomBytes);

			// převedení na text
			string randomText = Convert.ToBase64String(randomBytes);

			return randomText;
		}
	}
}
