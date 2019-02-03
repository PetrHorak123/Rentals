using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.Common.Extensions;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class HistoryController : AdminBaseController
	{
		public HistoryController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public ActionResult Create(int id)
		{
			var renting = this.RepositoriesFactory.Rentings.GetById(id);

			if (renting == null /*|| renting.State != RentalState.Lended*/)
				return BadRequest();

			var model = new HistoryCreatorViewModel(renting);

			return View(model);
		}

		[HttpPost]
		public ActionResult Create(HistoryCreatorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				var renting = this.RepositoriesFactory.Rentings.GetById(model.RentingId);

				foreach (var history in model.ItemsHistory)
				{
					if (history.AddToHistory && !history.Content.IsNullOrEmpty())
					{
						var item = this.RepositoriesFactory.Items.GetByUniqueIdentifier(history.Item);

						if (item == null)
							return NotFound();

						var historyRecord = History.CreateEntity(history.Content, model.RentingId, item.Id);
						this.RepositoriesFactory.Histories.Add(historyRecord);

						if (history.IsImportant)
						{
							item.Note = history.NewDescription;
						}
					}
				}

				renting.State = RentalState.Returned;
				this.RepositoriesFactory.SaveChanges();

				return RedirectToAction("Index", "Home");
			}

			return View(postedModel);
		}
	}
}