using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Rentals.Web.Models
{
	/// <summary>
	/// Login model pro uživatele, a. k. a. lidi co si chtěj něco půjčit.
	/// </summary>
	public class UserLoginViewModel
	{
		/// <summary>
		/// Externí služby pro přihlášení.
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
		/// Poskytovatel externího přihlášení.
		/// </summary>
		public string Provider
		{
			get;
			set;
		}
	}
}
