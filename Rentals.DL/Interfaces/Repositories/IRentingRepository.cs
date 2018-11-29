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

		Task<Renting[]> GetRentingInTimeAsync(DateTime from, DateTime to);
	}
}
