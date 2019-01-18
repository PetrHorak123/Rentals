using Rentals.Common.Enums;
using Rentals.DL.Entities;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Viewmodel reprezentující zaměstance půjčovny.
	/// </summary>
	public class EmployeeViewModel
	{
		public EmployeeViewModel(User user)
		{
			this.Id = user.Id;
			this.Name = user.UserName;
			this.Email = user.Email;
			this.Roles = user.Roles.Select(r => r.Role.RoleType).ToArray();
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
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
