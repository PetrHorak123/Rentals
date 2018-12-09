using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.Models;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.ViewComponents
{
	public class Accessories : ViewComponent
	{
		private readonly IRepositoriesFactory factory;

		public Accessories(IRepositoriesFactory factory)
		{
			this.factory = factory;
		}

		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			var data = await factory.Types.GetAccessoriesAsync(id);
			var model = new AccessoryItemTypesViewModel(data, id);

			return View(model);
		}
	}
}
