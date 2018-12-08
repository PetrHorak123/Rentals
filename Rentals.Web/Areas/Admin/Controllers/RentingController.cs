using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class RentingController : AdminBaseController
	{
		public RentingController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public ActionResult Create()
		{
			var model = FetchModel(new RentingCreatorViewModel());

			return View(model);
		}

		[HttpPost]
		public ActionResult Create(RentingCreatorViewModel postedModel)
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

		public ActionResult Detail(int id)
		{
			var renting = this.RepositoriesFactory.Rentings.GetById(id);

			if (renting == null)
				return NotFound();

			var model = new RentingViewModel(renting);

			return View(model);
		}

		public ActionResult Edit(int id)
		{
			var renting = this.RepositoriesFactory.Rentings.GetById(id);

			if (renting == null)
				return NotFound();

			if (renting.EndsAt <= DateTime.Now)
				return BadRequest();

			var model = FetchModel(new RentingEditorViewModel(renting));

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(RentingEditorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				// Pokud by neexistovala, neprojde přes IValidateObject v modelu.
				var renting = this.RepositoriesFactory.Rentings.GetById(postedModel.RentingId);
				model.UpdateEntity(renting);
				this.RepositoriesFactory.SaveChanges();
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

		public ActionResult SetState(int id, RentalState? state, bool redirectToIndex = false)
		{
			if (state == null)
				return BadRequest();

			var renting = this.RepositoriesFactory.Rentings.GetById(id);

			if (renting == null)
				return NotFound();

			renting.State = state.Value;
			RepositoriesFactory.SaveChanges();

			if(redirectToIndex)
				return RedirectToAction("Index", "Home");

			return Content("OK");
		}
	}
}