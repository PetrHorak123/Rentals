using Rentals.Common.Enums;
using Rentals.DL.Entities;
using System;
using System.Collections.Generic;

namespace Rentals.Web.Areas.Admin.Models
{
	public class AdminLinkViewModel
	{
		public AdminLinkViewModel(AdminInvite invite)
		{
			this.ForUser = invite.ForUser;
			this.ExpiresAt = invite.ExpiresAt;
			this.ForRoles = new List<RoleType>();

			if (invite.WillBeAdmin)
				this.ForRoles.Add(RoleType.Administrator);

			if (invite.WillBeEmployee)
				this.ForRoles.Add(RoleType.Employee);
		}

		public string ForUser
		{
			get;
			set;
		}

		public ICollection<RoleType> ForRoles
		{
			get;
			set;
		}

		public DateTime ExpiresAt
		{
			get;
			set;
		}
	}
}
