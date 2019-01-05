using Rentals.DL.Entities;
using System;
using System.Linq;

namespace Rentals.Web.Models
{
	public class ItemOverviewViewModel : BaseViewModel
	{
		public ItemOverviewViewModel(Item[] items)
		{
			Items = items.GroupBy(i => new
			{
				i.CoverImage,
				i.Note,
			}).Select(g => new ItemViewModel()
			{
				Name = g.First().Type.Name,
				UniqueId = g.First().UniqueIdentifier,
				CoverImage = g.Key.CoverImage,
				Description = g.First().Type.Description,
				NumberOfItems = g.Count(),
				Note = g.Key.Note
			}).ToArray();
		}

		public ItemOverviewViewModel(ItemType[] types)
		{
			Items = types.SelectMany(t => t.ActualItems.GroupBy(i => new
			{
				i.CoverImage,
				i.Note,
			}).Select(g => new ItemViewModel()
			{
				Name = g.First().Type.Name,
				UniqueId = g.First().UniqueIdentifier,
				CoverImage = g.Key.CoverImage,
				Description = g.First().Type.Description,
				NumberOfItems = g.Count(),
				Note = g.Key.Note
			})).ToArray();
		}

		/// <summary>
		/// Již roztřízené předměty, vhodné k zobrazení.
		/// </summary>
		public ItemViewModel[] Items
		{
			get;
			private set;
		}
	}
}
