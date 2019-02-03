using Rentals.DL.Entities;
using System;

namespace Rentals.Web.Areas.Admin.Models
{
	/// <summary>
	/// Viewmodel pro zobrazení historie.
	/// </summary>
	public class HistoryViewModel
	{
		public HistoryViewModel(History history)
		{
			this.Content = history.Content;
			this.HappenedAt = history.Renting.EndsAt;
			this.Item = history.Item.UniqueIdentifier;
			this.ItemTypeId = history.Item.ItemTypeId;
			this.CausedBy = history.Renting.User.Name ?? history.Renting.User.UserName;
			this.CausedById = history.Renting.UserId;
		}

		/// <summary>
		/// Co se stalo.
		/// </summary>
		public string Content
		{
			get;
			set;
		}

		/// <summary>
		/// Kdy se to stalo (používá se čas navrácení)
		/// </summary>
		public DateTime HappenedAt
		{
			get;
			set;
		}

		/// <summary>
		/// Přesný předmět.
		/// </summary>
		public string Item
		{
			get;
			set;
		}

		/// <summary>
		/// Id typu, ze kterého je předmět.
		/// </summary>
		public int ItemTypeId
		{
			get;
			set;
		}

		/// <summary>
		/// KDo to zapříčinil.
		/// </summary>
		public string CausedBy
		{
			get;
			set;
		}

		/// <summary>
		/// Id zapříčinilete.
		/// </summary>
		public int CausedById
		{
			get;
			set;
		}
	}
}
