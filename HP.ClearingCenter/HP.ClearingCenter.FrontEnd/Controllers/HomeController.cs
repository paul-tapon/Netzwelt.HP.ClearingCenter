using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // if user has access to agent processes, show that screen
            return this.RedirectToAction("Index", "Transactions");

            // else show the product database screen            
        }

    }
}
