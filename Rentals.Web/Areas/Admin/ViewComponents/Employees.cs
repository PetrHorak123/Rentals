using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.ViewComponents
{
	public class Employees : ViewComponent
	{
		private readonly IRepositoriesFactory factory;

		public Employees(IRepositoriesFactory factory)
		{
			this.factory = factory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var currentUser = this.factory.Users.GetByName(this.User.Identity.Name);
			var employees = await this.factory.Users.GetUsersWithRolesAsync(currentUser.Id, RoleType.Employee, RoleType.Administrator);

			var model = employees.Select(e => new EmployeeViewModel(e)).ToList();

			return View(model);
		}
	}
}
