using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Tabulka reprezenzující historii předmětu (co se mu kdy stalo).
	/// </summary>
	public class History
	{
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje co se stalo.
		/// </summary>
		[Required]
		public string Content
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje k jaké výpůjčce se záznam vztahuje.
		/// </summary>
		public int RentingId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje předmět, se kterým se něco stalo.
		/// </summary>
		public int ItemId
		{
			get;
			set;
		}

		[ForeignKey(nameof(RentingId))]
		public virtual Renting Renting
		{
			get;
			set;
		}

		[ForeignKey(nameof(ItemId))]
		public virtual Item Item
		{
			get;
			set;
		}
	}
}
