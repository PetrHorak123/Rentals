using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Typ předmětu.
	/// </summary>
	public partial class ItemType : IEntity
	{
		public ItemType()
		{
			this.Items = new HashSet<Item>();
			this.Accessories = new HashSet<ItemTypeToItemType>();
			this.AccesoryTo = new HashSet<ItemTypeToItemType>();
		}

		[Key]
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název předmetů.
		/// </summary>
		[Required]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje popisek předmětů.
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Označení typu jako smazaného.
		/// </summary>
		public bool IsDeleted
		{
			get;
			set;
		}

		/// <summary>
		/// Půjčovna, ve které se tento typ předmětu půjčuje.
		/// </summary>
		public int RentalId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje všechny fyzické předměty tohoto typu
		/// </summary>
		public virtual ICollection<Item> Items
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje příslušenství k tomuto typu.
		/// </summary>
		public virtual ICollection<ItemTypeToItemType> Accessories
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, k čemu je tento typ příslušenství.
		/// </summary>
		public virtual ICollection<ItemTypeToItemType> AccesoryTo
		{
			get;
			set;
		}

		[ForeignKey(nameof(RentalId))]
		public virtual Rental Rental
		{
			get;
			set;
		}
	}
}
