using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Rentals.Common.Enums;
using Rentals.DL;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Interfaces;
using Rentals.Web.Models;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Model pro práci s výpůjčkami.
	/// </summary>
	public class RentingEditorViewModel : BaseViewModel, IValidatableObject, IAfterFetchModel
	{
		public RentingEditorViewModel()
		{
		}

		/// <summary>
		/// Id zákazníka.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.Renting_Customer), ResourceType = typeof(Localization.Admin))]
		public int CustomerId
		{
			get;
			set;
		}

		/// <summary>
		/// Jméno zákazníka (pouze pro zobrazení do formuláře)
		/// </summary>
		public string CustomerName
		{
			get;
			set;
		}

		/// <summary>
		/// Začátek výpůjčky - datum.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[Display(Name = nameof(Localization.Admin.Renting_StartsAt), ResourceType = typeof(Localization.Admin))]
		public DateTime StartsAtDate
		{
			get;
			set;
		}

		/// <summary>
		/// Začátek výpůjčky - čas.
		/// </summary>
		[Required]
		[DataType(DataType.Time)]
		public TimeSpan StartsAtTime
		{
			get;
			set;
		}

		/// <summary>
		/// Začátek výpůjčky.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[Display(Name = nameof(Localization.Admin.Renting_EndsAt), ResourceType = typeof(Localization.Admin))]
		public DateTime EndsAtDate
		{
			get;
			set;
		}

		/// <summary>
		/// Začátek výpůjčky - čas.
		/// </summary>
		[Required]
		[DataType(DataType.Time)]
		public TimeSpan EndsAtTime
		{
			get;
			set;
		}

		/// <summary>
		/// Idčka Zapůjčených předmětů.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Renting_Items), ResourceType = typeof(Localization.Admin))]
		public int[] ItemIds
		{
			get;
			set;
		}

		/// <summary>
		/// Stav výpůjčky.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Renting_State), ResourceType = typeof(Localization.Admin))]
		public RentalState State
		{
			get;
			set;
		}

		/// <summary>
		/// Typy předmětů.
		/// </summary>
		public IEnumerable<ItemTypeViewModel> ItemTypes
		{
			get;
			set;
		}

		/// <summary>
		/// Předměty, které budou zapůjčeny.
		/// </summary>
		public ItemSearchModel[] Items
		{
			get;
			set;
		}

		/// <summary>
		/// Id půjčovny, ze které se půjčuje.
		/// </summary>
		public int RentalId
		{
			get;
			set;
		}

		private DateTime StartsAt => this.StartsAtDate.Add(this.StartsAtTime);

		private DateTime EndsAt => this.EndsAtDate.Add(this.EndsAtTime);

		public void AfterFetchModel(IRepositoriesFactory repositoriesFactory)
		{
			var today = DateTime.Today;

			this.StartsAtDate = today;
			this.EndsAtDate = today;
			this.RentalId = this.Rental.Id;

			this.ItemTypes = repositoriesFactory.Types.GetItemTypes()
				.Select(t => new ItemTypeViewModel()
				{
					Id = t.Id,
					Name = t.Name,
				});

			this.CustomerName = repositoriesFactory.Users.GetById(this.CustomerId)?.UserName;

			if (this.ItemIds != null)
			{
				this.ItemIds = this.ItemIds.Distinct().Where(i => i != 0).ToArray();

				this.Items = repositoriesFactory.Items.GetByIds(this.ItemIds)
					.Select(i => new ItemSearchModel(i)).ToArray();
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var context = RepositoriesFactory.Create();

			#region Customer

			var user = context.Users.GetById(this.CustomerId);

			if (user == null)
			{
				// Tato situace nenastane, pokud někdo nezedituje kód, tudíž vyhodím pouze obecnou validační zprávu.
				yield return new ValidationResult(Localization.Admin.Renting_NoCustomer);
			}

			#endregion

			#region Dates

			var rental = context.Rentals.GetById(this.RentalId);

			if(rental == null)
			{
				// Todle taky nenastane pokud někdo nezedituje kód stránky.
				yield return new ValidationResult(Localization.Admin.Renting_NoRental);
			}
			else if (!rental.IsInWorkingHours(this.StartsAt))
			{
				yield return new ValidationResult(Localization.Admin.Renting_StartsAtNotInWorkingHours);
			}
			else if (!rental.IsInWorkingHours(this.EndsAt))
			{
				yield return new ValidationResult(Localization.Admin.Renting_EndsAtNotInWorkingHours);
			}

			if (this.StartsAt >= this.EndsAt)
			{
				yield return new ValidationResult(Localization.Admin.Renting_WrongDate);
			}

			if (this.StartsAt <= DateTime.Now)
			{
				yield return new ValidationResult(Localization.Admin.Renting_DateInPast);
			}

			#endregion

			#region Items

			if (this.ItemIds != null)
			{
				foreach (var i in this.ItemIds)
				{
					var item = context.Items.GetById(i);

					if (item == null)
					{
						// Tato situace nenastane, pokud někdo nezedituje kód, tudíž vyhodím pouze obecnou validační zprávu.
						yield return new ValidationResult(Localization.Admin.Renting_ItemNotFound);
					}
					else if (!item.IsAvaible(this.StartsAt, this.EndsAt))
					{
						yield return new ValidationResult(string.Format(Localization.Admin.Renting_ItemUnavaible, item.UniqueIdentifier, this.StartsAt, this.EndsAt));
					}
				}
			}
			else
			{
				yield return new ValidationResult(Localization.Admin.Renting_NoItem);
			}

			#endregion

		}

		public Renting CreateEntity()
		{
			var renting = Renting.Create(
				this.CustomerId, this.StartsAt, this.EndsAt,
				this.State, this.ItemIds
			);

			return renting;
		}
	}
}
