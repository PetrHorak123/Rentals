using System.Linq;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;

namespace Rentals.DL.Repositories
{
	internal class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(EntitiesContext context) : base(context)
		{
		}

		public User[] FindCustomers(string searchTerm)
		{
			var customers = this.Context.Users
				.Where(u =>  
					u.NormalizedUserName.Contains(searchTerm.Normalize().ToUpper())
				).ToArray();

			return customers;
		}
	}
}
