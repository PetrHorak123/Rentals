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
		public int MinTimeUnit
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje začátek pracovní doby.
		/// </summary>
		[Required]
		public TimeSpan StartsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje konec pracovní doby.
		/// </summary>
		[Required]
		public TimeSpan EndsAt
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
