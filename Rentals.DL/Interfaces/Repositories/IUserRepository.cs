using Rentals.DL.Entities;

namespace Rentals.DL.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		/// <summary>
		/// Vyhledává zákazníky podle úplné, ale i částečné shody s searchTerm.
		/// </summary>
		User[] FindCustomers(string searchTerm);

		/// <summary>
		/// Vrací uživatele na základě jména.
		/// </summary>
		User GetByName(string name);
	}
}
