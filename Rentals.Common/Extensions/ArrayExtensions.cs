using System;

namespace Rentals.Common.Extensions
{
	public static class ArrayExtensions
	{
		public static T[] Add<T>(this T[] array, T item)
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array));
			}

			T[] result = new T[array.Length + 1];
			array.CopyTo(result, 0);
			result[array.Length] = item;

			return result;
		}
	}
}
