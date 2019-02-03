using Rentals.Common.Enums;
using Rentals.DL.Entities;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Viewmodel reprezentující zaměstance půjčovny.
	/// </summary>
	public class EmployeeViewModel : UserModel
	{
		public EmployeeViewModel(User user) : base(user)
		{
			this.Roles = user.Roles.Select(r => r.Role.RoleType).ToArray();
		}

		/// <summary>
		/// Role, do kterých zamšstnanec spadá (může jich být i více).
		/// </summary>
		public RoleType[] Roles
		{
			get;
			set;
		}
	}
}
