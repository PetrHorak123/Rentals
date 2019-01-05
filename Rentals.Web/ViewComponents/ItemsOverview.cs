using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using System;
using System.Threading.Tasks;
using Rentals.DL.Entities;
using Rentals.Web.Models;

namespace Rentals.Web.ViewComponents
{
	public class ItemsOverview : ViewComponent
	{
		private readonly IRepositoriesFactory factory;

		public ItemsOverview(IRepositoriesFactory factory)
		{
			this.factory = factory;
		}

		public async Task<IViewComponentResult> InvokeAsync(DateTime? from = null, DateTime? to = null)
		{
			ItemOverviewViewModel model;

			if(!from.HasValue || !to.HasValue)
			{
				var types = await factory.Types.GetItemTypesAsync();
				model = new ItemOverviewViewModel(types);
			}
			else
			{
				to = to.Value.AddDays(1);
				var items = await factory.Items.GetAllAvaibleItemsAsync(from.Value, to.Value);
				model = new ItemOverviewViewModel(items);
			}

			return View(model);
		}
	}
}
