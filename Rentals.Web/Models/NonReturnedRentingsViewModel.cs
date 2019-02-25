using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Rentals.Web.Models
{
	public class NonReturnedRentingsViewModel
	{
		public NonReturnedRentingsViewModel(ICollection<Renting> rentings, IRepositoriesFactory factory)
		{
			AreAny = rentings.Count != 0;

			if (AreAny)
			{
				var items = rentings.SelectMany(s => s.Items);

				Items = items.Select(i => i.Type.Name + "(" + i.UniqueIdentifier + ")");

				foreach(var renting in rentings)
				{
					if (renting.Items.Any(i => factory.Rentings.GetRentingsInTimeForItem(i.Id, renting.EndsAt, renting.EndsAt.AddDays(1)).Any(r => r.Id != renting.Id)))
					{
						this.AreRented = true;
					}
				}
			}
		}

		/// <summary>
		/// Zda jsou nenavrácené předměty.
		/// </summary>
		public bool AreAny
		{
			get;
			set;
		}

		/// <summary>
		/// Nenavrácené předměty.
		/// </summary>
		public IEnumerable<string> Items
		{
			get;
			set;
		}

		/// <summary>
		/// Zda jsou některé předměty vypůjčeny dál.
		/// </summary>
		public bool AreRented
		{
			get;
			set;
		}
	}
}
