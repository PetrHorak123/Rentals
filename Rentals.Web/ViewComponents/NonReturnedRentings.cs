using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using Rentals.Web.Models;
using System.Threading.Tasks;

namespace Rentals.Web.ViewComponents
{
	public class NonReturnedRentings : ViewComponent
	{
		private readonly IRepositoriesFactory factory;

		public NonReturnedRentings(IRepositoriesFactory factory)
		{
			this.factory = factory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var rentings = await this.factory.Rentings.GetNonReturnedForUser(this.factory.Users.GetByName(this.User.Identity.Name).Id);

			var model = new NonReturnedRentingsViewModel(rentings, this.factory);

			return View(model);
		}
	}
}
