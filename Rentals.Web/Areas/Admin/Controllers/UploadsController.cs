using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Interfaces;

namespace Rentals.Web.Areas.Admin.Controllers
{
	public class UploadsController : AdminBaseController
	{
		private readonly IHostingEnvironment hostingEnvironment;

		public UploadsController(IRepositoriesFactory factory, IHostingEnvironment hostingEnvironment) : base(factory)
		{
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file)
		{
			if (!file.ContentType.Contains("image"))
				return null;

			var filePath = Path.GetTempFileName();

			var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
			var fullPath = Path.Combine(uploads, GetUniqueFileName(file.FileName));
			file.CopyTo(new FileStream(fullPath, FileMode.Create));

			var path = fullPath.Replace(hostingEnvironment.WebRootPath, string.Empty);

			return Content(path);
		}
		private string GetUniqueFileName(string fileName)
		{
			fileName = Path.GetFileName(fileName);
			return Path.GetFileNameWithoutExtension(fileName)
						+ "_"
						+ Guid.NewGuid().ToString().Substring(0, 4)
						+ Path.GetExtension(fileName);
		}
	}
}