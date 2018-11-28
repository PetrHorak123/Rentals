using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Fyzický předmět, který se nachází v půjčovně.
	/// </summary>
	public partial class Item : IEntity
	{
		public Item()
		{
			this.RentingToItems = new HashSet<RentingToItem>();
		}

		[Key]
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje unikátní identifikátor tohoto předmětu, editovatelný.
		/// </summary>
		[Required]
		public string UniqueIdentifier
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje poznámku k tomuto předmětu.
		/// </summary>
		public string Note
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje cestu k náhledovému obrázku předmětu.
		/// </summary>
		public string CoverImage
		{
			get;
			set;
		}

		/// <summary>
		/// Označení přemětu jako smazaného.
		/// </summary>
		public bool IsDeleted
		{
			get;
			set;
		}

		/// <summary>
		/// Typ tohoto předmětu.
		/// </summary>
		public int ItemTypeId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, jací zákazníci si tento předmět půjčili.
		/// </summary>
		public virtual ICollection<RentingToItem> RentingToItems
		{
			get;
			set;
		}

		[ForeignKey(nameof(ItemTypeId))]
		public virtual ItemType Type
		{
			get;
			set;
		}
	}
}
