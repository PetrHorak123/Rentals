using Microsoft.EntityFrameworkCore;
using Rentals.Common.Enums;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System;
using System.Collections.Generic;
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

		public Task<Renting[]> GetNonRetruned()
		{
			var query = this.RentingNonReturnedQuery().ToArrayAsync();

			return query;
		}


		private IQueryable<Renting> RentingNonReturnedQuery()
		{
			var now = DateTime.Now;

			var query = this.Context.Rentings.Where(
				r => !r.IsCanceled &&
				r.EndsAt < now &&
				r.State == RentalState.Lended
			);

			return query;
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

		public Renting[] GetRentingsInTimeForItem(int itemId, DateTime from, DateTime to)
		{
			var query = this.RentingInTimeQuery(from, to)
				.Where(r => r.RentingToItems.Select(rti => rti.ItemId).Contains(itemId));

			return query.ToArray();
		}

		//PH
		/// <summary>
		/// Vrací všechny nezrušené výpůjčky. 
		/// </summary>
		public Renting[] GetRentingsForItems(IEnumerable<int> items)
		{
			var query = this.Context.Rentings
				.Where(r => r.RentingToItems.Select(rti => rti.ItemId).Any(i => items.Contains(i)) && r.State != 0)
				.OrderByDescending(r => r.EndsAt);

			return query.ToArray();
		}

		public Renting[] GetRentingsInTimeForItems(IEnumerable<int> items, DateTime from, DateTime to)
		{
			var query = this.RentingInTimeQuery(from, to)
				.Where(r => r.RentingToItems.Select(rti => rti.ItemId).Any(i => items.Contains(i)))
				.OrderBy(r => r.EndsAt);

			return query.ToArray();
		}

		public Task<Renting[]> GetRentingsEndingToday()
		{
			var query = this.Context.Rentings.Where(r => r.EndsAt.Date == DateTime.Today && !r.NotificationSent && !r.IsCanceled);

			return query.ToArrayAsync();
		}

		public Task<Renting[]> GetNonReturnedForUser(int userId)
		{
			var query = this.RentingNonReturnedQuery().Where(r => r.UserId == userId);

			return query.ToArrayAsync();
		}

		public Renting GetRentingByCancelationCode(string code)
		{
			var renting = this.Context.Rentings.Where(r => r.CancelationCode.Equals(code));

			return renting.FirstOrDefault();
		}
	}
}
