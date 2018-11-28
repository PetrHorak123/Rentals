using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class CalendarController : AdminBaseController
	{
		/// <summary>
		/// Konstanta označující, jaké číslo se přidá k id předmětu, aby mi nevnikali duplikátní id pro timeline.
		/// </summary>
		private const int idIncrementForUniqueIds = 2000;

		public CalendarController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public ActionResult Index()
		{
			var model = FetchModel(new ItemTypesViewModel(this.RepositoriesFactory.Types.GetItemTypes()));

			return View(model);
		}

		public JsonResult GetTypesAndItems()
		{
			var items = new List<object>();
			var itemTypes = new List<object>();

			foreach (var itemType in this.RepositoriesFactory.Types.GetItemTypes())
			{
				var itemTypeToAdd = new
				{
					id = itemType.Id,
					content = itemType.Name,
					showNested = false,
					nestedGroups = new List<int>()
				};

				foreach (var item in itemType.Items)
				{
					var id = item.Id + idIncrementForUniqueIds;

					itemTypeToAdd.nestedGroups.Add(id);

					items.Add(new
					{
						id = id,
						content = item.UniqueIdentifier,
						visible = false,
						nestedInGroup = itemType.Id
					});
				}

				itemTypes.Add(itemTypeToAdd);
			}

			return Json(new { types = itemTypes, items = items });
		}

		public JsonResult GetRentings(DateTime from, DateTime to)
		{
			var rentings = this.RepositoriesFactory.Rentings.GetRentingInTime(from, to).SelectMany(r => r.RentingToItems).Select(r => new
			{
				group = r.ItemId + idIncrementForUniqueIds,
				content = r.Renting.User.UserName,
				start = r.Renting.StartsAt,
				end = r.Renting.EndsAt,
				state = r.Renting.State,
			})
			.ToArray();

			return Json(rentings);
		}
	}
}