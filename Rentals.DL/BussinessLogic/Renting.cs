using Rentals.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals.DL.Entities
{
	public partial class Renting
	{
		/// <summary>
		/// Vrací, zda je výpůjčka zrušená.
		/// </summary>
		[NotMapped]
		public bool IsCanceled
		{
			get
			{
				bool result = this.State == RentalState.Canceled;
				return result;
			}
		}

		/// <summary>
		/// Vytvoří entitu, vnitřně nic nekontroluje, je potřeba zkontrolovat předem.
		/// </summary>
		public static Renting Create(int customerId, DateTime startsAt, DateTime endsAt, RentalState state, string note, int[] items)
		{
			var renting = new Renting()
			{
				UserId = customerId,
				StartsAt = startsAt,
				EndsAt = endsAt,
				State = state,
				Note = note,
			};

			// Přidám do výpůjčky předměty.
			foreach(var i in items)
			{
				renting.RentingToItems.Add(new RentingToItem()
				{
					ItemId = i
				});
			}

			return renting;
		}
	}
}
