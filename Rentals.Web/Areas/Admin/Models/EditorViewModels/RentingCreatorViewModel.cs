using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Rentals.Common.Enums;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Interfaces;
using Rentals.Web.Models;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Model pro práci s výpůjčkami.
	/// </summary>
	public class RentingCreatorViewModel : BaseViewModel, IValidatableObject, IAfterFetchModel
	{
		public RentingCreatorViewModel()
		{
			// Inicializační hodnoty, připravím si je tady, abych se o ně nemusel starat ve view.
			var today = DateTime.Today;

			this.StartsAtDate = today;
			this.EndsAtDate = today;
		}

		/// <summary>
		/// Id zákazníka.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.GlobalResources.Customer), ResourceType = typeof(Localization.GlobalResources))]
		public int CustomerId
		{
			get;
			set;
		}

		/// <summary>
		/// Jméno zákazníka (popřípadě email pokud se vytváří nový).
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
		[Range(typeof(TimeSpan), "00:00", "23:59")]
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
		[Range(typeof(TimeSpan), "00:00", "23:59")]
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
		/// Poznámka k výpůjčce.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Renting_Note), ResourceType = typeof(Localization.Admin))]
		public string Note
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
		/// <remarks>Je zde kvůli validaci, protože se volá před <see cref="IAfterFetchModel"/>.</remarks>
		public int RentalId
		{
			get;
			set;
		}

		private DateTime StartsAt => this.StartsAtDate.Add(this.StartsAtTime);

		private DateTime EndsAt => this.EndsAtDate.Add(this.EndsAtTime);

		public void AfterFetchModel(IRepositoriesFactory repositoriesFactory)
		{
			this.RentalId = this.Rental.Id;

			this.ItemTypes = repositoriesFactory.Types.GetItemTypes()
				.Select(t => new ItemTypeViewModel()
				{
					Id = t.Id,
					Name = t.Name,
				});

			this.CustomerName = repositoriesFactory.Users.GetById(this.CustomerId)?.UserName ?? this.CustomerName;

			if (this.ItemIds != null)
			{
				this.ItemIds = this.ItemIds.Distinct().Where(i => i != 0).ToArray();

				this.Items = repositoriesFactory.Items.GetByIds(this.ItemIds)
					.Select(i => new ItemSearchModel(i)).ToArray();
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var factory = (IRepositoriesFactory)validationContext.GetService(typeof(IRepositoriesFactory));

			#region Customer

			var user = factory.Users.GetById(this.CustomerId);

			if (user == null)
			{
				if (!Regex.Match(this.CustomerName, "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*").Success)
				{
					// Pokud nerozpoznám zákazníka, ať už existujíécího nebo novýho vyhodím hlášku.
					yield return new ValidationResult(Localization.Admin.Renting_NoCustomer, new[] { nameof(this.CustomerId) });
				}
			}

			#endregion

			#region Dates

			var rental = factory.Rentals.GetById(this.RentalId);

			if (rental == null)
			{
				// Todle taky nenastane pokud někdo nezedituje kód stránky.
				yield return new ValidationResult(Localization.Admin.Renting_NoRental);
			}
			else if (!rental.IsInWorkingHours(this.StartsAt))
			{
				yield return new ValidationResult(Localization.Admin.Renting_StartsAtNotInWorkingHours, new[] { nameof(this.StartsAtDate) });
			}
			else if (!rental.IsInWorkingHours(this.EndsAt))
			{
				yield return new ValidationResult(Localization.Admin.Renting_EndsAtNotInWorkingHours, new[] { nameof(this.EndsAtDate) });
			}

			if (this.StartsAt >= this.EndsAt)
			{
				yield return new ValidationResult(Localization.Admin.Renting_WrongDate, new[] { nameof(this.StartsAtDate) });
			}

			if (this.StartsAtDate < DateTime.Now.Date)
			{
				yield return new ValidationResult(Localization.Admin.Renting_DateInPast, new[] { nameof(this.StartsAtDate) });
			}

			if (this.EndsAt < DateTime.Now && this.State != RentalState.Returned)
			{
				yield return new ValidationResult(Localization.Admin.Renting_EndsInPast, new[] { nameof(this.EndsAtDate) });
			}

			#endregion

			#region Items

			if (this.ItemIds != null)
			{
				foreach (var i in this.ItemIds)
				{
					var item = factory.Items.GetById(i);

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

		public async Task<Renting> CreateEntity(UserManager<User> userManager)
		{
			if(this.CustomerId == 0)
			{
				// Uživatel není v databázi, vytvořím ho.
				var user = new User { UserName = this.CustomerName, Email = this.CustomerName };
				var userResult = await userManager.CreateAsync(user);
				if (userResult.Succeeded)
				{
					await userManager.AddToRoleAsync(user, RoleType.Customer.ToString());
				}

				this.CustomerId = user.Id;
			}

			var renting = Renting.Create(
				this.CustomerId, this.StartsAt, this.EndsAt,
				this.State, this.Note, this.ItemIds
			);

			return renting;
		}
	}
}
