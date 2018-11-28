using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Uživatel aplikace.
	/// </summary>
	public class User : IdentityUser<int>, IEntity
	{
		public User()
		{
			this.Rentings = new HashSet<Renting>();
		}

		/// <summary>
		/// Vrací nebo nastavuje zákazníkovi zápůjčky.
		/// </summary>
		public virtual ICollection<Renting> Rentings
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, zda je zákazník označen jako smazaný (nemažu na "natvrdo", aby nevznikaly reference ukazující na nic).
		/// </summary>
		public bool IsDeleted
		{
			get;
			set;
		}
	}
}
