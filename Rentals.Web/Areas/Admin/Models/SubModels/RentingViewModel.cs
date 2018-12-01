using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rentals.DL.Entities;

namespace Rentals.Web.Areas.Admin.Models.SubModels
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
			this.Note = renting.Note;
			this.StartsAt = renting.StartsAt;
			this.EndsAt = renting.EndsAt;
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
		/// Zákazník, kterému byly(budou) předměty půjčeny.
		/// </summary>
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
		public string Note
		{
			get;
			set;
		}

		/// <summary>
		/// Začátek výpůjčky.
		/// </summary>
		public DateTime StartsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Konec výpůjčky.
		/// </summary>
		public DateTime EndsAt
		{
			get;
			set;
		}
	}
}
