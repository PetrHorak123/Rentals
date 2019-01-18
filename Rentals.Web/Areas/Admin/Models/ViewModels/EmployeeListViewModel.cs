using Rentals.Web.Models;
using System.Collections.Generic;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// View model pro seznam zaměstnanců.
	/// </summary>
	public class EmployeeListViewModel : BaseViewModel
	{
		public ICollection<AdminLinkViewModel> ActiveLinks
		{
			get;
			set;
		}
	}
}
