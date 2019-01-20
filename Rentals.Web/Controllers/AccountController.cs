using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Extensions;
using System;
using Rentals.DL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Rentals.DL.Entities;
using Rentals.Web.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Rentals.Common.Enums;

namespace Rentals.Web.Controllers
{
	[AllowAnonymous]
	public class AccountController : BaseController
	{
		private readonly IAuthorizationService authorization;
		private readonly SignInManager<User> signInManager;
		private readonly UserManager<User> userManager;

		public AccountController(IRepositoriesFactory factory, SignInManager<User> signInManager, UserManager<User> userManager, IAuthorizationService authorization) : base(factory)
		{
			this.authorization = authorization;
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		public async Task<ActionResult> Login(string returnUrl = null)
		{
			var model = new UserLoginViewModel()
			{
				ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
				ReturnUrl = returnUrl,
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult ExternalLogin(UserLoginViewModel postedModel)
		{
			var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl = postedModel.ReturnUrl });
			var properties = signInManager.ConfigureExternalAuthenticationProperties(postedModel.Provider, redirectUrl);

			return Challenge(properties, postedModel.Provider);
		}

		public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
		{
			if (remoteError != null)
			{
				return RedirectToAction(nameof(Login));
			}

			var info = await signInManager.GetExternalLoginInfoAsync();
			if (info == null)
			{
				return RedirectToAction(nameof(Login));
			}

			var email = info.Principal.FindFirstValue(ClaimTypes.Email);

			var office = await authorization.AuthorizeAsync(this.User, email, "PslibOnly");

			if (!office.Succeeded)
				return View("PslibOnly");

			var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
				info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
			if (result.Succeeded)
			{
				return RedirectToLocal(returnUrl);
			}
			if (result.IsLockedOut)
			{
				return Content("Locked");
			}

			var user = this.RepositoriesFactory.Users.GetByName(email);

			if (user != null)
			{
				await signInManager.SignInAsync(user, isPersistent: false);
				return RedirectToLocal(returnUrl);
			}

			// Uživatel není v databázi, vytvořím ho.
			user = new User { UserName = email, Email = email };
			var userResult = await userManager.CreateAsync(user);
			if (userResult.Succeeded)
			{
				await userManager.AddToRoleAsync(user, RoleType.Customer.ToString());
				userResult = await userManager.AddLoginAsync(user, info);
				if (userResult.Succeeded)
				{
					await signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToLocal(returnUrl);
				}
			}

			return RedirectToAction(nameof(Login));
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

		public async Task<ActionResult> Logout()
		{
			await signInManager.SignOutAsync();

			return View();
		}
	}
}