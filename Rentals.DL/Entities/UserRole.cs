using Microsoft.AspNetCore.Identity;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Vázací tabulka mezi uživateli a rolemi (je zde kvůli navigation properties).
	/// </summary>
	public class UserRole : IdentityUserRole<int>
	{
		public virtual User User
		{
			get;
			set;
		}

		public virtual Role Role
		{
			get;
			set;
		}
	}
}
