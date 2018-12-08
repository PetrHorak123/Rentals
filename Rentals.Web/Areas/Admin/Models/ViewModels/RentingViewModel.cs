using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Rentals.Common.Enums;
using Rentals.DL.Entities;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// ViewModel pro zobrazení výpůjčky.
	/// </summary>
	public class RentingViewModel
	{
		public RentingViewModel(Renting renting)
		{
			this.Id = renting.Id;
			this.CustomerName = renting.User.UserName;
			this.CustomerEmail = renting.User.Email;
			this.State = renting.State;
			this.Note = renting.Note;
			this.StartsAt = renting.StartsAt;
			this.EndsAt = renting.EndsAt;
			this.Items = renting.RentingToItems
				.Select(i => i.Item)
				.GroupBy(i => i.Type)
				.ToDictionary(
					k => new ItemTypeViewModel()
					{
						Id = k.Key.Id,
						Name = k.Key.Name
					},
					v => string.Join(", ", v.Select(i => i.UniqueIdentifier)).TrimEnd()
			);
		}

		/// <summary>
		/// Id výpůjčky.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Zákazník, kterému byly (budou) předměty půjčeny.
		/// </summary>
		[Display(Name = nameof(Localization.GlobalResources.Customer), ResourceType = typeof(Localization.GlobalResources))]
		public string CustomerName
		{
			get;
			set;
		}

		/// <summary>
		/// Zákazník, kterému byly(budou) předměty půjčeny.
		/// </summary>
		public string CustomerEmail
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
		/// Dictionary, kde klíč je typ předmětu a hodnota jsou jednotlivé předmety z typu.
		/// </summary>
		public Dictionary<ItemTypeViewModel, string> Items
		{
			get;
			set;
		}

		/// <summary>
		/// Udává, zda se mají vykreslit editační tlačítka.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Renting_State), ResourceType = typeof(Localization.Admin))]
		public RentalState State
		{
			get;
			set;
		}

		/// <summary>
		/// Začátek výpůjčky.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Renting_StartsAt), ResourceType = typeof(Localization.Admin))]
		public DateTime StartsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Konec výpůjčky.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Renting_EndsAt), ResourceType = typeof(Localization.Admin))]
		public DateTime EndsAt
		{
			get;
			set;
		}
	}
}
