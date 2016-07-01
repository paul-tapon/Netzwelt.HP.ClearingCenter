using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HP.ClearingCenter.FrontEnd.Models
{
    public class SideNavigationModelFactory
    {
        private WebViewPage view;

        public SideNavigationModelFactory(WebViewPage view)
        {
            this.view = view;
        }

        public NavigationModel CreateInstance()
        {
            RouteValueDictionary routeValues = this.view.ViewContext.RouteData.Values;
            string currentController = routeValues["controller"].ToString();

            switch (currentController)
            {   
                default:
                    return new NavigationModel(string.Empty, this.view);
            }
        }
    }
}