using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Models.Categories
{
    public class CategorySelectListBuilder
    {
        private int? selectedValue;
        private IEnumerable<CategoryData> allCategories;
        private bool addEmptyItem;

        public CategorySelectListBuilder(int? selectedValue, IEnumerable<CategoryData> allCategories, bool addEmptyItem = true)
        {
            this.selectedValue = selectedValue;
            this.allCategories = allCategories ?? Enumerable.Empty<CategoryData>();
            this.addEmptyItem = addEmptyItem;
        }

        public HtmlString Build(string name)
        {
            StringBuilder contents = this.PrepareContentBuilder();
            
            foreach (string optGroup in this.BuildOptGroups())
            {
                contents.AppendLine(optGroup);
            }

            TagBuilder select = new TagBuilder("select");
            select.Attributes.Add("name", name);
            select.Attributes.Add("class", "large");
            select.InnerHtml = contents.ToString();

            return new HtmlString(select.ToString());
        }

        private StringBuilder PrepareContentBuilder()
        {
            var contents = new StringBuilder();
            
            if (this.addEmptyItem)
            {
                var defaultOption = this.CreateOptionItem("(none)");                
                contents.AppendLine(defaultOption.ToString());
            }

            return contents;
        }

        private IEnumerable<string> BuildOptGroups()
        {
            var results = new List<string>();

            // get root nodes
            var rootNodes = this.allCategories
                .Where(x => x.ParentId == null)
                .OrderBy(x => x.ShortName);

            foreach (var node in rootNodes)
            {
                var optGroup = new TagBuilder("optgroup");
                optGroup.Attributes["label"] = node.ShortName;
                optGroup.InnerHtml = this.BuildOptGroupContent(node);
                results.Add(optGroup.ToString());
            }

            return results;
        }

        private string BuildOptGroupContent(CategoryData node)
        {
            var html = new StringBuilder();

            // append the root node
            var option = this.CreateOptionItem(node.ShortName, node);

            html.AppendLine(option.ToString());

            // append child nodes
            var children = this.allCategories
                .Where(x => x.ParentId == node.Id)
                .OrderBy(x => x.ShortName);

            this.BuildOptions(node.ShortName, children, html);

            return html.ToString();
        }

        private void BuildOptions(string navigationPath, IEnumerable<CategoryData> nodes, StringBuilder html)
        {
            foreach (var node in nodes)
            {
                string navPath = "{0} » {1}".WithTokens(navigationPath, node.ShortName);
                var option = this.CreateOptionItem(navPath, node);
                html.AppendLine(option.ToString());

                // append child nodes
                var children = this.allCategories
                    .Where(x => x.ParentId == node.Id)
                    .OrderBy(x => x.ShortName);

                this.BuildOptions(navPath, children, html);
            }
        }

        private TagBuilder CreateOptionItem(string displayText, CategoryData node = null)
        {
            int value = node != null ? node.Id : 0;

            var option = new TagBuilder("option");
            option.SetInnerText(displayText);
            option.Attributes["value"] = value.ToString();

            if (this.selectedValue.HasValue && this.selectedValue == value)
            {
                option.Attributes["selected"] = "selected";
            }

            return option;
        }
    }
}