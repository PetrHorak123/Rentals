using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System;
using System.Linq;

namespace Rentals.DL.Repositories
{
	internal class RentingRepository : BaseRepository<Renting>, IRentingRepository
	{
		public RentingRepository(EntitiesContext context) : base(context)
		{
		}

		public Renting[] GetRentingInTime(DateTime from, DateTime to)
		{
			var rentings = this.Context.Rentings.Where(
				r => !r.IsCanceled &&
				(from <= r.EndsAt || from <= r.StartsAt) &&
				(to > r.EndsAt || to > r.StartsAt)
			)
			.ToArray();

			return rentings;
		}
	}
}
