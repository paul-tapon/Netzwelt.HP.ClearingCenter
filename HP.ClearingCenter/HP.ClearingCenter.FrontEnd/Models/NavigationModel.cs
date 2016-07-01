using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Models
{
    public class NavigationModel
    {   
        private List<NavigationItem> items;        

        public NavigationModel(string headerText, WebViewPage view)
        {
            this.HeaderText = headerText;
            this.View = view;
            this.items = new List<NavigationItem>();
        }

        public string HeaderText { get; private set; }

        protected WebViewPage View { get; set; }

        public virtual IEnumerable<NavigationItem> LinkItems
        {
            get
            {
                return this.items;
            }
        }

        public virtual NavigationModel AddNavItem(string linkText, string href, bool isActive = false)
        {   
            this.items.Add(new NavigationItem { DisplayText = linkText, LinkUrl = href });
            return this;
        }

        protected NavigationItem CreateNavigationItem(string headerText, string action, string controller, bool isSelected = false, object routeValues = null)
        {
            if (!isSelected)
            {
                string currentAction = this.View.ViewContext.RouteData.Values["action"].ToString();
                string currentController = this.View.ViewContext.RouteData.Values["controller"].ToString();

                isSelected = currentAction == action && currentController == controller;
            }

            return new NavigationItem
            {
                DisplayText = headerText,
                LinkUrl = this.View.Url.Action(action, controller, routeValues),
                IsActive = isSelected
            };
        }
    }
}