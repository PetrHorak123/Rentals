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
		/// Provider externí služby pro přihlašování.
		/// </summary>
		public string Provider
		{
			get;
			set;
		}

		/// <summary>
		/// Přihlašovací jméno.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.Login_UserName), ResourceType = typeof(Localization.Admin))]
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
		[Display(Name = nameof(Localization.Admin.Login_Password), ResourceType = typeof(Localization.Admin))]
		public string Password
		{
			get;
			set;
		}

		/// <summary>
		/// Pamatuj si mě funkce.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Login_RememberMe), ResourceType = typeof(Localization.Admin))]
		public bool RememberMe
		{
			get;
			set;
		}
	}
}
