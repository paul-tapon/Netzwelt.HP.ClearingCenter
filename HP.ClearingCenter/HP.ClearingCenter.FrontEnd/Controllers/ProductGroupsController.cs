using HP.ClearingCenter.Application.ProductGroups.Commands;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.FrontEnd.Models.ProductGroups;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    [Authorize]
    public class ProductGroupsController : SiteController
    {
        public ProductGroupsController(IApplicationContext app) : base(app) { }

        #region search

        public ActionResult Index()
        {
            return this.Search(null,1);
        }

        [HttpGet]
        public ActionResult Search(string term,int? page)
        {
            return View("Search", new ProductGroupsSearchViewModel(this.Application).Initialize(term,page));
        }

        #endregion

        #region Add/edit

        [HttpGet]
        public ActionResult Add()
        {
            return View("Details", 
                new ProductGroupDetailsViewModel(this.Application).Initialize(new AddProductGroupCommand()));
        }

        [HttpPost]
        public ActionResult Add(AddProductGroupCommand command)
        {
            command.ProductGroupId = null;
            command.IsActive = true;
            
            return this.Edit(command);
        }

        [HttpPost]
        public ActionResult Edit(AddProductGroupCommand command)
        {
            var result = this.ProcessCommand(command);

            if (result.IsSuccessful())
            {
                return command.ProductGroupId.HasValue ?
                    this.RedirectToAction("Details", new { id = command.ProductGroupId, externalCode = command.ExternalCode }) :
                    this.RedirectToAction("Index");
            }

            // show errors if any
            return View("Details",
                new ProductGroupDetailsViewModel(this.Application, isAddingNewGroup: true).Initialize(command));
        }

        #endregion

        [HttpGet]
        public ActionResult Details(int id, string externalCode)
        {
            var model = new ProductGroupDetailsViewModel(this.Application)
                .Initialize(new ProductGroupSearchQuery { Id = id, Term = externalCode });

            if (!model.IsProductGroupFound) return this.HttpNotFound();

            return View("Details", model);
        }

        [HttpPost]
        public ActionResult AddCategory(AddProductGroupFilterCategoryCommand command)
        {
            var result = this.ProcessCommand(command);
            return this.RedirectToAction("Details", new { id = command.ProductGroupId });
        }

        [HttpPost]
        public ActionResult RemoveCategory(RemoveCategoryCommand command)
        {
            this.ProcessCommand(command);
            return Json(true);
        }

        [HttpPost]
        public ActionResult AddCategoryAttributeFilter(AddCategoryAttributeFilterCommand command)
        {
            var result = this.ProcessCommand(command);
            return this.RedirectToAction("Details", new { id = command.ProductGroupId, externalCode = command.ProductGroupExternalCode });
        }

        [HttpPost]
        public ActionResult RemoveCategoryAttributeFilter(RemoveCategoryAttributeFilterCommand command)
        {
            var result = this.ProcessCommand(command);
            return this.RedirectToAction("Details", new { id = command.ProductGroupId, externalCode = command.ProductGroupExternalCode });
        }
    }
}
