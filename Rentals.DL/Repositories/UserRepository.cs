using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rentals.Common.Enums;
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

		public User GetByName(string name)
		{
			var user = this.Context.Users.Where(u => u.UserName == name).FirstOrDefault();

			return user;
		}

		public Task<User[]> GetUsersWithRolesAsync(int except = 0, params RoleType[] types)
		{
			var query = this.Context.Users
				.Where(u => 
					u.Roles
						.Select(r => r.Role.RoleType)
						.Any(r => types.Contains(r) && 
					u.Id != except)
				);

			return query.ToArrayAsync();
		}
	}
}
