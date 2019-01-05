using Rentals.DL.Entities;
using System;
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
		/// Vrací všechny výpůjčky pro daný předmět (ne typ!!)
		/// </summary>
		Renting[] GetRentingsInTimeForItem(string item, DateTime from, DateTime to);

		/// <summary>
		/// Vrátí všechny předměty které jsou v daný čas vypůjčené.
		/// </summary>
		Task<Renting[]> GetRentingInTimeAsync(DateTime from, DateTime to);

		/// <summary>
		/// Vrací všenchy výpůjčky, které nebyly navráceny.
		/// </summary>
		Task<Renting[]> GetNonRetruned();
	}
}
