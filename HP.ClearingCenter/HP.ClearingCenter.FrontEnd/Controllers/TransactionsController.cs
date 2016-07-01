using HP.ClearingCenter.Application.TransactionTransports.Commands;
using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.FrontEnd.Infrastructure.Web;
using HP.ClearingCenter.FrontEnd.Models.Transactions;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Controllers
{
    [Authorize]
    public class TransactionsController : SiteController
    {   
        public TransactionsController(IApplicationContext context) : base(context) {}

        #region search

        public ActionResult Index()
        {
            return this.Search(string.Empty);
        }

        public ActionResult Search(string id)
        {
            return View("Search", new TransactionsSearchViewModel(this.Application.Query, id).Initialize());
        }

        #endregion

        #region receiving

        [HttpGet]
        public ActionResult Receiving(string id)
        {
            var model = new ReceivingViewModel(this.Application.Query).Initialize(id);

            return model.TransactionTransport != null ? 
                (ActionResult)View(model) : this.HttpNotFound();
        }

        [NoCache]
        [HttpPost]        
        public ActionResult Receiving(ReceiveProductCommand command)
        {
            var result = this.ProcessCommand(command);

            return result.IsSuccessful() ?
                RedirectToAction("Index") :
                this.Receiving(command.TransportNumber);
        }

        #endregion

        #region clearing

        [HttpGet]
        public ActionResult Clearing(string id)
        {
            var model = new ClearingViewModel(this.Application.Query).Initialize(id);

            return model.TransactionTransport != null && model.TransactionTransport.IsClearingEnabled ?
                (ActionResult)View(model) : this.HttpNotFound();
        }

        [HttpPost]
        public ActionResult Clearing(ClearReceivedProductCommand command)
        {
            var result = this.ProcessCommand(command);

            if (result.IsSuccessful())
            {
                return RedirectToAction("Clearing");
            }

            return this.Clearing(command.TransportNumber);
        }

        [HttpPost]
        public ActionResult ValidateClearingProduct(ValidateClearingProductQuery query)
        {
            return Json(this.PerformQuery(query));
        }

        #endregion
    }
}
