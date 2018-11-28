using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
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
	}
}