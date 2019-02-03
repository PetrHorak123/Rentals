using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using Rentals.Web.Areas.Admin.ViewComponents;
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

		public async Task<IActionResult> RemoveRole(int id, RoleType role)
		{
			var user = this.RepositoriesFactory.Users.GetById(id);

			if (role == RoleType.Customer || user == null)
				return BadRequest();

			await userManager.RemoveFromRoleAsync(user, role.ToString());

			return Content("OK");
		}

		public ActionResult DeleteLink(int id)
		{
			var link = this.RepositoriesFactory.AdminInvites.GetById(id);

			if (link == null)
				return NotFound();

			this.RepositoriesFactory.AdminInvites.Remove(link);
			this.RepositoriesFactory.SaveChanges();

			return RedirectToAction("Index");
		}

		public ActionResult ReloadComponentView()
		{
			return ViewComponent(nameof(Employees));
		}
	}
}