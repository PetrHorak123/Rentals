using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Extensions;
using Rentals.DL.Interfaces;
using Rentals.Web.Areas.Admin.ViewComponents;
using Rentals.Web.Interfaces;
using Rentals.Web.Models;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class HomeController : AdminBaseController
	{
		private readonly IEmailSender sender;

		public HomeController(IRepositoriesFactory factory, IEmailSender sender) : base(factory)
		{
			this.sender = sender;
		}

		public ActionResult Index()
		{
			var model = FetchModel<BaseViewModel>();

			return View(model);
		}

		public ActionResult ReloadComponentView()
		{
			return ViewComponent(nameof(RentingOverview));
		}

		public async Task<ActionResult> SendNotifications()
		{
			if (this.MicrosoftAccessToken.IsNullOrEmpty())
				return BadRequest(Localization.Admin.RentingOverview_LoginUsingMicrosft);

			// Radši awaituji, zde rychlost potřebná není a navíc netuším kolik jich bude.
			var result = await this.sender.SendRentingNotReturned(this.MicrosoftAccessToken);

			if (!result)
				return StatusCode(503, Localization.Admin.RentingOverview_MicrosoftApiDown);

			// Radši awaituji, zde rychlost potřebná není a navíc netuším kolik jich bude.
			var result2 = await this.sender.SendRentingReminder(this.MicrosoftAccessToken);

			if (!result2)
				return StatusCode(503, Localization.Admin.RentingOverview_MicrosoftApiDown);

			return Content("OK");
		}
	}
}