using Rentals.DL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Models
{
	public class CustomerViewModel : UserModel
	{
		public CustomerViewModel(User user, bool addRentings = false) : base(user)
		{
			this.Class = user.ActualClass;

			if (addRentings)
			{
				this.Rentings = user.Rentings.Select(r => new RentingViewModel(r)).ToList();
			}
		}

		public CustomerViewModel(User user, IEnumerable<History> histories, bool addrentings = false) : this(user, addrentings)
		{
			if (histories != null)
			{
				this.History = histories.Select(h => new HistoryViewModel(h));
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

		public IEnumerable<HistoryViewModel> History
		{
			get;
			set;
		}
	}
}
