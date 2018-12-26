using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Extensions;
using System;
using Rentals.DL.Interfaces;

namespace Rentals.Web.Controllers
{
	[AllowAnonymous]
	public class AccountController : BaseController
	{
		public AccountController(IRepositoriesFactory factory) : base(factory)
		{
		}

		public ActionResult Login(string returnUrl = null)
		{
			// Implementace office 365


			return View();
		}

		public ActionResult DecideLogin(string returnUrl = null)
		{
			// Pokud nevím kam se chtěl dostal, vrátím ho na login pro zákazníky.
			if (returnUrl.IsNullOrEmpty())
				return RedirectToAction(nameof(Login), "Account", new { returnUrl });

			var splited = returnUrl.Split('/', StringSplitOptions.RemoveEmptyEntries);
			var area = splited.Length > 0 ? splited[0] : string.Empty;

			// Pokud se chtěl dostat do administrativy.
			if(area.ToLower() == "admin")
			{
				return RedirectToAction(nameof(Areas.Admin.Controllers.AccountController.Login), "Account", new { returnUrl, Area = "Admin" });
			}

			return RedirectToAction(nameof(Login), "Account", new { returnUrl });
		}

		public ActionResult AccessDenied(string returnUrl = null)
		{
			return View();
		}
	}
}