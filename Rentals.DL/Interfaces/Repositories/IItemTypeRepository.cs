using Rentals.DL.Entities;
using System.Threading.Tasks;

namespace Rentals.DL.Interfaces
{
	public interface IItemTypeRepository : IRepository<ItemType>
	{
		/// <summary>
		/// Vrací všechny nesmazané typy předmětů.
		/// </summary>
		ItemType[] GetItemTypes(string q = null);

		Task<ItemType[]> GetItemTypesAsync(string q = null);

		/// <summary>
		/// Vrací typ předmětu podle jeho jména.
		/// </summary>
		/// <param name="withSpaces">Pokud je nastaveno <code>true</code> beze v potaz i mezery, jinak je ignoruje.</param>
		ItemType GetByName(string name, bool withSpaces = true);

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
