using Rentals.DL.Entities;
using System;

namespace Rentals.DL.Interfaces
{
	public interface IItemRepository : IRepository<Item>
	{
		/// <summary>
		/// Vrací předměty na základě předaných idček
		/// </summary>
		Item[] GetByIds(int[] ids);

		/// <summary>
		/// Vrací předmět podle jeho unikátního identifikátoru, pokud ho nenajde vrací null.
		/// </summary>
		Item GetByUniqueIdentifier(string identifier);

		/// <summary>
		/// Vrací předměty, které jsou dostupné k zapůjčení v daný časový rozsah.
		/// </summary>
		Item[] GetAvailbeItems(int itemTypeId, DateTime startsAt, DateTime endsAt);
	}
}
