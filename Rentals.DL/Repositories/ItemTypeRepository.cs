using Microsoft.EntityFrameworkCore;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

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

		public Task<ItemType[]> GetAccessoriesAsync(int id)
		{
			var result = this.Context.Accessories
				.Where(a => a.AccesoryToId == id)
				.Select(at => at.Accesory)
				.Where(t => !t.IsDeleted)
				.ToArrayAsync();

			return result;
		}

		public ItemType[] GetAvaibleAccessories(int id)
		{
			var itemType = this.Context.ItemTypes.Find(id);

			var itemTypes = this.Context.ItemTypes.Where(
				t => !t.IsDeleted &&
				t.Id != id &&
				!itemType.Accessories.Select(a => a.AccesoryId).Contains(t.Id) &&
				!itemType.AccesoryTo.Select(a => a.AccesoryToId).Contains(t.Id)
			).ToArray();

			return itemTypes;
		}
	}
}
