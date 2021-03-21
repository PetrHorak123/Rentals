using Rentals.Common.Enums;
using Rentals.DL.Entities;
using System.Threading.Tasks;

namespace Rentals.DL.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		/// <summary>
		/// Vyhledává zákazníky podle úplné, ale i částečné shody s searchTerm.
		/// </summary>
		User[] FindCustomers(string searchTerm);

		User[] FindCustomersByName(string searchTerm);

		/// <summary>
		/// Vrací uživatele na základě jména.
		/// </summary>
		User GetByName(string name);

		/// <summary>
		/// Vrací uživatele, kteří patří do rolí.
		/// </summary>
		Task<User[]> GetUsersWithRolesAsync(int except = 0, params RoleType[] types);
	}
}
