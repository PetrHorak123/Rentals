using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;
using Rentals.Web.Controllers;

namespace Rentals.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Policy = "ElevatedRights")]
	public class AdminBaseController : BaseController
	{
		public AdminBaseController(IRepositoriesFactory factory) : base(factory)
		{
		}
	}
}