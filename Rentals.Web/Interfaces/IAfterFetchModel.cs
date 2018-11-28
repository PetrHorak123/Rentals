using Rentals.DL.Interfaces;

namespace Rentals.Web.Interfaces
{
	/// <summary>
	/// Interface označující modely, které potřebují dodatečně naplnit daty.
	/// </summary>
	public interface IAfterFetchModel
	{
		void AfterFetchModel(IRepositoriesFactory repositoriesFactory);
	}
}
