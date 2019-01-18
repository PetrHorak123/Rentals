using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Models;

namespace Rentals.Web.Controllers
{
	[Authorize(Policy = "BasicRights")]
	public class BaseController : Controller
	{
		private User currentUser;
		private FetchViewModel fetchViewModel;
		private Rental rental;

		public BaseController(IRepositoriesFactory factory)
		{
			this.RepositoriesFactory = factory;
		}

		protected User CurrentUser
		{
			get
			{
				if (this.currentUser == null && User.Identity.IsAuthenticated)
				{
					this.currentUser = this.RepositoriesFactory.Users.GetByName(User.Identity.Name);
				}

				return currentUser;
			}
		}

		protected Rental Rental
		{
			get
			{
				if (this.rental == null)
				{
					// Vezmu pouze první, v tabulce nikdy nebude více záznamů.
					this.rental = this.RepositoriesFactory.Rentals.GetFirst();
				}

				return rental;
			}
		}

		protected IRepositoriesFactory RepositoriesFactory
		{
			get;
			private set;
		}

		/// <summary>
		/// Vytvoří model, který dědí z <see cref="BaseViewModel"/> a naplní ho daty.
		/// </summary>
		public TModelType FetchModel<TModelType>() where TModelType : BaseViewModel, new ()
		{
			// Vytvořím model
			var result = new TModelType();

			// Objekt pro naplnění daty.
			this.fetchViewModel = this.fetchViewModel
				?? new FetchViewModel(this.CurrentUser, this.Rental);

			return this.fetchViewModel.FetchModel(result, this.RepositoriesFactory);
		}

		/// <summary>
		/// Vytvoří model, který dědí z <see cref="BaseViewModel"/> a naplní ho daty.
		/// </summary>
		public TModelType FetchModel<TModelType>(TModelType model) where TModelType : BaseViewModel
		{
			// Vytvořím model
			var result = model;

			// Objekt pro naplnění daty.
			this.fetchViewModel = this.fetchViewModel
				?? new FetchViewModel(this.CurrentUser, this.Rental);

			return this.fetchViewModel.FetchModel(result, this.RepositoriesFactory);
		}

		protected ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
	}
}