namespace Rentals.Common.Enums
{
	public enum EmailType
	{
		/// <summary>
		/// Nová výpůjčka
		/// </summary>
		RentingNew,

		/// <summary>
		/// Výpůjčka byla editována
		/// </summary>
		RentingEdit,

		/// <summary>
		/// Výpůjčka byla zrušena
		/// </summary>
		RentingCalcelation,

		/// <summary>
		/// Upozornění na končící výpůjčku
		/// </summary>
		RentingEndsReminder,

		/// <summary>
		/// Upozornění na nevrácenou výpůjčku
		/// </summary>
		RentingNotReturned,
	}
}
