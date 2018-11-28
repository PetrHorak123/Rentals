using Rentals.DL.Entities;

namespace Rentals.DL.Interfaces
{
	public interface IRentalRepository : IRepository<Rental>
	{
		/// <summary>
		/// Vrací první půjčovnu v databázi.
		/// </summary>
		Rental GetFirst();
	}
}
