using Rentals.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Výpůjčka
	/// </summary>
	public partial class Renting : IEntity
	{
		public Renting()
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
		/// Vrací nebo nastavuje, kdy zapůjčení začíná.
		/// </summary>
		public DateTime StartsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, kdy zapůjčení začíná.
		/// </summary>
		public DateTime EndsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje poznámku k výpůjčce.
		/// </summary>
		public string Note
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje stav výpůjčky.
		/// </summary>
		public RentalState State
		{
			get;
			set;
		}

		/// <summary>
		/// Zákazník, který si předmět zapůjčil.
		/// </summary>
		public int UserId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, co bylo zapůjčeno za předměty.
		/// </summary>
		public virtual ICollection<RentingToItem> RentingToItems
		{
			get;
			set;
		}

		[ForeignKey(nameof(UserId))]
		public virtual User User
		{
			get;
			set;
		}
	}
}
