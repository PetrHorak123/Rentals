using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rentals.DL.Entities;
using System.Collections.Generic;

namespace Rentals.DL
{
	public class EntitiesContext : IdentityDbContext<User, Role, int>
	{
		public EntitiesContext(DbContextOptions<EntitiesContext> options) : base(options)
		{
		}

		#region Tables

		// Role, Uživatelé a další tabulky, jsou již v Identity DbContextu, tudíž je zde nemusím registrovat.

		/// <summary>
		/// Vrací nebo nastavuje tabulku s půjčovnama.
		/// </summary>
		public DbSet<Rental> Rentals
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje tabulku s typama předmětů.
		/// </summary>
		public DbSet<ItemType> ItemTypes
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje tabulku s fyzickými předměty.
		/// </summary>
		public DbSet<Item> Items
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje tabulku s výpůjčkama.
		/// </summary>
		public DbSet<Renting> Rentings
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje vazební tabulku mezi výpůjčkou a předmětem.
		/// </summary>
		public DbSet<RentingToItem> RentingToItems
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje vázací tabulku mezi dvěmi typy. (příslušenství)
		/// </summary>
		public DbSet<ItemTypeToItemType> Accessories
		{
			get;
			set;
		}

		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Nastavení vázací tabulky.
			modelBuilder.Entity<RentingToItem>()
				.HasKey(rti => new { rti.RentingId, rti.ItemId });

			modelBuilder.Entity<RentingToItem>()
				.HasOne(rti => rti.Renting)
				.WithMany(r => r.RentingToItems)
				.HasForeignKey(rti => rti.RentingId);

			modelBuilder.Entity<RentingToItem>()
				.HasOne(rti => rti.Item)
				.WithMany(i => i.RentingToItems)
				.HasForeignKey(rti => rti.ItemId);

			// Nastavení vázací tabulky.
			modelBuilder.Entity<ItemTypeToItemType>()
				.HasKey(iti => new { iti.AccesoryId, iti.AccesoryToId });

			modelBuilder.Entity<ItemTypeToItemType>()
				.HasOne(a => a.Accesory)
				.WithMany(t => t.AccesoryTo)
				.HasForeignKey(a => a.AccesoryId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ItemTypeToItemType>()
				.HasOne(a => a.AccesoryTo)
				.WithMany(t => t.Accessories)
				.HasForeignKey(iti => iti.AccesoryToId)
				.OnDelete(DeleteBehavior.Restrict);

			// Unikátní identifikátor na předmětu.
			modelBuilder.Entity<Item>()
				.HasIndex(i => i.UniqueIdentifier)
				.IsUnique();

			// Konverze přímo do jsonu, abych měl jedoduché ukládání.
			modelBuilder.Entity<User>()
				.Property(b => b.Basket)
				.HasConversion(
					v => JsonConvert.SerializeObject(v),
					v => JsonConvert.DeserializeObject<Dictionary<string, int>>(v)
				);

			base.OnModelCreating(modelBuilder);
		}
	}
}
