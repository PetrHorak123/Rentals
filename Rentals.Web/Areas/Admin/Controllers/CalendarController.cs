using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Rentals.Common.Extensions;

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
					nestedGroups = new List<int>()
				};

				foreach (var item in itemType.Items)
				{
					var id = item.Id + idIncrementForUniqueIds;

					itemTypeToAdd.nestedGroups.Add(id);

					items.Add(new
					{
						id,
						content = item.UniqueIdentifier,
						nestedInGroup = itemType.Id,
					});
				}

				itemTypes.Add(itemTypeToAdd);
			}

			return Json(new { types = itemTypes, items });
		}

		public JsonResult GetRentings(DateTime from, DateTime to)
		{
			

			var rentings = this.RepositoriesFactory.Rentings
				.GetRentingInTime(from, to)
				.SelectMany(r => r.RentingToItems)
				.Select(
					r => new
					{
						group = r.ItemId + idIncrementForUniqueIds,
						content = string.Format(
							"<a href=\"{0}\" class=\"timeline-a\">{1}, {2}</a>",
							Url.Action("Detail", "Renting", new { id = r.RentingId }),
							r.Renting.User.Name ?? r.Renting.User.UserName,
							r.Renting.User.Class ?? Localization.Admin.Calendar_NoClass
						),
						start = r.Renting.StartsAt,
						end = r.Renting.EndsAt,
						state = r.Renting.State,
						className = DecideColor(r.Renting.State, r.Renting.EndsAt),
						title = DecideTitle(r.Renting.State, r.Renting.EndsAt)
					})
				.ToArray();

			return Json(rentings);
		}

		private string DecideColor(RentalState state, DateTime endsAt)
		{
			string color = string.Empty;

			switch (state)
			{
				case RentalState.Lended:
					if (endsAt < DateTime.Now)
						color = "red";
					else
						color = "blue";
					break;
				case RentalState.NotLended:
					color = "orange";
					break;
				case RentalState.Returned:
					color = "green";
					break;
			}

			return color;
		}

		private string DecideTitle(RentalState state, DateTime endsAt)
		{
			string text = string.Empty;

			switch (state)
			{
				case RentalState.Lended:
					if (endsAt < DateTime.Now)
						text = Localization.Admin.Calendar_NotReturned;
					else
						text = Localization.Admin.Calendar_Lended;
					break;
				case RentalState.NotLended:
					text = text = Localization.Admin.Calendar_NotLended;
					break;
				case RentalState.Returned:
					text = Localization.Admin.Calendar_Returned;
					break;
			}

			return text;
		}
	}
}