using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    [AllowAnonymous]
    public class ExportsController : SiteController
    {
        public ExportsController(IApplicationContext app) : base(app) { }

        public ActionResult ProductGroups(GetReturnObjectsByProductGroupCodeQuery query)
        {
            IList<ReturnObjectData> results = this.Application
                .PerformQuery(query) as IList<ReturnObjectData>;

            return Json(new { ItemCount = results.Count, Results = results }, JsonRequestBehavior.AllowGet);
        }
    }
}
