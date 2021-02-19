﻿using Microsoft.EntityFrameworkCore;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Rentals.Common.Extensions;

namespace Rentals.DL.Repositories
{
	internal class ItemRepository : BaseRepository<Item>, IItemRepository
	{
		public ItemRepository(EntitiesContext context) : base(context)
		{
		}

		public Task<Item[]> GetAllAvaibleItemsAsync(DateTime from, DateTime to, string q = null)
		{
			var query = this.Context.Items
				.Where(i =>
					!i.IsDeleted &&
					!i.RentingToItems
						.Any(r =>
							(r.Renting.StartsAt >= from && r.Renting.StartsAt <= to) ||
							(r.Renting.EndsAt > from && r.Renting.EndsAt < to)
								
				)).OrderBy(x => x.UniqueIdentifier);

			if (!q.IsNullOrEmpty())
			{
				query = query.Where(i => i.Type.Name.Contains(q)).OrderBy(x => x.UniqueIdentifier);
			}

			return query.ToArrayAsync();
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
							(r.Renting.EndsAt > startsAt && r.Renting.EndsAt < endsAt)
						)).OrderBy(x => x.UniqueIdentifier)
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

		public Item GetByUniqueIdentifier(string identifier, bool withSpaces = true)
		{
			Item result;

			if (withSpaces)
			{
				result = this.Context.Items.FirstOrDefault(t => t.UniqueIdentifier == identifier);
			}
			else
			{
				result = this.Context.Items.FirstOrDefault(t => t.UniqueIdentifier.Replace(" ", string.Empty) == identifier && !t.IsDeleted);
			}

			return result;
		}

		public Item[] GetNonSpecificAvaibleItems(int itemTypeId, DateTime startsAt, DateTime endsAt)
		{
			var items = this.Context.Items
				.Where(i =>
					i.ItemTypeId == itemTypeId &&
					!i.IsDeleted &&
					!i.RentingToItems
						.Any(r =>
							(r.Renting.StartsAt >= startsAt && r.Renting.StartsAt <= endsAt) ||
							(r.Renting.EndsAt > startsAt && r.Renting.EndsAt < endsAt)
				))
				.GroupBy(i => new
				{
					i.CoverImage,
					i.Note,
				})
				.Select(g => new
				{
					Count = g.Count(),
					Items = g
				})
				.OrderByDescending(g => g.Count)
				.FirstOrDefault()?.Items
				.ToArray();

			if (items == null)
				return new Item[0];

			return items;
		}
	}
}
