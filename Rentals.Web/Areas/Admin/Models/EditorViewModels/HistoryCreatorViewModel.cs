using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// View Model pro vytváření záznamů do historie.
	/// </summary>
	public class HistoryCreatorViewModel : BaseViewModel, IValidatableObject
	{
		public HistoryCreatorViewModel()
		{
		}

		public HistoryCreatorViewModel(Renting renting)
		{
			this.RentingId = renting.Id;
			this.CustomerId = renting.UserId;
			this.ItemsHistory = renting.Items
				.Select(i => new HistoryCreatorSubViewModel(i))
				.ToArray();
		}

		public int RentingId
		{
			get;
			set;
		}

		public int CustomerId
		{
			get;
			set;
		}

		public HistoryCreatorSubViewModel[] ItemsHistory
		{
			get;
			set;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var factory = (IRepositoriesFactory)validationContext.GetService(typeof(IRepositoriesFactory));

			var renting = factory.Rentings.GetById(this.RentingId);

			if(this.CustomerId != renting.UserId)
			{
				yield return new ValidationResult(Localization.Admin.History_InvalidCustomer);
			}
		}
	}
}
