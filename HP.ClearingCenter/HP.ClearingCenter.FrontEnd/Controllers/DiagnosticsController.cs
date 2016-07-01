using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    public class DiagnosticsController : SiteController
    {
        public DiagnosticsController(IApplicationContext app) : base(app) { }
        
        public ActionResult Content(bool? shortcutEditMode)
        {
            this.Application
                .Configuration
                .SessionData
                .IsShortcutEditModeEnabled = shortcutEditMode.GetValueOrDefault();

            return View();
        }
    }
}
