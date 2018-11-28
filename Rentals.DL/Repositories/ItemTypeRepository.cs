using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System.Linq;

namespace Rentals.DL.Repositories
{
	internal class ItemTypeRepository : BaseRepository<ItemType>, IItemTypeRepository
	{
		public ItemTypeRepository(EntitiesContext context) : base(context)
		{
		}

		public ItemType[] GetItemTypes()
		{
			var result = this.Context.ItemTypes
				.Where(t => !t.IsDeleted)
				.ToArray();

			return result;
		}
	}
}
