using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rentals.DL
{
	public class EntitiesContextDbFactory : IDesignTimeDbContextFactory<EntitiesContext>
	{
		public EntitiesContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<EntitiesContext>();
			builder.UseSqlServer(@"Data Source=DESKTOP-D5HL9IN;Integrated Security=True;");
			builder.UseLazyLoadingProxies();

			return new EntitiesContext(builder.Options);
		}
	}
}
