using System.Linq;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Repositories
{
	internal class HistoryRepository : BaseRepository<History>, IHistoryRepository
	{
		public HistoryRepository(EntitiesContext context) : base(context)
		{
		}

		public History[] GetHistoryForUser(int userId)
		{
			var query = this.Context.Histories.Where(h => h.Renting.UserId == userId);

			return query.ToArray();
		}
	}
}
