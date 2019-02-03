using Rentals.DL.Entities;

namespace Rentals.DL.Interfaces
{
	public interface IHistoryRepository : IRepository<History>
	{
		/// <summary>
		/// Vrací všechnu historii, která je svázaná s daným uživatelem.
		/// </summary>
		History[] GetHistoryForUser(int userId);
	}
}
