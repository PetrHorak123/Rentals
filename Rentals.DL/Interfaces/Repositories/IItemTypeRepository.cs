using Rentals.DL.Entities;

namespace Rentals.DL.Interfaces
{
	public interface IItemTypeRepository : IRepository<ItemType>
	{
		/// <summary>
		/// Vrací všechny nesmazané typy předmětů.
		/// </summary>
		ItemType[] GetItemTypes();
	}
}
