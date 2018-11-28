using Rentals.DL.Entities;
using Rentals.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// ViewModel pro zobrazení seznamu typů předmětů.
	/// </summary>
	public class ItemTypesViewModel : BaseViewModel
	{
		public ItemTypesViewModel(IEnumerable<ItemType> types)
		{
			this.ItemTypes = types.Select(t => new ItemTypeViewModel()
			{
				Id = t.Id,
				Name = t.Name,
				CoverImage = t.ActualItems.FirstOrDefault()?.CoverImage,
				NumberOfItems = t.ActualItems.Count,
				Description = t.Description,
			});
		}

		/// <summary>
		/// Všechny typy předmětů.
		/// </summary>
		public IEnumerable<ItemTypeViewModel> ItemTypes
		{
			get;
			private set;
		}
	}
}
