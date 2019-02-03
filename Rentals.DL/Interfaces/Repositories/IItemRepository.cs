using Rentals.DL.Entities;
using System;
using System.Threading.Tasks;

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
		/// <param name="withSpaces">Pokud je nastaveno <code>true</code> beze v potaz i mezery, jinak je ignoruje.</param>
		Item GetByUniqueIdentifier(string identifier, bool withSpaces = true);

		/// <summary>
		/// Vrací předměty, které jsou dostupné k zapůjčení v daný časový rozsah.
		/// </summary>
		Item[] GetAvailbeItems(int itemTypeId, DateTime startsAt, DateTime endsAt);

		/// <summary>
		/// Vrací všechny předměty, které jsou dostupné mezi zadnými datumy.
		/// </summary>
		Task<Item[]> GetAllAvaibleItemsAsync(DateTime from, DateTime to, string q = null);

		/// <summary>
		/// Vrací předměty, které jsou dostupný v daný čas, a zároveň nejsou ničím odlišné od ostatních.
		/// </summary>
		Item[] GetNonSpecificAvaibleItems(int itemTypeId, DateTime startsAt, DateTime endsAt);
	}
}
