using HP.ClearingCenter.Application.Security.Commands;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    public class SecureController : SiteController
    {
        public SecureController(IApplicationContext app) : base(app) { }

        public ActionResult SignIn()
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInCommand command)
        {
            var result = this.ProcessCommand(command);

            if (result.IsSuccessful())
            {
                return RedirectToAction("Index", "Home");
            }

            return View("SignIn");
        }

        public ActionResult SignOut()
        {
            this.Application.AuthenticationProvider.SignOut();
            return this.RedirectToAction("SignIn");
        }

    }
}
