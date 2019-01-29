using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rentals.DL
{
	public class EntitiesContextDbFactory : IDesignTimeDbContextFactory<EntitiesContext>
	{
		public EntitiesContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<EntitiesContext>();
			builder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Integrated Security=True;");
			builder.UseLazyLoadingProxies();

			return new EntitiesContext(builder.Options);
		}
	}
}
