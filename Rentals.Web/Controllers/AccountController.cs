using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Extensions;
using System;
using Rentals.DL.Interfaces;

namespace Rentals.Web.Controllers
{
	public class AccountController : BaseController
	{
		public AccountController(IRepositoriesFactory factory) : base(factory)
		{
		}

		[AllowAnonymous]
		public ActionResult Login(string returnUrl = null)
		{


			return View();
		}

		public ActionResult DecideLogin(string returnUrl = null)
		{
			// Pokud nevím kam se chtěl dostal, vrátím ho na login pro zákazníky.
			if (returnUrl.IsNullOrEmpty())
				return RedirectToAction(nameof(Login), "Account", new { returnUrl });

			var area = returnUrl.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];

			// Pokud se chtěl dostat do administrativy.
			if(area.ToLower() == "admin")
			{
				// Pokud není přihlášen přesměruji ho na login.
				return RedirectToAction(nameof(Areas.Admin.Controllers.AccountController.Login), "Account", new { returnUrl, Area = "Admin" });

				// Pokud je přihlášen, znamená to, že nemá dostatečná oprávnění a vyhodím hlášku.

			}

			return RedirectToAction(nameof(Login), "Account", new { returnUrl });
		}
	}
}