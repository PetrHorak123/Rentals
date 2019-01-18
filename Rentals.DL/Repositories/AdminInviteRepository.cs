using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System;
using System.Linq;

namespace Rentals.DL.Repositories
{
	internal class AdminInviteRepository : BaseRepository<AdminInvite>, IAdminInviteRepository
	{
		public AdminInviteRepository(EntitiesContext context) : base(context)
		{
		}

		public AdminInvite[] GetActiveInvites()
		{
			var query = this.ActiveLinks;

			return query.ToArray();
		}

		public AdminInvite GetByUser(string userName)
		{
			var query = this.ActiveLinks.Where(i => i.ForUser == userName);

			return query.FirstOrDefault();
		}

		private IQueryable<AdminInvite> ActiveLinks => this.Context.AdminInvites.Where(i => !i.IsRedeemed && i.ExpiresAt > DateTime.Now);
	}
}
