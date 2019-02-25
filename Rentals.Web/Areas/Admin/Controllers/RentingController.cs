using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using Rentals.Web.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class RentingController : AdminBaseController
	{
		private readonly UserManager<User> userManager;
		private readonly IEmailSender sender;

		public RentingController(IRepositoriesFactory factory, UserManager<User> userManager, IEmailSender sender) : base(factory)
		{
			this.userManager = userManager;
			this.sender = sender;
		}

		public ActionResult Create()
		{
			var model = FetchModel(new RentingCreatorViewModel());

			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Create(RentingCreatorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				var renting = await model.CreateEntity(userManager);
				this.RepositoriesFactory.Rentings.Add(renting);

				this.RepositoriesFactory.SaveChanges();

				this.sender.SendRentingCreated(renting, this.CurrentUser, this.MicrosoftAccessToken);

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

				this.sender.SendRentingEdited(renting, this.MicrosoftAccessToken);

				return RedirectToAction("Detail", "Renting", new { id = renting.Id });
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

			if (state == RentalState.Canceled)
				this.sender.SendRentingCanceled(renting, this.CurrentUser, this.MicrosoftAccessToken);

			if(redirectToIndex)
				return RedirectToAction("Index", "Home");

			return Content("OK");
		}
	}
}