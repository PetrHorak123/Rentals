using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using Rentals.Web.Areas.Admin.ViewComponents;
using System;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class ItemsController : AdminBaseController
	{
		public ItemsController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public ActionResult Index(string q = null)
		{
			var model = FetchModel(new ItemTypesViewModel(this.RepositoriesFactory.Types.GetItemTypes(q)));
			ViewData["q"] = q;

			return View(model);
		}

		public ActionResult Detail(int id)
		{
			var itemType = RepositoriesFactory.Types.GetById(id);

			if (itemType == null)
				return NotFound();

			var model = FetchModel(new ExtendedItemTypeViewModel(itemType));

			return View(model);
		}

		public ActionResult Create()
		{
			var model = FetchModel<ItemTypeEditorViewModel>();

			return View(model);
		}

		[HttpPost]
		public ActionResult Create(ItemTypeEditorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				// Vytvořím si typ a předměty (ty se vytvářejí ve vnitř), volá SaveChanges
				var type = model.CreateEntity(this.RepositoriesFactory, this.Rental);

				return RedirectToAction("Index", "Items");
			}

			return View(postedModel);
		}

		public ActionResult Edit(int id)
		{
			var itemType = RepositoriesFactory.Types.GetById(id);

			if (itemType == null)
				return NotFound();

			var model = this.FetchModel(new ItemTypeEditorViewModel(itemType));

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(ItemTypeEditorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				var itemType = RepositoriesFactory.Types.GetById(model.Id);

				if (itemType == null)
					return NotFound();

				model.UpdateEntity(itemType);
				this.RepositoriesFactory.SaveChanges();

				return RedirectToAction("Detail", "Items", new { id = model.Id });
			}

			return View(model);
		}

		// Udělat jako komponentu celej seznam předmětů ?
		[HttpPost]
		public ActionResult AddItem(ItemTypeEditorViewModel postedModel)
		{
			var model = FetchModel(postedModel);

			if (ModelState.IsValid)
			{
				var itemType = RepositoriesFactory.Types.GetById(postedModel.Id);

				if (itemType == null)
					return NotFound();

				// Vnitřně volá save changes.
				model.AddItem(itemType, this.RepositoriesFactory);

				return RedirectToAction("Edit", "Items", new { id = postedModel.Id });
			}

			return RedirectToAction("Edit", "Items", new { id = postedModel.Id });
		}

		public ActionResult Delete(int itemId)
		{
			var item = this.RepositoriesFactory.Items.GetById(itemId);

			if (item == null)
				return NotFound();

			item.Delete();
			this.RepositoriesFactory.SaveChanges();

			return RedirectToAction("Edit", "Items", new { id = item.ItemTypeId });
		}

		public ActionResult DeleteType(int id)
		{
			var itemType = RepositoriesFactory.Types.GetById(id);

			if (itemType == null)
				return NotFound();

			itemType.Delete();
			this.RepositoriesFactory.SaveChanges();

			return RedirectToAction("Index", "Items");
		}

		public ActionResult RemoveAccessory(int accessoryToId, int accessoryId)
		{
			var itemType = RepositoriesFactory.Types.GetById(accessoryToId);

			if (itemType == null)
				return NotFound();

			itemType.Accessories.Remove(itemType.Accessories.Single(a => a.AccesoryId == accessoryId));
			this.RepositoriesFactory.SaveChanges();

			return Content("OK");
		}

		public ActionResult AddAccessory(int accessoryToId, int accessoryId)
		{
			var itemType = this.RepositoriesFactory.Types.GetById(accessoryToId);

			if (itemType == null)
				return NotFound();

			var itemTypeToType = this.RepositoriesFactory.Accessories
				.Find(a => 
					(a.AccesoryId == accessoryId && a.AccesoryToId == accessoryToId) &&
					(a.AccesoryToId == accessoryId && a.AccesoryId == accessoryToId)
				);

			if(itemTypeToType.Length != 0)
			{
				return BadRequest();
			}

			this.RepositoriesFactory.Accessories.Add(ItemTypeToItemType.Create(accessoryToId, accessoryId));
			this.RepositoriesFactory.SaveChanges();

			return Content("OK");
		}

		public ActionResult ReloadComponentView(int id)
		{
			return ViewComponent(nameof(Accessories), new { id });
		}

		public JsonResult AvaibleAccessories(int id)
		{
			var itemType = this.RepositoriesFactory.Types.GetById(id);

			if (itemType == null)
				return null;

			var itemTypes = this.RepositoriesFactory.Types.GetAvaibleAccessories(id)
				.Select(t => new ItemTypeViewModel()
				{
					Id = t.Id,
					Name = t.Name,
				});

			return Json(itemTypes);

		}

		public JsonResult GetItemTypes()
		{
			var itemTypes = this.RepositoriesFactory.Types.GetItemTypes()
				.Select(t => new ItemTypeViewModel()
				{
					Id = t.Id,
					Name = t.Name,
				});

			return Json(itemTypes);
		}

		public JsonResult GetAvaibleItems(int itemTypeId, string startsAt, string endsAt)
		{
			var itemType = RepositoriesFactory.Types.GetById(itemTypeId);

			var startsAtDateTime = DateTime.Parse(startsAt);
			var endsAtDateTime = DateTime.Parse(endsAt);

			if (startsAt == null || endsAt == null || itemType == null)
				return null;

			var items = this.RepositoriesFactory.Items
				.GetAvailbeItems(itemTypeId, startsAtDateTime, endsAtDateTime)
				.Select(i => new ItemSearchModel(i))
				.ToArray();

			return Json(items);
		}
	}
}