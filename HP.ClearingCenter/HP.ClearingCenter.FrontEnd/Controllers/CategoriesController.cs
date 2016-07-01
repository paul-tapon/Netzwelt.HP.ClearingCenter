using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.FrontEnd.Models.Categories;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    public class CategoriesController : SiteController
    {
        public CategoriesController(IApplicationContext app) : base(app) { }

        public ActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public ActionResult Search(string term, int? page)
        {
            return View("Search", model: new CategoriesSearchViewModel(this.Application).Initialize(term,page));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = new CategoryDetailsViewModel(this.Application).Initialize(id);
            if (model.IsCategoryFound)
            {
                return View("Details", model);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Details(AddOrUpdateCategoryCommand command)
        {
            var result = this.ProcessCommand(command);
            return this.Details(command.CategoryId);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Details", 
                new CategoryDetailsViewModel(this.Application).Initialize(new AddOrUpdateCategoryCommand()));
        }
        
        [HttpPost]
        public ActionResult Add(AddOrUpdateCategoryCommand command)
        {
            var result = this.ProcessCommand(command);
            if (result.IsSuccessful())
            {
                return RedirectToAction("Search", new { term = command.ExternalCode });
            }
            
            return View("Details",
                new CategoryDetailsViewModel(this.Application).Initialize(command));
        }

        [HttpPost]
        public ActionResult AddAttribute(AddCategoryAttributeCommand command)
        {
            var result = this.ProcessCommand(command);
            return RedirectToAction("Details", new { id = command.CategoryId });
        }

        [HttpPost]
        public ActionResult RemoveAttribute(RemoveCategoryAttributeCommand command)
        {
            var result = this.ProcessCommand(command);
            return RedirectToAction("Details", new { id = command.CategoryId });
        }
    }
}
