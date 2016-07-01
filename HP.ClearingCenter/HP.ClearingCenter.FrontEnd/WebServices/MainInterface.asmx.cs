using HP.ClearingCenter.Application.Api.Contracts;
using HP.ClearingCenter.Application.TransactionTransports.Commands;
using HP.ClearingCenter.Application.TransactionTransports.Queries;
using HP.ClearingCenter.FrontEnd.Infrastructure.Security;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace HP.ClearingCenter.FrontEnd.WebServices
{
    /// <summary>
    /// SOAP API for ClearingCenter
    /// </summary>
    [WebService(Namespace = "http://h41201.www4.hp.com/ClearingCenter.Web/WebServices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]    
    public class MainInterface : System.Web.Services.WebService
    {
        private IApplicationContext app;

        public MainInterface() : this(MvcApplication.ApplicationContextFactory()) {}

        public MainInterface(IApplicationContext application)
        {
            this.app = application;
        }

        public ApiAuthenticationToken AuthenticationToken { get; set; }

        [WebMethod]
        [SoapHeader("AuthenticationToken")]
        public ResponseResult AdviseClearingCenterProduct(AdviseClearingCenterProductRequest request)
        {
            var result = this.app.ExecuteCommand(new AdviseClearingCenterProductCommand
            {
                CreatedBy = this.AuthenticationToken.Username,
                CreatedAt = this.app.GetUtcNow(),
                Order = request.Order
            });

            return new ResponseResult { 
                ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToArray(),
                IsSuccessful = result.IsSuccessful()
            };
        }

        [WebMethod]
        [SoapHeader("AuthenticationToken")]
        public GetProductListResponse GetReceivedProducts(GetProductListRequest request)
        {
            return this.GetProductsReceived(request, true);
        }
        
        [WebMethod]
        [SoapHeader("AuthenticationToken")]
        public GetProductListResponse GetClearedProducts(GetProductListRequest request)
        {
            return this.GetProductsCleared(request, true);
        }
        
        [WebMethod]
        [SoapHeader("AuthenticationToken")]
        public ResponseResult ConfirmReceivedProductsReception(ConfirmProcessStatusReceptionRequest request)
        {
            var result = this.app.ExecuteCommand(new ConfirmReceivedProductsReceptionCommand
            {
                TransportIds = request.TransportIds,
                ReceivingConfirmationDate = DateTime.UtcNow,
                ReceivingConfirmationUsername = this.AuthenticationToken.Username
            });

            return new ResponseResult
            {
                ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToArray(),
                IsSuccessful = true
            };
        }

        [WebMethod]
        [SoapHeader("AuthenticationToken")]
        public ResponseResult ConfirmClearedProductsReception(ConfirmProcessStatusReceptionRequest request)
        {
            var result = this.app.ExecuteCommand(new ConfirmClearedProductsReceptionCommand
            {
                TransportIds = request.TransportIds,
                ClearingConfirmationDate = DateTime.UtcNow,
                ClearingConfirmationUsername = this.AuthenticationToken.Username
            });

            return new ResponseResult
            {
                ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToArray(),
                IsSuccessful = true
            };
        }

        [WebMethod]
        [SoapHeader("AuthenticationToken")]
        public ResponseResult Echo(EchoRequest request)
        {
            return new ResponseResult();
        }

        #region private methods

        private GetProductListResponse GetProductsReceived(GetProductListRequest request, bool isReceivingSuccessful)
        {
            var results = this.app.PerformQuery(new GetReceivedProductsQuery
            {
                MarketingProgramId = int.Parse(request.MarketingProgramId),
                CountryIsoCode = request.CountryIsoCode,
                StartDate = request.StartDate
            });

            return new GetProductListResponse
            {
                Transports = results
            };
        }

        private GetProductListResponse GetProductsCleared(GetProductListRequest request, bool isClearingSuccessful)
        {
            var results = this.app.PerformQuery(new GetClearedProductsQuery
            {
                MarketingProgramId = int.Parse(request.MarketingProgramId),
                CountryIsoCode = request.CountryIsoCode,
                StartDate = request.StartDate
            });

            return new GetProductListResponse
            {
                Transports = results
            };
        }

        #endregion
    }
}
