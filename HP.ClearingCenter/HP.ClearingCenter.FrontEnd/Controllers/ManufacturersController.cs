using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.FrontEnd.Models.Manufacturers;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    public class ManufacturersController : SiteController
    {
        public ManufacturersController(IApplicationContext app) : base(app) { }

        public ActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public ActionResult Search(string term,int? page)
        {
            return View("Search", new ManufacturersSearchViewModel(this.Application).Initialize(term,page));
        }

        public ActionResult Details(int? id)
        {
            ManufacturerDetailsViewModel model = new ManufacturerDetailsViewModel(this.Application).Initialize(id);
            if (model.IsManufacturerFound || model.IsAddNew)
            {
                return View("Details", model);
            }

            return HttpNotFound();
        }
        
        [HttpPost]
        public ActionResult Details(AddOrUpdateManufacturerCommand command)
        {
            var result = this.ProcessCommand(command);
            if (result.IsSuccessful())
            {
                return RedirectToAction("Search", new { term = command.ExternalCode });
            }

            return this.Details(command.ManufacturerId);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Details", new ManufacturerDetailsViewModel(this.Application).Initialize(isAddNew: true));
        }

    }
}
