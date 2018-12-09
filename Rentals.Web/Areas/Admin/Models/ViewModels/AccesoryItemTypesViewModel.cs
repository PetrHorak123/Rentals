using System.Collections.Generic;
using Rentals.DL.Entities;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// View model s příslušenstvím k předmětu.
	/// </summary>
	public class AccesoryItemTypesViewModel : ItemTypesViewModel
	{
		public AccesoryItemTypesViewModel(IEnumerable<ItemType> types, int itemTypeId) : base(types)
		{
			this.ItemTypeId = itemTypeId;
		}

		/// <summary>
		/// Id předmětu, ke kterému se příslušenství vázě.
		/// </summary>
		public int ItemTypeId
		{
			get;
			set;
		}
	}
}
