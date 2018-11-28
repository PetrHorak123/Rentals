using Microsoft.AspNetCore.Identity;
using Rentals.Common.Enums;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Entities
{
	public class Role : IdentityRole<int>, IEntity
	{
		public Role()
		{
		}

		public Role(RoleType type, string stringType) : base(stringType)
		{
			this.RoleType = type;
		}

		/// <summary>
		/// Typ role (abych se nespoléhal na texty ve stringu)
		/// </summary>
		public RoleType RoleType
		{
			get;
			set;
		}
	}
}
