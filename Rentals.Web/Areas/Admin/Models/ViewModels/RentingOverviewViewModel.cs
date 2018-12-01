using Rentals.DL.Entities;
using Rentals.Web.Areas.Admin.Models.SubModels;
using Rentals.Web.Models;
using System.Collections.Generic;
using Rentals.Common.Enums;
using System;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// View model pro seznam dnešních výpůjček.
	/// </summary>
	public class RentingOverviewViewModel : BaseViewModel
	{
		public RentingOverviewViewModel(IEnumerable<Renting> rentings)
		{
			this.StartingToday = new List<RentingViewModel>();
			this.Returned = new List<RentingViewModel>();
			this.OnGoing = new List<RentingViewModel>();
			this.ShouldBeRetuned = new List<RentingViewModel>();
			this.ToBeRetuned = new List<RentingViewModel>();

			var now = DateTime.Now;
			var today = now.Date;

			foreach (var renting in rentings)
			{
				switch (renting.State)
				{
					case RentalState.NotLended:
						this.StartingToday.Add(new RentingViewModel(renting));
						break;
					case RentalState.Returned:
						this.Returned.Add(new RentingViewModel(renting));
						break;
					case RentalState.Lended:
						// Pokud nekončí dnes.
						if (renting.EndsAt > today)
						{
							this.OnGoing.Add(new RentingViewModel(renting));
							break;
						}
						// Pokud skončili v minulosti.
						else if (renting.EndsAt < now)
						{
							this.ShouldBeRetuned.Add(new RentingViewModel(renting));
							break;
						}
						else
						{
							this.ToBeRetuned.Add(new RentingViewModel(renting));
							break;
						}
				}
			}
		}

		/// <summary>
		/// Výpůjčky co dnes začínájí a předměty ještě nejsou vydány.
		/// </summary>
		public ICollection<RentingViewModel> StartingToday
		{
			get;
			set;
		}

		/// <summary>
		/// Výpůjčky, které mají být dnes vráceny.
		/// </summary>
		public ICollection<RentingViewModel> ToBeRetuned
		{
			get;
			set;
		}

		/// <summary>
		/// Výpůjčky, které měli být vráceny, ale ještě nejsou.
		/// </summary>
		public ICollection<RentingViewModel> ShouldBeRetuned
		{
			get;
			set;
		}

		/// <summary>
		/// Výpůjčky, které byly dnes vráceny.
		/// </summary>
		public ICollection<RentingViewModel> Returned
		{
			get;
			set;
		}

		/// <summary>
		/// Výpůjčky, které probíhají přes den.
		/// </summary>
		public ICollection<RentingViewModel> OnGoing
		{
			get;
			set;
		}
	}
}
