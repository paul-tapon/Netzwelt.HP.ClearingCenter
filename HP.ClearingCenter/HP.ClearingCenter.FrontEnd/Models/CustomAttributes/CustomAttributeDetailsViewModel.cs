using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Models.CustomAttributes
{
    public class CustomAttributeDetailsViewModel
    {
        private IApplicationContext app;

        public CustomAttributeDetailsViewModel(IApplicationContext app)
        {
            this.app = app;
            this.Command = new AddOrUpdateCustomAttributeCommand();
        }

        public IEnumerable<SelectListItem> CustomAttributeTypes {
            get
            {
                CustomAttributeType selectedValue = this.CustomAttributeData == null ? CustomAttributeType.String : this.CustomAttributeData.CustomAttributeType;
                
                yield return BuildCustomAttributeTypeItem(CustomAttributeType.String, selectedValue);
                yield return BuildCustomAttributeTypeItem(CustomAttributeType.Boolean, selectedValue);
                yield return BuildCustomAttributeTypeItem(CustomAttributeType.Integer, selectedValue);
                yield return BuildCustomAttributeTypeItem(CustomAttributeType.Decimal, selectedValue);
            }
        }

        public CustomAttributeData CustomAttributeData { get; private set; }

        public AddOrUpdateCustomAttributeCommand Command { get; private set; }
        
        public bool IsAddNew { get; private set; }

        public bool IsCustomAttributeFound
        {
            get
            {
                return CustomAttributeData.Exists();
            }
        }

        public CustomAttributeDetailsViewModel Initialize(AddOrUpdateCustomAttributeCommand command)
        {
            this.IsAddNew = true;
            this.Command = command;
            return this;
        }

        public CustomAttributeDetailsViewModel Initialize(int customAttributeId)
        {
            this.CustomAttributeData = this.app
                .PerformQuery(new GetCustomAttributeQuery { CustomAttributeId = customAttributeId })
                .FirstOrDefault();

            if (this.CustomAttributeData.Exists())
            {
                this.Command.CustomAttributeId = this.CustomAttributeData.Id;
                this.Command.CustomAttributeTypeId = ((int)this.CustomAttributeData.CustomAttributeType).ToString();
                this.Command.ExternalCode = this.CustomAttributeData.ExternalCode;
                this.Command.Shortname = this.CustomAttributeData.ShortName;
                this.Command.UnitText = this.CustomAttributeData.UnitText;
            }

            return this;
        }

        private SelectListItem BuildCustomAttributeTypeItem(CustomAttributeType customAttributeType, CustomAttributeType selectedValue)
        {
            bool isSelected = customAttributeType == selectedValue;
            return new SelectListItem { Value = ((int)customAttributeType).ToString(), Text = customAttributeType.ToString(), Selected = isSelected };
        }

    }
}