using Rentals.DL.Entities;
using System;

namespace Rentals.DL.Interfaces
{
	public interface IRentingRepository : IRepository<Renting>
	{
		/// <summary>
		/// Vrátí všechny předměty které jsou v daný čas vypůjčené.
		/// </summary>
		Renting[] GetRentingInTime(DateTime from, DateTime to);
	}
}
