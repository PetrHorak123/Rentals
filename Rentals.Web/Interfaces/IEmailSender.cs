using System.Threading.Tasks;
using Rentals.Common.Enums;
using Rentals.DL.Entities;
using Rentals.Web.Models.Email;

namespace Rentals.Web.Interfaces
{
	/// <summary>
	/// Tasky musí být awaitované kvůli logování emailů.
	/// </summary>
	public interface IEmailSender
	{
		/// <summary>
		/// Pošle email o vytvoření výpůjčky.
		/// </summary>
		Task<bool> SendRentingCreated(Renting renting, string token, string cancelation);

		/// <summary>
		/// Pošle email o editaci výpůjčky.
		/// </summary>
		Task<bool> SendRentingEdited(Renting renting, string token);

		/// <summary>
		/// Pošle email o zrušení výpůjčky.
		/// </summary>
		Task<bool> SendRentingCanceled(Renting renting, User canceledBy, string token);

		/// <summary>
		/// POšle upozorňovací email.
		/// </summary>
		Task<bool> SendRentingReminder(string token);

		/// <summary>
		/// Pošle email u pozorňující na nenavrácenou výpůjčku.
		/// </summary>
		Task<bool> SendRentingNotReturned(string token);

		/// <summary>
		/// Odešle email.
		/// </summary>
		Task<bool> SendMail(Message email, string token, EmailType type);
	}
}
