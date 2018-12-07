using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rentals.DL.Entities;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Nedědí z <see cref="Web.Models.BaseViewModel"/>, prootže to nedává smysl.
	/// </summary>
	public class RentalEditorViewModel: IValidatableObject
	{
		public RentalEditorViewModel()
		{
		}

		public RentalEditorViewModel(Rental rental)
		{
			if (rental != null)
			{
				this.Name = rental.Name;
				this.StartsAt = rental.StartsAt;
				this.EndsAt = rental.EndsAt;
				this.MinTimeUnit = rental.MinTimeUnit;
				this.City = rental.City;
				this.Street = rental.Street;
				this.ZipCode = rental.ZipCode;
			}
		}

		/// <summary>
		/// Vrací nebo nastavuje název půjčovny.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.Rental_Name), ResourceType = typeof(Localization.Admin))]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název půjčovny.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.Rental_StartsAt), ResourceType = typeof(Localization.Admin))]
		public TimeSpan StartsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název půjčovny.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.Rental_EndsAt), ResourceType = typeof(Localization.Admin))]
		public TimeSpan EndsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název půjčovny.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.Rental_MinTimeUnit), ResourceType = typeof(Localization.Admin))]
		[Range(15, 60)]
		public int MinTimeUnit
		{
			get;
			set;
		}

		/// <summary>
		/// Adresa - Vrací nebo nastavuje město.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Rental_City), ResourceType = typeof(Localization.Admin))]
		public string City
		{
			get;
			set;
		}

		/// <summary>
		/// Adresa - Vrací nebo nastavuje ulici.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Rental_Street), ResourceType = typeof(Localization.Admin))]
		public string Street
		{
			get;
			set;
		}

		/// <summary>
		/// Adresa - Vrací nebo nastavuje poštovní směrovací číslo.
		/// </summary>
		[MaxLength(7)]
		[MinLength(6)] // předělat na validaci zip code
		[Display(Name = nameof(Localization.Admin.Rental_ZipCode), ResourceType = typeof(Localization.Admin))]
		public string ZipCode
		{
			get;
			set;
		}

		public Rental CreateEntity()
		{
			var rental = new Rental();

			return this.UpdateEntity(rental);
		}

		public Rental UpdateEntity(Rental rental)
		{
			rental.Name = this.Name;
			rental.StartsAt = this.StartsAt;
			rental.EndsAt = this.EndsAt;
			rental.MinTimeUnit = this.MinTimeUnit;
			rental.Street = this.Street;
			rental.City = this.City;
			rental.ZipCode = this.ZipCode;

			return rental;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (this.StartsAt >= this.EndsAt)
			{
				yield return new ValidationResult(Localization.Admin.Rental_OpeningHoursError, new[] { nameof(this.StartsAt), nameof(this.EndsAt) });
			}
		}
	}
}
