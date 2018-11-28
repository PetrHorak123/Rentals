using System.ComponentModel.DataAnnotations.Schema;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Vazební tabulka 
	/// </summary>
	public class RentingToItem : IEntity
	{
		// Vlastnosti jsou svázány v <see cref="DbContext.cs"/>

		public int RentingId
		{
			get;
			set;
		}

		public virtual Renting Renting
		{
			get;
			set;
		}

		public int ItemId
		{
			get;
			set;
		}
		
		public virtual Item Item
		{
			get;
			set;
		}
	}
}
