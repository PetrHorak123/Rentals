using Rentals.Common.Enums;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Interfaces;
using Rentals.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Model, používaný pro editaci výpůjčky, jsou zde pouze vlastnosti, které dovoluji editovat.
	/// </summary>
	public class RentingEditorViewModel : BaseViewModel, IValidatableObject, IAfterFetchModel
	{
		public RentingEditorViewModel()
		{
		}

		public RentingEditorViewModel(Renting renting)
		{
			this.RentingId = renting.Id;
			this.EndsAt = renting.EndsAt;
			this.Note = renting.Note;
			this.State = renting.State;
		}

		/// <summary>
		/// Id výpůjčky.
		/// </summary>
		public int RentingId
		{
			get;
			set;
		}

		/// <summary>
		/// Id půjčovny.
		/// </summary>
		public int RentalId
		{
			get;
			set;
		}

		/// <summary>
		/// Čas kdy výpůjčka končí.
		/// </summary>
		[DataType(DataType.Time)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
		[Range(typeof(TimeSpan), "00:00", "23:59")]
		public TimeSpan EndsAtTime
		{
			get;
			set;
		}

		/// <summary>
		/// Datum kdy výpůjčka končí.
		/// </summary>
		public DateTime EndsAtDate
		{
			get;
			set;
		}

		/// <summary>
		/// Poznámka k výpůjčce.
		/// </summary>
		public string Note
		{
			get;
			set;
		}

		/// <summary>
		/// Stav rezervace.
		/// </summary>
		public RentalState State
		{
			get;
			set;
		}

		public DateTime EndsAt
		{
			get
			{
				return this.EndsAtDate.Add(this.EndsAtTime);
			}
			set
			{
				this.EndsAtTime = value.TimeOfDay;
				this.EndsAtDate = value.Date;
			}
		}

		public void UpdateEntity(Renting renting)
		{
			renting.EndsAt = this.EndsAt;
			renting.Note = this.Note;
			renting.State = this.State;
		}

		public void AfterFetchModel(IRepositoriesFactory repositoriesFactory)
		{
			this.RentalId = this.Rental.Id;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var factory = (IRepositoriesFactory)validationContext.GetService(typeof(IRepositoriesFactory));

			var renting = factory.Rentings.GetById(this.RentingId);
			var rental = factory.Rentals.GetById(this.RentalId);

			if (rental == null)
			{
				// Nenastane pokud někdo nezedituje kód stránky.
				yield return new ValidationResult(Localization.Admin.Renting_NoRental);
			}

			if (!rental.IsInWorkingHours(this.EndsAt))
			{
				yield return new ValidationResult(Localization.Admin.Renting_EndsAtNotInWorkingHours, new[] { nameof(this.EndsAtDate) });
			}

			if (this.EndsAt <= DateTime.Now)
			{
				yield return new ValidationResult(Localization.Admin.Renting_CannotBeSetInPast, new[] { nameof(this.EndsAtDate) });
			}

			if (renting != null)
			{
				foreach (var item in renting.Items)
				{
					if (!item.IsAvaible(renting.EndsAt, this.EndsAt))
					{
						yield return new ValidationResult(string.Format(Localization.Admin.Renting_ExtendUnavaible, item.UniqueIdentifier));
					}
				}
			}
			else
			{
				// Nenastane pokud někdo nezedituje kód stránky.
				yield return new ValidationResult(Localization.Admin.Renting_NoRenting);
			}
		}
	}
}
