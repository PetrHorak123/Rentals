using System.ComponentModel.DataAnnotations;

namespace Rentals.Common.Enums
{
	public enum RentalState
	{
		/// <summary>
		/// Výpůjčka byla zrušena.
		/// </summary>
		[Display(Name = nameof(Localization.Resources.RentalState_Canceled), ResourceType = typeof(Localization.Resources))]
		Canceled = 0,

		/// <summary>
		/// Nezapůjčeno - předmět ještě nebyl vyzvednut.
		/// </summary>
		[Display(Name = nameof(Localization.Resources.RentalState_NotLended), ResourceType = typeof(Localization.Resources))]
		NotLended = 10,

		/// <summary>
		/// Vypůjčeno - předmět byl vyzvendnut a nachází se u zákazníka.
		/// </summary>
		[Display(Name = nameof(Localization.Resources.RentalState_Lended), ResourceType = typeof(Localization.Resources))]
		Lended = 100,

		/// <summary>
		/// Vráceno - předmět byl po zápůjčce vrácen.
		/// </summary>
		[Display(Name = nameof(Localization.Resources.RentalState_Returned), ResourceType = typeof(Localization.Resources))]
		Returned = 200,
	}
}
