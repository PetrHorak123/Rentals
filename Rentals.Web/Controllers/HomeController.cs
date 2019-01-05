using System;
using System.Collections.Generic;
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
				var itemId = this.RepositoriesFactory.Items.GetByUniqueIdentifier(item).Id;

				var data = this.RepositoriesFactory.Rentings
					.GetRentingsInTimeForItem(itemId, from, to).Select(r =>
						new AvaibilityViewModel(r.StartsAt, r.EndsAt)
					);

				return Json(data);
			}
			else
			{
				var type = this.RepositoriesFactory.Types.GetByName(item);

				var rentings = this.RepositoriesFactory.Rentings
					.GetRentingsInTimeForType(type.Id, from, to);

				// Pokud nejsou výpůjčky, nemusím nic dělat.
				if (rentings.Length == 0)
				{
					return Json(new int[0]);
				}

				// Tady začíná sranda, pokračování ve čtení je pouze na vlasní nebezpečí.
				var results = new List<AvaibilityViewModel>();

				// Pro všechny výpůjčky z odbodí.
				foreach (var referenceRenting in rentings)
				{
					int overlapingRentings = 0;

					// Pro všechny ostatní, kountroluji překrývání.
					foreach (var renting in rentings)
					{
						// Pokud tato, končí dýl nebo ve stejnou chvíli než tak, u které se pohybu v nadřazeném loopu a překrývají se,
						// spadne sem i pokud je stejná ajok v nadřazeném loopu, proto inicializuju zpočátku předměty na nulu.
						if (renting.EndsAt >= referenceRenting.EndsAt && referenceRenting.IsOverlapingWith(renting))
						{
							// Přičtu vypůjčené předměty.
							overlapingRentings += renting.ItemsForType(type.Id).Length;
						}
					}

					// Pokud mi po odčetní vypůjčených předmětů od všechny zbylo míň než si chce vypůjčit, zaznamenám to.
					if (type.ActualItems.Count - overlapingRentings < count)
					{
						// Nakonec přidám a konec srandy :(.
						results.Add(new AvaibilityViewModel(referenceRenting.StartsAt, referenceRenting.EndsAt));
					}
				}

				return Json(results);
			}
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
