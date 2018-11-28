using Rentals.DL.Entities;
using Rentals.DL.Interfaces;

namespace Rentals.Web.Models
{
	/// <summary>
	/// Model pro fetchování ostatních modelů.
	/// </summary>
	public class FetchViewModel : BaseViewModel
	{
		public FetchViewModel(User user, Rental rental)
		{
			this.User = user;
			this.Rental = rental;
		}

		/// <summary>
		/// Metoda pro plnění modelu základnímy daty.
		/// </summary>
		public T FetchModel<T>(T model, IRepositoriesFactory factory) where T : BaseViewModel
		{
			return FetchModelFromThis(model, factory);
		}
	}
}
