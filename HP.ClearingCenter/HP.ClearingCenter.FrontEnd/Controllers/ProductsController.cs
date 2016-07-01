using HP.ClearingCenter.Infrastructure.Helpers;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using HP.ClearingCenter.FrontEnd.Models.Products;
using HP.ClearingCenter.Application.Products.Commands;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    [Authorize]
    public class ProductsController : SiteController
    {
        public ProductsController(IApplicationContext context) : base(context) { }

        public ActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public ActionResult Search(string term, int? page)
        {
            return View("Search", new ProductsSearchViewModel(this.Application).Initialize(term,page));
        }

        [HttpGet]
        public ActionResult SearchJson(string term)
        {
            //TODO: move this to an API controller
            var query = new SimpleSearchProductsQuery { Term = (term ?? string.Empty).Trim().ToUpperInvariant() };
            var results = this.Application
                .PerformQuery(query)
                .Select(x => new {
                    id = x.ToJson(),
                    value = string.Format("{0} {1} ({2})", x.Manufacturer, x.ProductName, x.ProductNumber)
                });

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = new ProductDetailsViewModel(this.Application).Initialize(id);
            if (model.IsProductFound)
            {
                return View("Details", model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Details(AddOrUpdateProductCommand command)
        {
            var result = this.ProcessCommand(command);
            return this.Details(command.ProductId);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new ProductDetailsViewModel(this.Application).Initialize(new AddOrUpdateProductCommand());
            return View("Details", model);
        }

        [HttpPost]
        public ActionResult Add(AddOrUpdateProductCommand command)
        {
            var result = this.ProcessCommand(command);
            if (result.IsSuccessful())
            {
                return this.RedirectToAction("Details", new { id = result.Response<int>() });
            }

            return View("Details",
                new ProductDetailsViewModel(this.Application).Initialize(command));
        }

        [HttpPost]
        public ActionResult CustomAttributes(SubmitProductAttributesCommand command) 
        {
            var result = this.ProcessCommand(command);
            return RedirectToAction("Details", new { id = command.ProductId });
        }
    }
}
