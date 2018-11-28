using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Model pro přihášení do administrace.
	/// </summary>
	/// <remarks>
	/// Administrace má svůj vlstní login systém, kvůli jednomu uživateli "natvrdo".
	/// </remarks>
	public class LoginViewModel
	{
		/// <summary>
		/// Externí služby pro přihlášení, později možná zmizí, jestě nevím jak to bude se školnímy 365.
		/// </summary>
		public List<AuthenticationScheme> ExternalLogins
		{
			get;
			set;
		}

		/// <summary>
		/// Url přesměrování po přihlášení.
		/// </summary>
		public string ReturnUrl
		{
			get;
			set;
		}

		/// <summary>
		/// Přihlašovací jméno.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.UserName), ResourceType = typeof(Localization.Admin))]
		public string UserName
		{
			get;
			set;
		}

		/// <summary>
		/// Přihlašovací heslo.
		/// </summary>
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = nameof(Localization.Admin.Password), ResourceType = typeof(Localization.Admin))]
		public string Password
		{
			get;
			set;
		}

		/// <summary>
		/// Pamatuj si mě funkce.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.RememberMe), ResourceType = typeof(Localization.Admin))]
		public bool RememberMe
		{
			get;
			set;
		}

		//public async Task OnGetAsync(string returnUrl = null)
		//{
		//	if (!string.IsNullOrEmpty(ErrorMessage))
		//	{
		//		ModelState.AddModelError(string.Empty, ErrorMessage);
		//	}

		//	returnUrl = returnUrl ?? Url.Content("~/");

		//	// Clear the existing external cookie to ensure a clean login process
		//	await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

		//	ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

		//	ReturnUrl = returnUrl;
		//}

		//public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		//{
		//	returnUrl = returnUrl ?? Url.Content("~/");

		//	if (ModelState.IsValid)
		//	{
		//		// This doesn't count login failures towards account lockout
		//		// To enable password failures to trigger account lockout, set lockoutOnFailure: true
		//		var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
		//		if (result.Succeeded)
		//		{
		//			_logger.LogInformation("User logged in.");
		//			return LocalRedirect(returnUrl);
		//		}
		//		if (result.RequiresTwoFactor)
		//		{
		//			return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
		//		}
		//		if (result.IsLockedOut)
		//		{
		//			_logger.LogWarning("User account locked out.");
		//			return RedirectToPage("./Lockout");
		//		}
		//		else
		//		{
		//			ModelState.AddModelError(string.Empty, "Invalid login attempt.");
		//			return Page();
		//		}
		//	}

		//	// If we got this far, something failed, redisplay form
		//	return Page();
		//}
	}
}
