using System.ComponentModel.DataAnnotations;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Podmodel pro editaci předmětů.
	/// </summary>
	public class ItemEditorViewModel
	{
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Unikátní identifikátor předmětu (napříč všemi).
		/// </summary>
		[Display(Name = nameof(Localization.Admin.UniqueIdentifier), ResourceType = typeof(Localization.Admin))]
		public string UniqueIdentifier
		{
			get;
			set;
		}

		/// <summary>
		/// Obrázek předmětu.
		/// </summary>
		public string CoverImage
		{
			get;
			set;
		}

		/// <summary>
		/// Poznámka k předmětu, např může být trošku rozbitý.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Note), ResourceType = typeof(Localization.Admin))]
		public string Note
		{
			get;
			set;
		}
	}
}
