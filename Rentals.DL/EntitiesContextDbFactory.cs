using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Rentals.DL
{
	public class EntitiesContextDbFactory : IDesignTimeDbContextFactory<EntitiesContext>
	{
		public EntitiesContext CreateDbContext(string[] args)
		{
			IConfigurationRoot conf = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<EntitiesContext>();
			builder.UseSqlServer(conf.GetConnectionString("DefaultConnection"));
			builder.UseLazyLoadingProxies();

			return new EntitiesContext(builder.Options);
		}
	}
}
