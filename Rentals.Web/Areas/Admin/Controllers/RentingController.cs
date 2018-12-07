using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class RentingController : AdminBaseController
	{
		private UserManager<User> userManager;

		public RentingController(IRepositoriesFactory factory, UserManager<User> userManager) : base(factory)
		{
			this.userManager = userManager;
		}

		public ActionResult Create()
		{
			var model = FetchModel(new RentingEditorViewModel());

			return View(model);
		}

		[HttpPost]
		public ActionResult Create(RentingEditorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				var renting = model.CreateEntity();
				this.RepositoriesFactory.Rentings.Add(renting);

				this.RepositoriesFactory.SaveChanges();

				return RedirectToAction("Index", "Calendar");
			}

			return View(model);
		}

		// Do customer controlleru, kterej ještě neextistuje..
		public JsonResult GetCustomers(string term)
		{
			if (term == null)
				return null;

			var customers = this.RepositoriesFactory.Users.FindCustomers(term)
				.Select(c => new CustomerSearchModel()
				{
					Id = c.Id,
					Name = c.UserName,
				}).ToArray();

			return Json(customers);
		}

		public ActionResult SetState(int id, RentalState? state)
		{
			if (state == null)
				return BadRequest();

			var renting = this.RepositoriesFactory.Rentings.GetById(id);

			if (renting == null)
				return NotFound();

			renting.State = state.Value;
			RepositoriesFactory.SaveChanges();

			return Content("OK");
		}
	}
}