using Rentals.DL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Models
{
	public class CustomerViewModel : UserModel
	{
		public CustomerViewModel(User user, bool addRentings = false) : base(user)
		{
			this.Class = user.Class;

			if (addRentings)
			{
				this.Rentings = user.Rentings.Select(r => new RentingViewModel(r)).ToList();
			}
		}

		public string Class
		{
			get;
			set;
		}

		public ICollection<RentingViewModel> Rentings
		{
			get;
			set;
		}
	}
}
