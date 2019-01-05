using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using Rentals.Web.Models;
using Rentals.Web.ViewComponents;

namespace Rentals.Web.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public ActionResult Index(DateTime? from = null, DateTime? to = null)
		{
			dynamic vm = new ExpandoObject();
			vm.from = from;
			vm.to = to;
			vm.rental = this.Rental.Name;

			return View(vm);
		}

		[Route("/TypeDetail/{itemType}")]
		public ActionResult TypeDetail(string itemType)
		{
			var item = this.RepositoriesFactory.Types.GetByName(itemType, withSpaces: false);
			if (item == null)
				return NotFound();

			var model = this.FetchModel(new ItemDetailViewModel(item));

			return View("Detail", model);
		}

		[Route("ItemDetail/{uid}")]
		public ActionResult ItemDetail(string uid)
		{
			var item = this.RepositoriesFactory.Items.GetByUniqueIdentifier(uid, withSpaces: false);
			if (item == null)
				return NotFound();

			var model = this.FetchModel(new ItemDetailViewModel(item));

			return View("Detail", model);
		}

		public JsonResult GetTimeLine(string item, int count, bool isSpecificItem, DateTime from, DateTime to)
		{
			if (isSpecificItem)
			{
				var data = this.RepositoriesFactory.Rentings
					.GetRentingsInTimeForItem(item, from, to).Select(r => new
					{
						start = r.StartsAt,
						end = r.EndsAt,
						rendering = "background"
					});

				return Json(data);
			}
			else
			{

			}

			return Json(1);
		}

		public ActionResult SeeItems(DateTime? from, DateTime? to)
		{
			return ViewComponent(nameof(ItemsOverview), new { from, to });
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
