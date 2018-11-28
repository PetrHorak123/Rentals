namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Model pro vyhledávání zákazníků.
	/// </summary>
	public class CustomerSearchModel
	{
		/// <summary>
		/// Id zákazníka.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Jméno zákazníka.
		/// </summary>
		public string Name
		{
			get;
			set;
		}
	}
}
