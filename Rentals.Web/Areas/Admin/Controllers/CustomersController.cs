using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class CustomersController : AdminBaseController
	{
		public CustomersController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public async Task<IActionResult> Index()
		{
			var customers = await this.RepositoriesFactory.Users.GetUsersWithRolesAsync(this.CurrentUser.Id, RoleType.Customer);
			var model = customers.Select(c => new CustomerViewModel(c)).ToList();

			return View(model);
		}

		public ActionResult Detail(int id)
		{
			var customer = this.RepositoriesFactory.Users.GetById(id);

			if (customer == null)
				return NotFound();

			var model = FetchModel(new CustomerViewModel(customer, addRentings: true));

			return View(model);
		}
	}
}