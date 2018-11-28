using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Rentals.DL.Entities
{
	public partial class ItemType
	{
		public static ItemType CreateEntity(string name, string description, ICollection<Item> items)
		{
			var type = new ItemType()
			{
				Name = name,
				Description = description,
				Items = items,
			};

			return type;
		}

		public void UpdateEntity(string name, string description)
		{
			this.Name = name;
			this.Description = description;
		}

		public void Delete()
		{
			this.IsDeleted = true;
		}

		[NotMapped]
		public ICollection<Item> ActualItems
		{
			get
			{
				return this.Items.Where(i => !i.IsDeleted).ToList();
			}
		}
	}
}
