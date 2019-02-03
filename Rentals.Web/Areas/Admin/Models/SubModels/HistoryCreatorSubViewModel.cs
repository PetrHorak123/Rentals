using Rentals.DL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Rentals.Web.Areas.Admin.Models
{
	public class HistoryCreatorSubViewModel
	{
		public HistoryCreatorSubViewModel()
		{
		}

		public HistoryCreatorSubViewModel(Item item)
		{
			this.Item = item.UniqueIdentifier;
			this.NewDescription = item.Note;
		}

		
		[Display(Name = nameof(Localization.Admin.History_Record), ResourceType = typeof(Localization.Admin))]
		public string Content
		{
			get;
			set;
		}

		public string Item
		{
			get;
			set;
		}

		[Display(Name = nameof(Localization.Admin.History_ChangeDescription), ResourceType = typeof(Localization.Admin))]
		public bool IsImportant
		{
			get;
			set;
		}

		[Display(Name = nameof(Localization.Admin.History_NewDescription), ResourceType = typeof(Localization.Admin))]
		public string NewDescription
		{
			get;
			set;
		}

		[Display(Name = nameof(Localization.Admin.History_NewRecord), ResourceType = typeof(Localization.Admin))]
		public bool AddToHistory
		{
			get;
			set;
		}
	}
}
