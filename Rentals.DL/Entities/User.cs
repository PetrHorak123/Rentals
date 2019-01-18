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
		private Dictionary<string, int> basket;

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
		/// Vrací nebo nastavuje role uživatele.
		/// </summary>
		public virtual ICollection<UserRole> Roles
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

		/// <summary>
		/// Vrací nebo nastavuje obsah košíku uživatele,
		/// klíčem je předmět který si chce vypůjčit, přičemž hodnota je počet,
		/// pokud hodnota je -1, znamená to, že uživatel si zažádel specifický předmět.
		/// Tato položka je convertována přímo entity frameworkem,
		/// proto je nutné při jakékoliv editaci zavolat <code>instance.Update()</code>.
		/// </summary>
		public Dictionary<string, int> Basket
		{
			get
			{
				if (this.basket == null)
				{
					this.basket = new Dictionary<string, int>();
				}

				return this.basket;
			}
			set
			{
				this.basket = value;
			}
		}
	}
}
