using System;
using System.Linq;

namespace Rentals.DL.Entities
{
	public partial class Item
	{
		public void UpdateEntity(string identifier, string note, string coverImage)
		{
			this.UniqueIdentifier = identifier;
			this.Note = note;
			this.CoverImage = coverImage;
		}

		public static Item CreateEntity(string identifier, string coverImage)
		{
			var item = new Item()
			{
				UniqueIdentifier = identifier,
				CoverImage = coverImage,
			};

			return item;
		}

		public void Delete()
		{
			this.IsDeleted = true;
		}

		public bool IsAvaible(DateTime from, DateTime to)
		{
			bool result = this.RentingToItems
				.Select(r => r.Renting)
				.Any(r =>
					!r.IsCanceled &&
					(r.StartsAt >= from && r.StartsAt <= to) || // začíná v rozmezí from - to
					(r.EndsAt >= from && r.EndsAt <= to) // končí v rozmezí from - to
				);

			return !result;
		}
	}
}
