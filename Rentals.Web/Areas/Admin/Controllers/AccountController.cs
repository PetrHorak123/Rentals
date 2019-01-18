using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentals.Common.Extensions;
using Rentals.DL.Interfaces;
using Rentals.DL.Entities;
using Rentals.Web.Areas.Admin.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Rentals.Common.Enums;

namespace Rentals.Web.Areas.Admin.Controllers
{
	[AllowAnonymous]
	public class AccountController : AdminBaseController
	{
		private readonly SignInManager<User> signInManager;
		private readonly IAuthorizationService authorization;
		private readonly UserManager<User> userManager;

		public AccountController(IRepositoriesFactory factory, SignInManager<User> signInManager, IAuthorizationService authorization, UserManager<User> userManager) : base(factory)
		{
			this.signInManager = signInManager;
			this.authorization = authorization;
			this.userManager = userManager;
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

			postedModel.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
			return View(postedModel);
		}

		[HttpPost]
		public ActionResult ExternalLogin(LoginViewModel postedModel)
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

			// pole 
			var cloud = await authorization.AuthorizeAsync(this.User, email, "PslibCloud");
			var office = await authorization.AuthorizeAsync(this.User, email, "Pslib365");

			if (!cloud.Succeeded && !office.Succeeded)
				return Content("Sorry pslib only");

			var name = info.Principal.FindFirstValue(ClaimTypes.Name);

			var link = this.RepositoriesFactory.AdminInvites.GetByUser(name);

			var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
				info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
			if (result.Succeeded)
			{
				var user = this.RepositoriesFactory.Users.GetByName(email);
				await TryAddRole(link, user);

				await signInManager.SignInAsync(user, isPersistent: false);

				return RedirectToLocal(returnUrl);
			}
			if (result.IsLockedOut)
			{
				return Content("Locked");
			}

			if (link != null)
			{
				// Uživatel není v databázi, vytvořím ho.
				var user = new User { UserName = email, Email = email };
				var userResult = await userManager.CreateAsync(user);
				if (userResult.Succeeded)
				{
					await TryAddRole(link, user);
					userResult = await userManager.AddLoginAsync(user, info);
					if (userResult.Succeeded)
					{
						await signInManager.SignInAsync(user, isPersistent: false);

						return RedirectToLocal(returnUrl);
					}
				}
			}

			return Content("You are not invited");
		}

		private async Task TryAddRole(AdminInvite link, User user)
		{
			if (link != null)
			{
				if (link.WillBeAdmin)
					await this.userManager.AddToRoleAsync(user, RoleType.Administrator.ToString());
				if (link.WillBeEmployee)
					await this.userManager.AddToRoleAsync(user, RoleType.Employee.ToString());

				link.IsRedeemed = true;
				RepositoriesFactory.SaveChanges();
			}
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