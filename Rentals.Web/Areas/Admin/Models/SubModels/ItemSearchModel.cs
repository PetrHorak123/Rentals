using Rentals.DL.Entities;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Model pro vyhledávíní předmětů, vhodných k zapůjčení.
	/// </summary>
	public class ItemSearchModel
	{
		public ItemSearchModel(Item item)
		{
			this.Id = item.Id;
			this.UniqueIdentifier = item.UniqueIdentifier;
		}

		public int Id
		{
			get;
			set;
		}

		public string UniqueIdentifier
		{
			get;
			set;
		}
	}
}
