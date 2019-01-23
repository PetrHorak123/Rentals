using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Enums;
using Rentals.DL;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentals.Web.Controllers
{
	public class BasketController : BaseController
	{
		private readonly EntitiesContext context;

		public BasketController(IRepositoriesFactory factory, EntitiesContext context) : base(factory)
		{
			this.context = context;
		}

		[Route("/Basket")]
		public ActionResult CreateRenting()
		{
			EnsureValidBasket();

			var model = FetchModel(new RentingUserCreatorViewModel());

			return View(model);
		}

		private void EnsureValidBasket()
		{
			var keysToRemove = new List<string>();

			foreach (var itemToRent in this.CurrentUser.Basket)
			{
				if (itemToRent.Value == -1)
				{
					var item = this.RepositoriesFactory.Items.GetByUniqueIdentifier(itemToRent.Key);

					if (item == null || item.IsDeleted)
						keysToRemove.Add(itemToRent.Key);
				}
				else
				{
					var type = this.RepositoriesFactory.Types.GetByName(itemToRent.Key);

					if (type == null || type.IsDeleted)
						keysToRemove.Add(itemToRent.Key);
				}
			}

			foreach(var key in keysToRemove)
			{
				this.CurrentUser.Basket.Remove(key);
			}

			this.context.Users.Update(this.CurrentUser);
			this.context.SaveChanges();
		}

		[HttpPost]
		[Route("/Basket")]
		public ActionResult CreateRenting(RentingUserCreatorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				var items = new List<Item>();
				bool allItemsAvaible = true;

				foreach (var itemToOrder in this.CurrentUser.Basket)
				{
					// Je specifický
					if(itemToOrder.Value == -1)
					{
						var item = this.RepositoriesFactory.Items.GetByUniqueIdentifier(itemToOrder.Key);

						if(!item.IsAvaible(model.StartsAt, model.EndsAt))
						{
							allItemsAvaible = false;
							ModelState.AddModelError(string.Empty, 
								string.Format(Localization.Localization.Renting_ItemNotAvaible, item.Type.Name + " " + item.UniqueIdentifier));
							items.Add(item);
						}
					}
					// Je obecný
					else
					{
						var type = this.RepositoriesFactory.Types.GetByName(itemToOrder.Key);
						var avaibleItems = this.RepositoriesFactory.Items
							.GetNonSpecificAvaibleItems(type.Id, model.StartsAt, model.EndsAt);

						// Pokud je dostatek předmětů dostupný.
						if(avaibleItems.Length < itemToOrder.Value)
						{
							allItemsAvaible = false;
							ModelState.AddModelError(string.Empty, 
								string.Format(Localization.Localization.Renting_ItemsNotAvaible, type.Name, avaibleItems.Length));
						}
						else
						{
							// Přidám ty, které byly vypůjčeny nejméně.
							items.AddRange(avaibleItems.OrderBy(r => r.RentingToItems.Count).Take(itemToOrder.Value));
						}
					}
				}

				if (allItemsAvaible)
				{
					// Přidám výpůjčku.
					var renting = Renting.Create(this.CurrentUser.Id, model.StartsAt, model.EndsAt, RentalState.NotLended, string.Empty, items);
					this.RepositoriesFactory.Rentings.Add(renting);

					// Vymažu mu košík.
					this.CurrentUser.Basket = new Dictionary<string, int>();
					this.context.Users.Update(this.CurrentUser);

					this.RepositoriesFactory.SaveChanges();

					return View("RentingCreationSuccessful");
				}

				return View(model);
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult AddItemsToBasket(string itemType, int count)
		{
			var type = this.RepositoriesFactory.Types.GetByName(itemType);

			if (type == null)
				return NotFound();

			if (type.ActualItems.Count < count || count == 0)
				return BadRequest();

			if (this.CurrentUser.Basket.ContainsKey(itemType))
			{
				this.CurrentUser.Basket[itemType] = count;
			}
			else
			{
				this.CurrentUser.Basket.Add(itemType, count);
			}

			this.context.Users.Update(this.CurrentUser);
			this.context.SaveChanges();

			return Redirect(Request.Headers["Referer"].ToString() ?? "/");
		}

		[HttpPost]
		public ActionResult AddItemToBasket(string uid)
		{
			var item = this.RepositoriesFactory.Items.GetByUniqueIdentifier(uid);
			if (item == null)
				return NotFound();

			if (this.CurrentUser.Basket.ContainsKey(uid))
				return BadRequest();

			this.CurrentUser.Basket.Add(uid, -1);
			this.context.Users.Update(this.CurrentUser);
			this.context.SaveChanges();

			return Redirect(Request.Headers["Referer"].ToString() ?? "/");
		}

		[HttpPost]
		public ActionResult RemoveItemFromBasket(string item)
		{
			if (item == null)
				return BadRequest();

			if (this.CurrentUser.Basket.ContainsKey(item))
			{
				this.CurrentUser.Basket.Remove(item);
				this.context.Users.Update(this.CurrentUser);
				this.context.SaveChanges();

				return Redirect(Request.Headers["Referer"].ToString() ?? "/");
			}

			return NotFound();
		}
	}
}