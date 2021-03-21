using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.ReactExtensions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rentals.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ReactController : BaseController
    {
        private readonly SignInManager<User> signInManager;

        public ReactController(IRepositoriesFactory factory, SignInManager<User> signInManageros) : base(factory)
        {
            this.signInManager = signInManageros;
        }

        [HttpGet("baf")]
        [AllowAnonymous]
        public ActionResult<string> EligibleUser()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

            var user = claims?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;
            //var user = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            return Ok(user);
        }

        [HttpGet("bafbaf")]
        public ActionResult<string> Test()
        {
            var user = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            return Ok("jetodobry");
        }

        [HttpGet("customers/{term}")]
        public JsonResult GetCustomers(string term)
        {
            if (term == null)
                return null;

            var customers = this.RepositoriesFactory.Users.FindCustomersByName(term)
                .Select(c => new CustomerSearchModel()
                {
                    Label = c.Name,
                    Value = c.Id,
                }).ToArray();

            return Json(customers);
        }

        //[HttpPost]
        //public async Task<ActionResult> Create(RentingCreatorViewModel postedModel)
        //{
        //    var model = FetchModel(postedModel);

        //    if (ModelState.IsValid)
        //    {
        //        var renting = await model.CreateEntity(userManager);
        //        this.RepositoriesFactory.Rentings.Add(renting);

        //        this.RepositoriesFactory.SaveChanges();

        //        await this.sender.SendRentingCreated(renting, this.MicrosoftAccessToken, Url.Action("CancelRenting", "Home", new { code = renting.CancelationCode }, HttpContext.Request.Scheme));

        //        return RedirectToAction("Index", "Calendar");
        //    }

        //    return View(model);
        //}
    }
}
