using Rentals.DL.Entities;

namespace Rentals.DL.Interfaces
{
	public interface IAdminInviteRepository : IRepository<AdminInvite>
	{
		/// <summary>
		/// Vrací všechny aktivní pozvánky do aministrace.
		/// </summary>
		AdminInvite[] GetActiveInvites();
	}
}
