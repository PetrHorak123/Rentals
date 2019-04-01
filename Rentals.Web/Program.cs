using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rentals.Web.Data;

namespace Rentals.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args);

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var serviceProvider = services.GetRequiredService<IServiceProvider>();
					var configuration = services.GetRequiredService<IConfiguration>();
					Seed.CreateData(serviceProvider, configuration).Wait();
				}
				catch (Exception exception)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(exception, "An error occurred while creating data");
				}
			}

			host.Run();
		}

		public static IWebHost CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}
