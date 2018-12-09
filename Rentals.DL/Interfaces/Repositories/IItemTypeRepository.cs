using Rentals.DL.Entities;
using System.Threading.Tasks;

namespace Rentals.DL.Interfaces
{
	public interface IItemTypeRepository : IRepository<ItemType>
	{
		/// <summary>
		/// Vrací všechny nesmazané typy předmětů.
		/// </summary>
		ItemType[] GetItemTypes();

		/// <summary>
		/// Vrací příslušenství k předmětu.
		/// </summary>
		Task<ItemType[]> GetAccessoriesAsync(int id);

		/// <summary>
		/// Vrací všechy typy předmětů, které mužou být danému předmětu přiřazeny jako příslušenství.
		/// </summary>
		ItemType[] GetAvaibleAccessories(int id);
	}
}
