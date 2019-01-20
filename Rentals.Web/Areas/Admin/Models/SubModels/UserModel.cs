using Rentals.DL.Entities;
using Rentals.Web.Models;

namespace Rentals.Web.Areas.Admin.Models
{
	public class UserModel : BaseViewModel
	{
		public UserModel(User user)
		{
			this.Id = user.Id;
			this.Name = user.Name ?? user.UserName;
			this.Email = user.Email;
		}

		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}
	}
}
