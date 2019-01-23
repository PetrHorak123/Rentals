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

			this.Items = new ItemRepository(context);
			this.Types = new ItemTypeRepository(context);
			this.Rentals = new RentalRepository(context);
			this.Rentings = new RentingRepository(context);
			this.Roles = new BaseRepository<Role>(context);
			this.Users = new UserRepository(context);
			this.Accessories = new BaseRepository<ItemTypeToItemType>(context);
			this.AdminInvites = new AdminInviteRepository(context);
			this.Histories = new BaseRepository<History>(context);
		}

		public IItemRepository Items { get; private set; }

		public IItemTypeRepository Types { get; private set; }

		public IRentalRepository Rentals { get; private set; }

		public IRentingRepository Rentings { get; private set;  }

		public IRepository<Role> Roles { get; private set; }

		public IUserRepository Users { get; private set; }

		public IRepository<ItemTypeToItemType> Accessories { get; private set; }

		public IAdminInviteRepository AdminInvites { get; private set; }

		public IRepository<History> Histories { get; private set; }

		public int SaveChanges()
		{
			return context.SaveChanges();
		}

		public void Dispose()
		{
			context.Dispose();
		}
	}
}
