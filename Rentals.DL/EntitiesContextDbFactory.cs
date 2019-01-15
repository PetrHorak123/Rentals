using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rentals.DL
{
	public class EntitiesContextDbFactory : IDesignTimeDbContextFactory<EntitiesContext>
	{
		public EntitiesContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<EntitiesContext>();
			builder.UseSqlServer(@"Data Source=SQL6005.site4now.net;Initial Catalog=DB_A4489C_rentals;User Id=DB_A4489C_rentals_admin;Password=vsolstksqb2;");
			builder.UseLazyLoadingProxies();

			return new EntitiesContext(builder.Options);
		}
	}
}
