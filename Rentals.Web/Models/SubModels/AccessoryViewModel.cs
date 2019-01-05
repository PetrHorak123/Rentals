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
	}
}
