using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Models.CustomAttributes
{
    public class CustomAttributeValueViewModel
    {
        private IDictionary<string, ProductCustomAttributeValueData> values;
        private CustomAttributeData attribute;
        private int index;

        public CustomAttributeValueViewModel(CustomAttributeData attribute, int index, IDictionary<string, ProductCustomAttributeValueData> values)
        {
            this.attribute = attribute;
            this.index = index;
            this.values = values;
        }

        public HtmlString BuildExternalCodeInput()
        {
            var input = new TagBuilder("input");
            input.Attributes["name"] = this.GetIndexedName("CustomAttributeExternalCode");
            input.Attributes["type"] = "hidden";
            input.Attributes["value"] = this.attribute.ExternalCode;
            
            return new HtmlString(input.ToString());
        }

        public HtmlString BuildLabel()
        {
            var label = new TagBuilder("label");
            label.Attributes["for"] = this.GetInputName();
            label.SetInnerText(this.attribute.ShortName);

            return new HtmlString(label.ToString());
        }

        public HtmlString BuildValueInput()
        {   
            var val = this.values.ContainsKey(this.attribute.ExternalCode) ?
                this.values[this.attribute.ExternalCode] : null;

            string inputName = this.GetInputName();


            switch (this.attribute.CustomAttributeType)
            {
                case CustomAttributeType.Boolean:
                    return HandleBooleanValue(inputName, this.attribute, val);
                case CustomAttributeType.Integer:                                        
                case CustomAttributeType.Decimal:
                    var valueData = val ?? new ProductCustomAttributeValueData();
                    string numValue = this.attribute.CustomAttributeType == CustomAttributeType.Integer ?
                        valueData.IntegerValue.GetValueOrDefault().ToString(Thread.CurrentThread.CurrentCulture) :
                        valueData.DecimalValue.GetValueOrDefault().ToString("#.##", Thread.CurrentThread.CurrentCulture);
                    
                    return HandleNumericInput(inputName, this.attribute, numValue);                
            }

            return HandleStringInput(inputName, this.attribute, val);
        }

        private static HtmlString HandleNumericInput(string inputName, CustomAttributeData attr, string val) 
        {
            return new HtmlString(BuildStandardTextInput(inputName, val, attr.CustomAttributeType).ToString());
        }

        private static HtmlString HandleBooleanValue(string inputName, CustomAttributeData attr, ProductCustomAttributeValueData val)
        {
            bool existingValue = val != null ? val.BooleanValue.GetValueOrDefault() : false;
            bool[] values = new[] { true, false };
            var selectListItems = values.Select(x => new SelectListItem
            {
                Value = x.ToString().ToLowerInvariant(),
                Text = x.ToString().ToLowerInvariant(),
                Selected = existingValue == x
            });

            return new HtmlString(
                BuildSelectList(inputName, selectListItems, existingValue.ToString().ToLowerInvariant()).ToString());
        }

        private static HtmlString HandleStringInput(string inputName, CustomAttributeData attr, ProductCustomAttributeValueData val)
        {
            string existingValue = val != null ? val.StringValue : string.Empty;

            // show option list items if there are any
            if (attr.OptionListItems != null && attr.OptionListItems.Any())
            {
                var selectListItems = attr.OptionListItems
                    .Select(x => new SelectListItem
                    {
                        Text = x.DisplayText ?? x.ValueText,
                        Value = x.ValueText,
                        Selected = x.ValueText == existingValue
                    });

                return new HtmlString(
                    BuildSelectList(inputName, selectListItems, existingValue, createEmptyItem: true).ToString());
            }

            return new HtmlString(
                BuildStandardTextInput(inputName, existingValue).ToString());
        }

        private static TagBuilder BuildStandardTextInput(string inputName, string existingValue, CustomAttributeType attrType = CustomAttributeType.String)
        {
            // show a standard input control
            var input = new TagBuilder("input");
            input.Attributes["name"] = inputName;
            input.Attributes["type"] = "text";
            input.Attributes["value"] = existingValue;

            bool isNumeric = attrType == CustomAttributeType.Integer || attrType == CustomAttributeType.Decimal;
            if (isNumeric)
            {
                input.Attributes["class"] = "numeric";
                input.Attributes["type"] = "number";
                input.Attributes["min"] = "0";
                input.Attributes["data-val"] = true.ToString().ToLowerInvariant();
                input.Attributes["data-val-number"] = "Input must be a number";
                input.Attributes["step"] = attrType == CustomAttributeType.Integer ?
                    "1" : "0.01";
            }

            return input;
        }

        private static TagBuilder BuildSelectList(string inputName, IEnumerable<SelectListItem> items, string selectedValue, bool createEmptyItem = false)
        {
            if (items == null || !items.Any()) return BuildStandardTextInput(inputName, selectedValue);            
            
            var options = new StringBuilder();

            if (createEmptyItem)
            {
                options.AppendLine("<option value=\"\"></option>");
            }

            foreach (var item in items)
            {
                var opt = new TagBuilder("option");
                opt.Attributes["value"] = item.Value;
                if (item.Selected)
                {
                    opt.Attributes["selected"] = "selected";
                }

                opt.SetInnerText(item.Text);
                options.AppendLine(opt.ToString());
            }

            var select = new TagBuilder("select");
            select.Attributes["name"] = inputName;
            select.InnerHtml = options.ToString();

            return select;
        }

        private string GetIndexedName(string propertyName)
        {
            return "Values[{0}].{1}".WithTokens(this.index, propertyName);
        }

        private string GetInputName()
        {
            switch (this.attribute.CustomAttributeType)
            {
                case CustomAttributeType.Boolean:
                    return this.GetIndexedName("BooleanValue");
                case CustomAttributeType.Integer:
                    return this.GetIndexedName("IntegerValue");
                case CustomAttributeType.Decimal:
                    return this.GetIndexedName("DecimalValue");
            }

            return this.GetIndexedName("StringValue");
        }
        
    }
}