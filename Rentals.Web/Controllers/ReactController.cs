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
        public async Task<ActionResult<string>> Test()
        {
            //var user = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

            var info = await signInManager.GetExternalLoginInfoAsync();

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            return Ok(email);
        }

        public ActionResult ToReact(User user)
        {
            var x = user;


            return Redirect("http://localhost:3000/Create"); ;
        }

        #region form

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

        [HttpGet("GetItemTypes")]
        public JsonResult GetItemTypes()
        {
            var itemTypes = this.RepositoriesFactory.Types.GetItemTypes();

            var results = new List<TreeSelectViewModel>();

            foreach (var item in itemTypes)
            {
                results.Add(new TreeSelectViewModel(
                    item.Name,
                    item.Id + 100000,
                    item.Id + 100000,
                    null,
                    false,
                    true,
                    false,
                    false
                ));
            }

            return Json(results);
        }

        [HttpGet("GetAvaibleItems")]
        public JsonResult GetAvaibleItems(int itemTypeId, string startsAt, string endsAt)
        {
            var itemType = RepositoriesFactory.Types.GetById(itemTypeId);

            var startsAtDateTime = DateTime.Parse(startsAt);
            var endsAtDateTime = DateTime.Parse(endsAt);

            if (startsAt == null || endsAt == null || itemType == null)
                return null;

            var items = this.RepositoriesFactory.Items
                .GetAvailbeItems(itemTypeId, startsAtDateTime, endsAtDateTime)
                .Select(i => new TreeSelectViewModel(
                    i.UniqueIdentifier,
                    i.Id,
                    i.Id,
                    null,
                    true,
                    false,
                    true,
                    true
                    ))
                .ToArray();

            return Json(items);
        }

        #endregion

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
