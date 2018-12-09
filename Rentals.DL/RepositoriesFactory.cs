using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.DL.Repositories;

namespace Rentals.DL
{
	public class RepositoriesFactory : IRepositoriesFactory
	{
		private readonly EntitiesContext context;

		public RepositoriesFactory(EntitiesContext context)
		{
			this.context = context;

			Items = new ItemRepository(context);
			Types = new ItemTypeRepository(context);
			Rentals = new RentalRepository(context);
			Rentings = new RentingRepository(context);
			Roles = new BaseRepository<Role>(context);
			Users = new UserRepository(context);
			Accessories = new BaseRepository<ItemTypeToItemType>(context);
		}

		public IItemRepository Items { get; private set; }

		public IItemTypeRepository Types { get; private set; }

		public IRentalRepository Rentals { get; private set; }

		public IRentingRepository Rentings { get; private set;  }

		public IRepository<Role> Roles { get; private set; }

		public IUserRepository Users { get; private set; }

		public IRepository<ItemTypeToItemType> Accessories { get; private set; }

		public int SaveChanges()
		{
			return context.SaveChanges();
		}

		public void Dispose()
		{
			context.Dispose();
		}

		/// <summary>
		/// Vrací novou instanci typu RepositoriesFactory.
		/// </summary>
		public static RepositoriesFactory Create()
		{
			return new RepositoriesFactory(new EntitiesContext());
		}
	}
}
