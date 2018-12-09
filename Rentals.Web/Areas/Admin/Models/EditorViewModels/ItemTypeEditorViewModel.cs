using Rentals.Common.Extensions;
using Rentals.DL.Entities;
using Rentals.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Rentals.DL.Interfaces;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Model pro editaci předmětů.
	/// </summary>
	public class ItemTypeEditorViewModel : BaseViewModel, IValidatableObject
	{
		public ItemTypeEditorViewModel()
		{
		}

		public ItemTypeEditorViewModel(ItemType type)
		{
			this.Name = type.Name;
			this.Description = type.Description;
			this.NumberOfItems = type.ActualItems.Count;
			this.CoverImage = type.ActualItems.FirstOrDefault()?.CoverImage;
			this.Id = type.Id;
			this.Items = type.ActualItems.Select(i =>
				new ItemEditorViewModel
				{
					Id = i.Id,
					UniqueIdentifier = i.UniqueIdentifier,
					CoverImage = i.CoverImage,
					Note = i.Note,
				}).ToArray();
		}

		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Název předmětu.
		/// </summary>
		[Required]
		[Display(Name = nameof(Localization.Admin.Item_Name), ResourceType = typeof(Localization.Admin))]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Počet předmětů, které tento typ obsahuje.
		/// </summary>
		[Range(1, 50)]
		[Required]
		[Display(Name = nameof(Localization.Admin.Item_NumberOfItemsLabel), ResourceType = typeof(Localization.Admin))]
		public int NumberOfItems
		{
			get;
			set;
		}

		/// <summary>
		/// Popisek tohoto typu předmětu.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Item_Description), ResourceType = typeof(Localization.Admin))]
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Obrázek, vázaný k těmto předmětům.
		/// </summary>
		/// <remarks>
		/// Je zde kvůli možnosti přidat/editovat obrázek u všech předmětů naráz.
		/// </remarks>
		[Display(Name = nameof(Localization.Admin.Item_CoverImage), ResourceType = typeof(Localization.Admin))]
		public string CoverImage
		{
			get;
			set;
		}

		/// <summary>
		/// Příslušenství k tomuto předmětu.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Item_Accessories), ResourceType = typeof(Localization.Admin))]
		public int[] Accessories
		{
			get;
			set;
		}

		/// <summary>
		/// Příslušenství k tomuto předmětu.
		/// </summary>
		[Display(Name = nameof(Localization.Admin.Item_AccessoryTo), ResourceType = typeof(Localization.Admin))]
		public int[] AccessoryTo
		{
			get;
			set;
		}

		/// <summary>
		/// Všechny předměty tohoto typu.
		/// </summary>
		public ItemEditorViewModel[] Items
		{
			get;
			set;
		}

		/// <summary>
		/// Vytvoří entitu typu, kterou je možné vložit do databáze.
		/// </summary>
		public ItemType CreateEntity(IRepositoriesFactory factory, Rental rental)
		{
			var type = ItemType.CreateEntity(this.Name, this.Description, this.CreateItems(), factory, rental, this.Accessories, this.AccessoryTo);

			return type;
		}

		/// <summary>
		/// Provede update entity.
		/// </summary>
		public void UpdateEntity(ItemType type)
		{
			type.UpdateEntity(this.Name, this.Description);

			for (int i = 0; i < type.ActualItems.Count; i++)
			{
				var currentItem = this.Items[i];

				type.ActualItems.ElementAt(i).UpdateEntity(currentItem.UniqueIdentifier, currentItem.Note, currentItem.CoverImage);
			}
		}

		/// <summary>
		/// Přidá do typu právě jeden item.
		/// </summary>
		public void AddItem(ItemType type, IRepositoriesFactory factory)
		{
			this.NumberOfItems = this.Items.Length + 1;

			// Přidám do typu prázdný přemět, o naplnění daty se postará metoda UpdateEntity.
			var item = new Item();

			type.Items.Add(item);

			var itemViewModel = new ItemEditorViewModel()
			{
				UniqueIdentifier = $"{this.Name}_{this.NumberOfItems}",
				CoverImage = this.CoverImage ?? this.Items[0].CoverImage,
			};

			this.Items = this.Items.Add(itemViewModel);

			this.UpdateEntity(type);
			factory.SaveChanges();

			// Nastavím Id u ViewModelu, aby nepadala validace.
			this.Items[this.NumberOfItems - 1].Id = item.Id;
		}

		/// <summary>
		/// Vytvoří z <see cref="Items"/> nové předměty, které je možné přidat do typu.
		/// </summary>
		/// <returns></returns>
		private ICollection<Item> CreateItems()
		{
			var items = new List<Item>();

			for (int i = 0; i < NumberOfItems; i++)
			{
				var item = Item.CreateEntity($"{this.Name}_{i + 1}", this.CoverImage);
				items.Add(item);
			}

			return items;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(this.Accessories != null && this.AccessoryTo != null)
			{
				var intersectingItems = this.Accessories.Intersect(this.AccessoryTo);

				// Nenastane, pokud někdo nezeditoval stránku, ale tak pro jistotu :).
				if(intersectingItems.Count() > 0)
				{
					yield return new ValidationResult(Localization.Admin.Item_DuplicateAccessory);
				}
			}

			if (this.Items != null)
			{
				var factory = (IRepositoriesFactory)validationContext.GetService(typeof(IRepositoriesFactory));
				var itemRepository = factory.Items;

				var groupedItems = this.Items.GroupBy(i => i.UniqueIdentifier).ToArray();

				if (groupedItems.Any(g => g.Count() > 1))
					yield return new ValidationResult(Localization.Admin.Item_SameUniqueIdentifiers1);

				foreach (var item in this.Items)
				{
					var identifiedItem = itemRepository.GetByUniqueIdentifier(item.UniqueIdentifier);

					if (identifiedItem == null || identifiedItem.Id == item.Id)
						continue;

					// Dám předmětu původní unikátní identifikátor.
					string oldIdentifier = item.UniqueIdentifier;
					item.UniqueIdentifier = itemRepository.GetById(item.Id).UniqueIdentifier;

					yield return new ValidationResult(string.Format(Localization.Admin.Item_SameUniqueIdentifiers2, oldIdentifier));
				}
			}
		}
	}
}
