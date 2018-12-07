﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rentals.Common.Enums;
using Rentals.DL;
using Rentals.DL.Entities;

namespace Rentals.Web.Data
{
	public static class Seed
	{
		public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
		{
			// Přidání rolí
			var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

			foreach (var type in Enum.GetValues(typeof(RoleType)))
			{
				// Pokud role neexistuje, tzn. jedná se o první nasazení aplikace, role vytvořím
				var roleExist = await roleManager.RoleExistsAsync(type.ToString());
				if (!roleExist)
				{
					await roleManager.CreateAsync(new Role((RoleType) type, type.ToString()));
				}
			}

			var user = await userManager.FindByNameAsync(Configuration.GetSection("UserSettings")["UserName"]);
			
			// Pokud admin účet neexistuje, vytvořím ho
			if (user == null)
			{
				var poweruser = new User()
				{
					UserName = Configuration.GetSection("UserSettings")["UserName"]
				};

				string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];

				var createPowerUser = await userManager.CreateAsync(poweruser, userPassword);
				if (createPowerUser.Succeeded)
				{
					// Přidání Admin role
					await userManager.AddToRoleAsync(poweruser, RoleType.Administrator.ToString());
				}
			}

			using(var context = RepositoriesFactory.Create())
			{
				if(context.Rentals.GetFirst() == null)
				{
					context.Rentals.Add(new Rental()
					{
						Name = Configuration.GetSection("RentalSettings")["RentalName"],
						StartsAt = new TimeSpan(int.Parse(Configuration.GetSection("RentalSettings")["StartsAt"]), 0, 0),
						EndsAt = new TimeSpan(int.Parse(Configuration.GetSection("RentalSettings")["EndsAt"]), 0, 0),
						MinTimeUnit = int.Parse(Configuration.GetSection("RentalSettings")["MinTimeUnit"])
					});

					context.SaveChanges();
				}
			}
		}
	}
}