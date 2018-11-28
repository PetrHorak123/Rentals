using System;
using Rentals.DL.Entities;

namespace Rentals.DL.Interfaces
{
	public interface IRepositoriesFactory : IDisposable
	{
		IItemRepository Items { get; }

		IItemTypeRepository Types { get; }

		IRentalRepository Rentals { get; }

		IRentingRepository Rentings { get; }

		IRepository<Role> Roles { get; }

		IUserRepository Users { get; }

		int SaveChanges();
	}
}
