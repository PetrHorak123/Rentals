namespace Rentals.Web.Models
{
	/// <summary>
	/// Model představující jednu "dlaždici" na přehledu předmětů.
	/// </summary>
	public class ItemViewModel
	{
		/// <summary>
		/// Název předmětu.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Databázové Id předmětu (Item). 
		/// </summary>
		public int ItemId
		{
			get;
			set;
		}

		/// <summary>
		/// Databázové Id předmětu (ItemType). 
		/// </summary>
		public int ItemTypeId
		{
			get;
			set;
		}

		/// <summary>
		/// Počet přetmětů, které je možno si zapůjčit.
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
		/// Popisek.
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Zda je tento předmět specifický (tzn. je odlišný od ostatních předmětů stejného typu).
		/// </summary>
		public bool IsSpecificItem => this.NumberOfItems == 1;

		/// <summary>
		/// Poznámka k specifickýmu předmětu.
		/// </summary>
		public string Note
		{
			get;
			set;
		}

		/// <summary>
		/// Unikátní id k specifickýmu předmětu.
		/// </summary>
		public string UniqueId
		{
			get;
			set;
		}
	}
}
