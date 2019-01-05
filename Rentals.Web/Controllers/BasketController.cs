using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;

namespace Rentals.Web.Controllers
{
	public class BasketController : BaseController
	{
		public BasketController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateRenting()
		{
			return RedirectToAction("Index", "Home");
		}

		public ActionResult AddToBasket(int typeId, int count)
		{
			return Content("OK");
		}

		public ActionResult AddToBasket(string uid)
		{
			return Content("OK");
		}
	}
}