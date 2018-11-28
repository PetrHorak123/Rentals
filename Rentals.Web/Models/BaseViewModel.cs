using Rentals.DL;
using Rentals.DL.Entities;
using Rentals.Web.Interfaces;
using System;
using Newtonsoft.Json;
using Rentals.DL.Interfaces;

namespace Rentals.Web.Models
{
	public class BaseViewModel
	{
		[JsonIgnore]
		public User User
		{
			get;
			set;
		}

		[JsonIgnore]
		public Rental Rental
		{
			get;
			set;
		}

		/// <summary>
		/// Fetchne model z tohoto modelu.
		/// </summary>
		protected virtual T FetchModelFromThis<T>(T model, IRepositoriesFactory repositoriesFactory) where T : BaseViewModel
		{
			if (model == null)
				throw new InvalidOperationException("This can be called only on non null models");

			// Naplním důležitými daty.
			model.User = this.User;
			model.Rental = this.Rental;

			// Pokud model vyžaduje speciální plnění, učiním to.
			var afterFetchModel = model as IAfterFetchModel;
			if (afterFetchModel != null)
			{
				repositoriesFactory = repositoriesFactory ?? RepositoriesFactory.Create();
				afterFetchModel.AfterFetchModel(repositoriesFactory);
			}

			return model;
		}
	}
}
