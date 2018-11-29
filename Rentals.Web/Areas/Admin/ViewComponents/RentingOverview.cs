using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System;
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
			var rentings = await factory.Rentings.GetRentingInTimeAsync(dateValue, dateValue.AddDays(1));
			var model = new RentingOverviewViewModel(rentings);

			return View(model);
		}
	}
}
