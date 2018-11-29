using System.ComponentModel.DataAnnotations;
using Rentals.DL.Entities;
using Rentals.Web.Models;

namespace Rentals.Web.Areas.Admin.Models
{
	public class RentalEditorViewModel : BaseViewModel
	{
		public RentalEditorViewModel()
		{
		}

		public RentalEditorViewModel(Rental rental)
		{
			if (rental != null)
			{
				this.Name = rental.Name;
				this.City = rental.City;
				this.Street = rental.Street;
				this.ZipCode = rental.ZipCode;
			}
		}

		/// <summary>
		/// Vrací nebo nastavuje název půjčovny.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.Name), ResourceType = typeof(Localization.Admin))]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Adresa - Vrací nebo nastavuje město.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.City), ResourceType = typeof(Localization.Admin))]
		public string City
		{
			get;
			set;
		}

		/// <summary>
		/// Adresa - Vrací nebo nastavuje ulici.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Street), ResourceType = typeof(Localization.Admin))]
		public string Street
		{
			get;
			set;
		}

		/// <summary>
		/// Adresa - Vrací nebo nastavuje poštovní směrovací číslo.
		/// </summary>
		[MaxLength(7)]
		[MinLength(6)]
		[Display(Name = nameof(Localization.Admin.ZipCode), ResourceType = typeof(Localization.Admin))]
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
			rental.Street = this.Street;
			rental.City = this.City;
			rental.ZipCode = this.ZipCode;

			return rental;
		}
	}
}
