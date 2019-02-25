using Newtonsoft.Json;

namespace Rentals.Web.Models.Email
{
	/// <summary>
	/// Reprezentuje emailovou adresu.
	/// </summary>
	public class EmailAddress
	{
		/// <summary>
		/// Samotná adresa.
		/// </summary>
		[JsonProperty("address")]
		public string Address
		{
			get;
			set;
		}
	}
}
