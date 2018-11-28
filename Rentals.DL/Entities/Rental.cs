using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Půjčovna.
	/// </summary>
	public partial class Rental : IEntity
	{
		public Rental()
		{
			this.ItemTypes = new HashSet<ItemType>();
			this.MinTimeUnit = 30;
			this.WorksOnWeekend = false;
			this.StartsAt = new TimeSpan(8, 0, 0);
			this.EndsAt = new TimeSpan(15, 0, 0);
		}

		[Key]
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název půjčovny.
		/// </summary>
		[Required]
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje minimální časový úsek výpůjčky v minutách.
		/// </summary>
		[Required]
		[NotMapped]
		public int MinTimeUnit
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje začátek pracovní doby.
		/// </summary>
		[NotMapped]
		[Required]
		public TimeSpan StartsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje konec pracovní doby.
		/// </summary>
		[NotMapped]
		[Required]
		public TimeSpan EndsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, zda-li je otevřeno i přes víkend.
		/// </summary>
		[NotMapped]
		public bool WorksOnWeekend
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje všechny typy předmětů.
		/// </summary>
		public virtual ICollection<ItemType> ItemTypes
		{
			get;
			set;
		}

		#region Adresa

		/// <summary>
		/// Adresa - Vrací nebo nastavuje město.
		/// </summary>
		public string City
		{
			get;
			set;
		}

		/// <summary>
		/// Adresa - Vrací nebo nastavuje ulici.
		/// </summary>
		public string Street
		{
			get;
			set;
		}

		/// <summary>
		/// Adresa - Vrací nebo nastavuje poštovní směrovací číslo.
		/// </summary>
		public string ZipCode
		{
			get;
			set;
		}

		#endregion
	}
}
