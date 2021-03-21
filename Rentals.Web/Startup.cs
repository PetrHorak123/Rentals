﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rentals.Common.Enums;
using Rentals.DL;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using System;
using Rentals.Web.Code;
using Rentals.Web.Interfaces;

namespace Rentals.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					builder =>
					{
						builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
					});
			});

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<EntitiesContext>(options =>
				options
					.UseLazyLoadingProxies()
					// .UseInMemoryDatabase()
					.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<EntitiesContext>()
				.AddDefaultTokenProviders();

			services.AddScoped<IRepositoriesFactory, RepositoriesFactory>();
			services.AddScoped<IEmailSender, EmailSender>();

			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

				options.LoginPath = "/Account/DecideLogin";
				options.AccessDeniedPath = "/Account/AccessDenied";
				options.SlidingExpiration = true;
			});

			services.AddSingleton<IAuthorizationHandler, DomainRequirementHandler>();

			services.AddAuthorization(options =>
			{
				options.AddPolicy("PslibOnly", policy => policy.AddRequirements(new DomainRequirement("pslib.cloud", "365.pslib.cz")));
				options.AddPolicy("AbsoluteRights", policy => policy.RequireRole(RoleType.Administrator.ToString()));
				options.AddPolicy("ElevatedRights", policy => policy.RequireRole(RoleType.Administrator.ToString(), RoleType.Employee.ToString()));
				options.AddPolicy("BasicRights", policy => policy.RequireRole(RoleType.Administrator.ToString(), RoleType.Employee.ToString(), RoleType.Customer.ToString()));
			});

			services.AddAuthentication()
			.AddMicrosoftAccount(microsoftOptions =>
			{
				microsoftOptions.ClientId = Configuration.GetSection("MicrosftSettings")["ClientId"];
				microsoftOptions.ClientSecret = Configuration.GetSection("MicrosftSettings")["ClientSecret"];
				microsoftOptions.SaveTokens = true;

				microsoftOptions.Scope.Add("people.read");
				microsoftOptions.Scope.Add("mail.send");
				microsoftOptions.Scope.Add("openid");
				microsoftOptions.Scope.Add("offline_access");
			});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				app.UseCors();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areaRoute",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

			});

			
		}
	}
}
