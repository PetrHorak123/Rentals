using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rentals.Common.Enums;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;

namespace Rentals.Web.Data
{
	public static class SeedTestData
	{
		public static async Task Create(IRepositoriesFactory context, UserManager<User> userManager)
		{
			var customer = new User()
			{
				UserName = "josef@josef.cz",
				Name = "Novák Josef",
				Email = "josef@josef.cz",
				Class = "P-2015-2019(ma)",
			};

			var createPowerUser = await userManager.CreateAsync(customer);
			if (createPowerUser.Succeeded)
			{
				// Přidání Admin role
				await userManager.AddToRoleAsync(customer, RoleType.Customer.ToString());
			}

			var sony = ItemType.CreateEntity("Kamera sony",
				"Digitální fotoaparát - bezzrcadlovka, Full Frame, CMOS Exmor 24.3 Mpx",
				new List<Item>() { Item.CreateEntity("Kamera sony_1", ""), Item.CreateEntity("Kamera sony_2", "") }, context,
				context.Rentals.GetFirst());

			var objektiv = ItemType.CreateEntity("Objektiv",
				"FE 50mm f/1.8",
				new List<Item>() { Item.CreateEntity("Objektiv_1", ""), Item.CreateEntity("Objektiv_2", "") }, context,
				context.Rentals.GetFirst());

			var accessory = ItemTypeToItemType.Create(sony.Id, objektiv.Id);

			context.Accessories.Add(accessory);

			context.SaveChanges();
		}
	}
}
