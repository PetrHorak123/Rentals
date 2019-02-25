using Newtonsoft.Json;

namespace Rentals.Web.Models.Email
{
	/// <summary>
	/// Tělo emailu
	/// </summary>
	public class Body
	{
		/// <summary>
		/// Obsah Emailu.
		/// </summary>
		[JsonProperty("content")]
		public string Content
		{
			get;
			set;
		}

		/// <summary>
		/// Zatím jenom html.
		/// </summary>
		[JsonProperty("contentType")]
		public string ContentType => "Text";
	}
}