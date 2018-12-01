using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.ViewComponents
{
	public class RentingOverview : ViewComponent
	{
		private readonly IRepositoriesFactory factory;

		public RentingOverview(IRepositoriesFactory factory)
		{
			this.factory = factory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var dateValue = DateTime.Today;
			var todayRentings = await factory.Rentings.GetRentingInTimeAsync(dateValue, dateValue.AddDays(1));
			var nonRenturnedRentings = await factory.Rentings.GetNonRetruned();

			var rentings = todayRentings.Union(nonRenturnedRentings);

			var model = new RentingOverviewViewModel(rentings);

			return View(model);
		}
	}
}
