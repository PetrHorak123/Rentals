using Microsoft.EntityFrameworkCore;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Rentals.Common.Extensions;

namespace Rentals.DL.Repositories
{
	internal class ItemTypeRepository : BaseRepository<ItemType>, IItemTypeRepository
	{
		public ItemTypeRepository(EntitiesContext context) : base(context)
		{
		}

		public ItemType GetByName(string name, bool withSpaces = true)
		{
			ItemType result;
			var query = this.Context.ItemTypes.Where(t => !t.IsDeleted);

			if (withSpaces)
			{
				result = query.FirstOrDefault(t => t.Name == name);
			}
			else
			{
				result = query.FirstOrDefault(t => t.Name.Replace(" ", string.Empty) == name);
			}

			return result;
		}

		public ItemType[] GetItemTypes(string q = null)
		{
			return GetItemsQuery(q).ToArray();
		}

		public Task<ItemType[]> GetItemTypesAsync(string q = null)
		{
			return GetItemsQuery(q).ToArrayAsync();
		}

		private IQueryable<ItemType> GetItemsQuery(string q)
		{
			var query = this.Context.ItemTypes
				.Where(t => !t.IsDeleted);

			if (!q.IsNullOrEmpty())
			{
				query = query.Where(t => t.Name.Contains(q));
			}

			return query;
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
				!itemType.Accessories.Select(a => a.AccesoryId).Contains(t.Id) 
				// && !itemType.AccesoryTo.Select(a => a.AccesoryToId).Contains(t.Id)
			).ToArray();

			return itemTypes;
		}
	}
}
