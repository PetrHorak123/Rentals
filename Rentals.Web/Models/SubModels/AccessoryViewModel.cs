using Rentals.DL.Entities;
using System.Linq;

namespace Rentals.Web.Models
{
	/// <summary>
	/// Příslušenství k předmětu.
	/// </summary>
	public class AccessoryViewModel
	{
		public AccessoryViewModel(ItemType type)
		{
			this.Name = type.Name;
			this.CoverImage = type.Items.FirstOrDefault()?.CoverImage;
			this.ItemTypeId = type.Id;
		}

		/// <summary>
		/// Název příslušenství.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Náhledový obrázek příslušenství.
		/// </summary>
		public string CoverImage
		{
			get;
			set;
		}

		/// <summary>
		/// Id typu předmětu (ItemType)
		/// </summary>
		public int ItemTypeId
		{
			get;
			set;
		}
	}
}
