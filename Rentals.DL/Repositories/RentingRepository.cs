using Microsoft.EntityFrameworkCore;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.DL.Repositories
{
	internal class RentingRepository : BaseRepository<Renting>, IRentingRepository
	{
		public RentingRepository(EntitiesContext context) : base(context)
		{
		}

		public Renting[] GetRentingInTime(DateTime from, DateTime to)
		{
			var rentings = RentingInTimeQuery(from, to).ToArray();

			return rentings;
		}

		public Task<Renting[]> GetRentingInTimeAsync(DateTime from, DateTime to)
		{
			var renting = RentingInTimeQuery(from, to).ToArrayAsync();

			return renting;
		}

		/// <summary>
		/// Vytvoří pouze query pro databázi, které následně metody pouštějí.
		/// </summary>
		private IQueryable<Renting> RentingInTimeQuery(DateTime from, DateTime to)
		{
			var query = this.Context.Rentings.Where(
				r => !r.IsCanceled &&
				(from <= r.EndsAt || from <= r.StartsAt) &&
				(to > r.EndsAt || to > r.StartsAt)
			);

			return query;
		}
	}
}
