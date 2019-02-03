using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals.DL.Entities
{
	public partial class AdminInvite
	{
		/// <summary>
		/// Vrací, zda-li je pozvánka stále platná.
		/// </summary>
		[NotMapped]
		public bool IsValid
		{
			get
			{
				var result = this.ExpiresAt > DateTime.Now && !IsRedeemed;

				return result;
			}
		}

		public static AdminInvite CreateEntity(string forUser, DateTime expiresAt, bool willBeAdmin, bool willBeEmployee)
		{
			var invite = new AdminInvite()
			{
				IsRedeemed = false,
				ForUser = forUser,
				ExpiresAt = expiresAt,
				WillBeAdmin = willBeAdmin,
				WillBeEmployee = willBeEmployee
			};

			return invite;
		}
	}
}
