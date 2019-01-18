using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.Common.Extensions;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class EmployeesController : AdminBaseController
	{
		private UserManager<User> userManager;

		public EmployeesController(IRepositoriesFactory factory, UserManager<User> userManager) : base(factory)
		{
			this.userManager = userManager;
		}

		public ActionResult Index()
		{
			var model = FetchModel<EmployeeListViewModel>();
			model.ActiveLinks = this.RepositoriesFactory.AdminInvites.GetActiveInvites()
				.Select(l => new AdminLinkViewModel(l))
				.ToList();

			return View(model);
		}

		public ActionResult CreateInviteLink()
		{
			var model = FetchModel<AdminLinkEditorViewModel>();

			return View(model);
		}

		[HttpPost]
		public ActionResult CreateInviteLink(AdminLinkEditorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				var link = model.Create();
				this.RepositoriesFactory.AdminInvites.Add(link);
				this.RepositoriesFactory.SaveChanges();

				return RedirectToAction("Index");
			}

			return View(model);
		}

		// Testovací metoda, TODO: REMOVE
		public async Task<IActionResult> Create(string name)
		{
			var poweruser = new User()
			{
				UserName = name
			};

			string userPassword = "P@ssw0rd";

			var createPowerUser = await userManager.CreateAsync(poweruser, userPassword);
			if (createPowerUser.Succeeded)
			{
				// Přidání Admin role
				await userManager.AddToRoleAsync(poweruser, RoleType.Employee.ToString());
			}

			return Content("OK");
		}
	}
}