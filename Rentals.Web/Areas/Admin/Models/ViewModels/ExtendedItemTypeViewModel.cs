using System.Collections.Generic;
using System.Linq;
using Rentals.DL.Entities;
using Rentals.Web.Models;

namespace Rentals.Web.Areas.Admin.Models
{
	public class ExtendedItemTypeViewModel : BaseViewModel
	{
		public ExtendedItemTypeViewModel(ItemType type, Renting[] rentings)
		{
			this.Id = type.Id;
			this.Name = type.Name;
			this.Description = type.Description;
			this.Accessories = type.ActualAccessories
				.Select(t => new ItemTypeViewModel(t));
			this.Items = type.ActualItems
				.GroupBy(i => new
				{
					i.CoverImage,
					i.Note,
				})
				.Select(g => new ItemViewModel()
				{
					CoverImage = g.Key.CoverImage,
					Note = g.Key.Note,
					NumberOfItems = g.Count(),
				});
			this.History = type.ActualItems
				.SelectMany(t => t.History)
				.Select(h => new HistoryViewModel(h));

			//PH
			this.Rentings = rentings.Select(r => new RentingViewModel(r)).ToList();

		}

		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Název typu předmětu.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Popisek typu.
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Sgroupované předměty podle poznámky a obrázku.
		/// </summary>
		public IEnumerable<ItemViewModel> Items
		{
			get;
			set;
		}

		/// <summary>
		/// Příslušenství k tomuto předmětu.
		/// </summary>
		public IEnumerable<ItemTypeViewModel> Accessories
		{
			get;
			set;
		}

		/// <summary>
		/// Historie všech předmětů tohoto typu.
		/// </summary>
		public IEnumerable<HistoryViewModel> History
		{
			get;
			set;
		}


        //PH
        /// <summary>
        /// Historie všech výpujček tohoto typu.
        /// </summary>
        public IEnumerable<RentingViewModel> Rentings
		{
			get;
			set;
		}
}
}