using System.ComponentModel.DataAnnotations;

namespace Rentals.Common.Enums
{
	public enum RoleType
	{
		/// <summary>
		/// Zákazník.
		/// </summary>
		[Display(Name = nameof(Localization.Resources.RoleType_Customer), ResourceType = typeof(Localization.Resources))]
		Customer = 1,

		/// <summary>
		/// Zaměstanenec.
		/// </summary>
		[Display(Name = nameof(Localization.Resources.RoleType_Employee), ResourceType = typeof(Localization.Resources))]
		Employee = 10,

		/// <summary>
		/// Správce půjčovny.
		/// </summary>
		[Display(Name = nameof(Localization.Resources.RoleType_Administrator), ResourceType = typeof(Localization.Resources))]
		Administrator = 100,
	}
}
