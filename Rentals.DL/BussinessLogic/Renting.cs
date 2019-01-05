using Rentals.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
		/// Všechny předměty, které tato výpůjčka obsahuje.
		/// </summary>
		[NotMapped]
		public Item[] Items
		{
			get
			{
				var items = this.RentingToItems.Select(r => r.Item).ToArray();
				return items;
			}
		}

		/// <summary>
		/// Vrací všechny předměty z výpůjčky, které náleží typu.
		/// </summary>
		public Item[] ItemsForType(int typeId)
		{
			return this.Items.Where(i => i.ItemTypeId == typeId).ToArray();
		}

		/// <summary>
		/// Vrací, zda je tato výpůjčka překrývá s jinou.
		/// </summary>
		public bool IsOverlapingWith(Renting renting)
		{
			bool overlap = this.StartsAt < renting.EndsAt && renting.StartsAt< this.EndsAt;

			return overlap;
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
