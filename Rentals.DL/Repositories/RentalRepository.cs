using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System.Linq;

namespace Rentals.DL.Repositories
{
	internal class RentalRepository : BaseRepository<Rental>, IRentalRepository
	{
		public RentalRepository(EntitiesContext context) : base(context)
		{
		}

		public Rental GetFirst()
		{
			var rental = this.Context.Rentals.FirstOrDefault();

			return rental;
		}
	}
}
