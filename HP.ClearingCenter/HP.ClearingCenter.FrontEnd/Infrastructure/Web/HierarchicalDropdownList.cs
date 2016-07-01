using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    public class HierarchicalDropdownList
    {
        private string elementId;
        private string callbackFunctionName;
        private string captionText;        
        Func<HtmlString> buildItemList;

        public HierarchicalDropdownList() {}

        public HierarchicalDropdownList Id(string elementId)
        {
            this.elementId = elementId;
            return this;
        }

        public HierarchicalDropdownList SelectionCaption(string captionText)
        {
            this.captionText = captionText;
            return this;
        }

        public HierarchicalDropdownList OnSelectionChange(string jsFunctionName)
        {
            this.callbackFunctionName = jsFunctionName;
            return this;
        }

        public HierarchicalDropdownList DataSource<T>(IEnumerable<T> data, Func<T, dynamic> idMember, Func<T, dynamic> parentIdMember, Func<T, string> valueMember, Func<T, string> displayMember = null) where T : class
        {
            this.buildItemList = () => {
                StringBuilder html = new StringBuilder();

                var dm = displayMember ?? valueMember;
                this.BuildChildren<T>(html, data, idMember, parentIdMember, valueMember, dm, null);

                return new HtmlString(html.ToString());
            };

            return this;
        }

        private void BuildChildren<T>(StringBuilder html, IEnumerable<T> data, Func<T, dynamic> idMember, Func<T, dynamic> parentIdMember, Func<T, string> valueMember, Func<T, string> displayMember, dynamic parentId) where T : class
        {   
            var ulContent = new StringBuilder();

            // get parents            
            var parents = data.Where<T>(d => parentIdMember(d) == parentId);

            foreach (T item in parents)
            {
                StringBuilder liContent = new StringBuilder();
                
                TagBuilder a = new TagBuilder("a");
                a.Attributes["href"] = "javascript:void(0)";
                a.Attributes["data-value"] = valueMember(item);
                a.InnerHtml = displayMember(item);

                liContent.Append(a.ToString());
                this.BuildChildren<T>(liContent, data, idMember, parentIdMember, valueMember, displayMember, idMember(item));

                TagBuilder li = new TagBuilder("li");
                li.InnerHtml = liContent.ToString();

                ulContent.AppendLine(li.ToString());
            }

            if (ulContent.Length > 0)
            {
                var ul = new TagBuilder("ul");
                ul.InnerHtml = ulContent.ToString();
                html.AppendLine(ul.ToString());
            }
        }

        public HtmlString RenderHtml()
        {
            StringBuilder htmlMarkup = new StringBuilder();

            if (elementId.IsNullOrEmpty())
            {
                elementId = "hddl_{0}".WithTokens(Math.Abs(DateTime.UtcNow.GetHashCode()));
            }

            this.BuildTriggerLink(elementId, htmlMarkup);
            this.BuildItems(elementId, htmlMarkup);
            this.BuildInitializerScript(elementId, htmlMarkup);

            return new HtmlString(htmlMarkup.ToString());
        }

        private void BuildInitializerScript(string elementId, StringBuilder htmlMarkup)
        {
            TagBuilder script = new TagBuilder("script");
            script.Attributes["type"] = "text/javascript";

            string jsCode = "(function(){jQuery(document).ready(function(){var triggerId='#{{trigger}}';var contentId='#{{content}}';var jsCallback='{{callback}}';jQuery(triggerId).menu({width:300,content:jQuery(contentId).html(),callback:'onCategorySelected'})})})();"
                .Replace("{{trigger}}", elementId)
                .Replace("{{content}}", this.BuildItemElementId(elementId))
                .Replace("{{callback}}", this.callbackFunctionName);

            script.InnerHtml = jsCode;

            htmlMarkup.AppendLine(script.ToString());
        }

        private void BuildTriggerLink(string elementId, StringBuilder htmlMarkup)
        {
            TagBuilder link = new TagBuilder("a");
            link.Attributes["id"] = elementId;
            link.Attributes["class"] = "fg-button fg-button-icon-right ui-widget ui-state-default ui-corner-all";
            link.Attributes["href"] = "#" + this.BuildItemElementId(elementId);
            link.InnerHtml = this.captionText.IsNullOrEmpty() ? "Choose one..." : this.captionText;

            htmlMarkup.AppendLine(link.ToString());
        }

        private void BuildItems(string elementId, StringBuilder htmlMarkup)
        {
            TagBuilder containerDiv = new TagBuilder("div");
            containerDiv.Attributes["id"] = this.BuildItemElementId(elementId);
            containerDiv.Attributes["class"] = "hidden dropdownData";
            containerDiv.InnerHtml = this.buildItemList == null ? string.Empty : this.buildItemList().ToHtmlString();

            htmlMarkup.AppendLine(containerDiv.ToString());
        }

        private string BuildItemElementId(string elementId)
        {
            return elementId + "_items";
        }
    }
}