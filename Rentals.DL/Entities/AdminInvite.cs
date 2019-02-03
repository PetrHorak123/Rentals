using System;
using System.ComponentModel.DataAnnotations;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Entities
{
	public partial class AdminInvite : IEntity
	{
		/// <summary>
		/// Vrací nebo nastavuje Id pozvánky.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, do kdy je pozvánka platná.
		/// </summary>
		public DateTime ExpiresAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, zda pozvánka už byla použita.
		/// </summary>
		public bool IsRedeemed
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, pro koho pozvánka je.
		/// </summary>
		[Required]
		public string ForUser
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, nebo nastavuje, zda-li po vyzvendutí pozvánky se z uživatele stane admin.
		/// </summary>
		public bool WillBeAdmin
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, nebo nastavuje, zda-li po vyzvendutí pozvánky se z uživatele stane zamšstnanec.
		/// </summary>
		public bool WillBeEmployee
		{
			get;
			set;
		}
	}
}
