using Rentals.DL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Rentals.DL.Entities
{
	public partial class ItemType
	{
		/// <summary>
		/// Smaže (označí jako smazanou) entitu.
		/// </summary>
		public void Delete()
		{
			this.IsDeleted = true;

			foreach(var item in this.Items)
			{
				item.IsDeleted = true;
			}
		}

		/// <summary>
		/// Nesmazané fyzické předměty.
		/// </summary>
		[NotMapped]
		public ICollection<Item> ActualItems
		{
			get
			{
				return this.Items.Where(i => !i.IsDeleted).ToList();
			}
		}

		[NotMapped]
		public ICollection<Item> NonSpecificItems
		{
			get
			{
				return this.ActualItems
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
					.FirstOrDefault().Items
					.ToList();
			}
		}

		/// <summary>
		/// Nesmazané příslušenství.
		/// </summary>
		[NotMapped]
		public ICollection<ItemType> ActualAccessories
		{
			get
			{
				return this.Accessories.Select(a => a.Accesory).Where(a => !a.IsDeleted).ToList();
			}
		}

		public static ItemType CreateEntity(string name, string description, ICollection<Item> items, IRepositoriesFactory factory,
			Rental rental, IEnumerable<int> accesorries = null, IEnumerable<int> accessoryTo = null)
		{
			var type = new ItemType()
			{
				Name = name,
				Description = description,
				Items = items,
				Rental = rental
			};

			factory.Types.Add(type);
			factory.SaveChanges();

			// Přidám příslušesnství
			if (accesorries != null)
			{
				foreach (var i in accesorries)
				{
					var accessory = factory.Types.GetById(i);
					if (accessory == null)
						continue;

					factory.Accessories.Add(ItemTypeToItemType.Create(accessoryToId: type.Id, accessoryId: i));
				}

				factory.SaveChanges();
			}

			// Přidám příslušenství k.
			if (accessoryTo != null)
			{
				foreach (var i in accessoryTo)
				{
					var accessory = factory.Types.GetById(i);
					if (accessory == null)
						continue;

					factory.Accessories.Add(ItemTypeToItemType.Create(accessoryToId: accessory.Id, accessoryId: type.Id));
				}

				factory.SaveChanges();
			}

			return type;
		}

		public void UpdateEntity(string name, string description)
		{
			this.Name = name;
			this.Description = description;
		}
	}
}
