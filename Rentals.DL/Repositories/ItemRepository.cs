using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System;
using System.Linq;

namespace Rentals.DL.Repositories
{
	internal class ItemRepository : BaseRepository<Item>, IItemRepository
	{
		public ItemRepository(EntitiesContext context) : base(context)
		{
		}

		public Item[] GetAvailbeItems(int itemTypeId, DateTime startsAt, DateTime endsAt)
		{
			var items = this.Context.Items
				.Where(i =>
					i.ItemTypeId == itemTypeId &&
					!i.IsDeleted &&
					!i.RentingToItems
						.Any(r =>
							(r.Renting.StartsAt >= startsAt && r.Renting.StartsAt <= endsAt) ||
							(r.Renting.EndsAt >= startsAt && r.Renting.EndsAt <= endsAt)
						))
				.ToArray();

			return items;
		}

		public Item[] GetByIds(int[] ids)
		{
			var items = this.Context.Items
				.Where(t => ids.Contains(t.Id))
				.ToArray();

			return items;
		}

		public Item GetByUniqueIdentifier(string identifier)
		{
			var item = this.Context.Items
				.FirstOrDefault(i => i.UniqueIdentifier == identifier);

			return item;
		}
	}
}
