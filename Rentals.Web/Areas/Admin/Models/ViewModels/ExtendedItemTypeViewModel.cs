using System.Collections.Generic;
using System.Linq;
using Rentals.DL.Entities;
using Rentals.Web.Models;

namespace Rentals.Web.Areas.Admin.Models
{
	public class ExtendedItemTypeViewModel : BaseViewModel
	{
		public ExtendedItemTypeViewModel(ItemType type)
		{
			this.Id = type.Id;
			this.Name = type.Name;
			this.Description = type.Description;
			this.Items = type.ActualItems
				.GroupBy(i => new
				{
					i.CoverImage,
					i.Note,
				})
				.Select(g => new ItemViewModel()
				{
					CoverImage = g.Key.CoverImage,
					Note = g.Key.Note,
					NumberOfItems = g.Count(),
				}).ToArray();
		}

		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Název typu předmětu.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Popisek typu.
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Sgroupované předměty podle poznámky a obrázku.
		/// </summary>
		public IEnumerable<ItemViewModel> Items
		{
			get;
			set;
		}
	}
}
