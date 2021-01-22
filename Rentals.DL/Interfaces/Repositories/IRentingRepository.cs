using Rentals.DL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rentals.DL.Interfaces
{
	public interface IRentingRepository : IRepository<Renting>
	{
		/// <summary>
		/// Vrátí všechny předměty které jsou v daný čas vypůjčené.
		/// </summary>
		Renting[] GetRentingInTime(DateTime from, DateTime to);

		/// <summary>
		/// Vrací všechny výpůjčky pro daný předmět (ne typ!!).
		/// </summary>
		Renting[] GetRentingsInTimeForItem(int itemId, DateTime from, DateTime to);

		/// <summary>
		/// Vrací všechny výpůjčky pro daný typ.
		/// </summary>
		Renting[] GetRentingsInTimeForItems(IEnumerable<int> items, DateTime from, DateTime to);

		//PH
		/// <summary>
		/// Vrací všechny výpůjčky pro daný typ předmětu nezávisle na čase.
		/// </summary>
		Renting[] GetRentingsForItems(IEnumerable<int> items);

		/// <summary>
		/// Vrátí všechny předměty které jsou v daný čas vypůjčené.
		/// </summary>
		Task<Renting[]> GetRentingInTimeAsync(DateTime from, DateTime to);

		/// <summary>
		/// Vrací všenchy výpůjčky, které nebyly navráceny.
		/// </summary>
		Task<Renting[]> GetNonRetruned();

		/// <summary>
		/// Vrací všechny výpůjčky, které nebyly navráceny daným uživatelem.
		/// </summary>
		Task<Renting[]> GetNonReturnedForUser(int userId);

		/// <summary>
		/// Vrací výpůjčky, které končí dnes.
		/// </summary>
		Task<Renting[]> GetRentingsEndingToday();

		/// <summary>
		/// Vrací výpůjčku podle jejího kódu pro zrušení.
		/// </summary>
		Renting GetRentingByCancelationCode(string code);
	}
}
