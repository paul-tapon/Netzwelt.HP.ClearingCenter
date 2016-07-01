using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Entities;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.FrontEnd.Models.CustomAttributes;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    public class CustomAttributesController : SiteController
    {
        public CustomAttributesController(IApplicationContext app) : base(app) { }

        public ActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public ActionResult Search(string term,int? page)
        {
            return View("Search", 
                new CustomAttributesSearchViewModel(this.Application).Initialize(term,page));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Details", new CustomAttributeDetailsViewModel(this.Application).Initialize(new AddOrUpdateCustomAttributeCommand()));
        }

        [HttpPost]
        public ActionResult Add(AddOrUpdateCustomAttributeCommand command)
        {
            var result = this.ProcessCommand(command);
            if (result.IsSuccessful())
            {
                return RedirectToAction("Search", new { term = command.ExternalCode });
            }

            return View("Details", new CustomAttributeDetailsViewModel(this.Application)
                .Initialize(new AddOrUpdateCustomAttributeCommand()));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = new CustomAttributeDetailsViewModel(this.Application).Initialize(id);
            if (model.IsCustomAttributeFound)
            {
                return View("Details", model);
            }

            return this.HttpNotFound();
        }

        [HttpPost]
        public ActionResult Details(AddOrUpdateCustomAttributeCommand command)
        {
            var result = this.ProcessCommand(command);

            if (result.IsSuccessful()) {
                return RedirectToAction("Details", new { id = command.CustomAttributeId });
            }

            return View("Details", new CustomAttributeDetailsViewModel(this.Application).Initialize(command.CustomAttributeId));
        }

        [HttpGet]
        public ActionResult DetailsJson(int customAttributeId)
        {
            //TODO: Move this to an API Controller
            CustomAttributeData result = this.Application
                .PerformQuery(new GetCustomAttributeQuery { CustomAttributeId = customAttributeId })
                .FirstOrDefault();

            if (result == null) return HttpNotFound();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
