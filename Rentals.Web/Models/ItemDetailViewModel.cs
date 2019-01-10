using Rentals.DL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Rentals.Web.Models
{
	/// <summary>
	/// ViewModel pro detail předmětu.
	/// </summary>
	public class ItemDetailViewModel : BaseViewModel
	{
		public ItemDetailViewModel(Item item)
		{
			var type = item.Type;
			this.UniqueIdentifier = item.UniqueIdentifier;
			this.Id = item.Id;
			this.CoverImage = item.CoverImage;
			this.Note = item.Note;
			this.Description = type.Description;
			this.Name = type.Name;
			this.Accessories = type.ActualAccessories.Select(a => new AccessoryViewModel(a));
		}

		public ItemDetailViewModel(ItemType type)
		{
			this.Id = type.Id;
			this.Name = type.Name;
			this.CoverImage = type.ActualItems.FirstOrDefault()?.CoverImage;
			this.NumberOfItems = type.NonSpecificItems.Count;
			this.Description = type.Description;
			this.Accessories = type.ActualAccessories.Select(a => new AccessoryViewModel(a));
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

		/// <summary>
		/// Unikátní identifikátor předmětu, používá se pokud je zobrazován přímo předmět.
		/// </summary>
		public string UniqueIdentifier
		{
			get;
			set;
		}

		public string Note
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, zda je tento předmět unikátní tzn. není to typ.
		/// </summary>
		public bool IsSpecificItem => this.UniqueIdentifier != null;

		public IEnumerable<AccessoryViewModel> Accessories
		{
			get;
			set;
		}
	}
}