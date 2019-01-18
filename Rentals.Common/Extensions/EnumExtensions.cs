using Rentals.Common.Localization;
using System;

namespace Rentals.Common.Extensions
{
	/// <summary>
	/// Extension metody pro enumy.
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Vrací lokalizované enum.
		/// </summary>
		public static string ToLocalizedEnum(this Enum enumValue, Type type)
		{
			return Resources.ResourceManager.GetString(type.Name + "_" + enumValue.ToString());
		}
	}
}
