using Rentals.DL.Entities;
using Rentals.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Model úpužívaný pro tvoru invete linků do admina.
	/// </summary>
	public class AdminLinkEditorViewModel : BaseViewModel, IValidatableObject
	{
		public AdminLinkEditorViewModel()
		{
			this.ExpiresAt = DateTime.Today;
		}

		[Required]
		[Display(Name = nameof(Localization.Admin.Employees_LinkFor), ResourceType = typeof(Localization.Admin))]
		public string ForUser
		{
			get;
			set;
		}

		[Display(Name = nameof(Localization.Admin.Employees_WillBeAdmin), ResourceType = typeof(Localization.Admin))]
		public bool WillBeAdmin
		{
			get;
			set;
		}

		[Display(Name = nameof(Localization.Admin.Employees_WillBeEmployee), ResourceType = typeof(Localization.Admin))]
		public bool WillBeEmployee
		{
			get;
			set;
		}

		[Required]
		[Display(Name = nameof(Localization.Admin.Employees_LinkExpiration), ResourceType = typeof(Localization.Admin))]
		public DateTime ExpiresAt
		{
			get;
			set;
		}

		public AdminInvite Create()
		{
			var link = AdminInvite.CreateEntity(this.ForUser, 
				this.ExpiresAt, this.WillBeAdmin, this.WillBeEmployee);

			return link;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (!this.WillBeEmployee && !this.WillBeAdmin)
				yield return new ValidationResult(Localization.Admin.Employees_MustHaveRole);

			if (this.ExpiresAt < DateTime.Now)
				yield return new ValidationResult(Localization.Admin.Employees_CantBeInPast);
		}
	}
}
