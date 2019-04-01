using Newtonsoft.Json;

namespace Rentals.Web.Models.Email
{
	/// <summary>
	/// Komu email přijde.
	/// </summary>
	public class ToRecipient
	{
		/// <summary>
		/// Emailová adresa cíle.
		/// </summary>
		[JsonProperty("emailAddress")]
		public EmailAddress EmailAddress
		{
			get;
			set;
		}
	}
}