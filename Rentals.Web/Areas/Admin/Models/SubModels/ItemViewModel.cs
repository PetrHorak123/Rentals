namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Viewmodel pro zobrazení předmětl, kde jsou předměty sgroupovány podle schody.
	/// </summary>
	public class ItemViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje unikátní identifikátory předmětů.
		/// </summary>
		public string[] UniqueIndetifiers
		{
			get;
			set;
		}

		/// <summary>
		/// Počet předmětů.
		/// </summary>
		public int NumberOfItems
		{
			get;
			set;
		}

		/// <summary>
		/// Náhledový obrázek.
		/// </summary>
		public string CoverImage
		{
			get;
			set;
		}

		/// <summary>
		/// Poznámka k předmětu.
		/// </summary>
		public string Note
		{
			get;
			set;
		}
	}
}
