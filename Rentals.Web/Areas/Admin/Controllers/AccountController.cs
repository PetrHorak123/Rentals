using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Extensions;
using Rentals.DL.Interfaces;
using Rentals.DL.Entities;
using Rentals.Web.Areas.Admin.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.Controllers
{
	[AllowAnonymous]
	public class AccountController : AdminBaseController
	{
		private readonly SignInManager<User> signInManager;

		public AccountController(IRepositoriesFactory factory, SignInManager<User> signInManager) : base(factory)
		{
			this.signInManager = signInManager;
		}

		public async Task<ActionResult> Login(string returnUrl = null)
		{
			var model = new LoginViewModel()
			{
				ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
				ReturnUrl = returnUrl,
			};

			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Login(LoginViewModel postedModel)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(postedModel.UserName,
					postedModel.Password, postedModel.RememberMe, lockoutOnFailure: true);

				if (result.Succeeded)
				{
					if (postedModel.ReturnUrl.IsNullOrEmpty())
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						return Redirect(postedModel.ReturnUrl);
					}
				}

				if (result.IsLockedOut)
				{
					return RedirectToAction(nameof(Lockout), "Account");
				}

				ModelState.AddModelError(string.Empty, Localization.Admin.Login_Error);
			}

			return View(postedModel);
		}

		public async Task<ActionResult> Logout()
		{
			await signInManager.SignOutAsync();

			return View();
		}

		public ActionResult Lockout()
		{
			return View();
		}
	}
}