using Rentals.DL.Entities;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Osekaný model pro zobrazení typu.
	/// </summary>
	public class ItemTypeViewModel
	{
		public ItemTypeViewModel()
		{
		}

		public ItemTypeViewModel(ItemType type)
		{
			this.Id = type.Id;
			this.Name = type.Name;
			this.CoverImage = type.ActualItems.FirstOrDefault()?.CoverImage;
			this.NumberOfItems = type.ActualItems.Count;
			this.Description = type.Description;
		}	

		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Náhledový obrázek předmětu.
		/// </summary>
		public string CoverImage
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
		/// Počet reálných předmětů v typu.
		/// </summary>
		public int NumberOfItems
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
	}
}