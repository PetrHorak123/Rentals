using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using Rentals.Web.Models;

namespace Rentals.Web.Areas.Admin.Controllers
{
	[Authorize(Policy = "AbsoluteRights")]
	public class RentalController : AdminBaseController
	{
		public RentalController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public ActionResult Index()
		{
			var model = FetchModel<BaseViewModel>();

			return View(model);
		}

		public ActionResult Edit()
		{
			var model = new RentalEditorViewModel(this.Rental);
		
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(RentalEditorViewModel postedModel)
		{
			if (ModelState.IsValid)
			{
				postedModel.UpdateEntity(this.Rental);
				RepositoriesFactory.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View(postedModel);
		}
	}
}