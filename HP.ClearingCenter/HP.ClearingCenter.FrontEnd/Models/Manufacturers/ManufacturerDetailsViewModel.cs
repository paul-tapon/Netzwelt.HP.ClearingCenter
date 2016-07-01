using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models.Manufacturers
{
    public class ManufacturerDetailsViewModel
    {
        private IApplicationContext app;

        public ManufacturerDetailsViewModel(IApplicationContext app)
        {
            this.app = app;
            this.Command = new AddOrUpdateManufacturerCommand();
        }

        public ManufacturerData Manufacturer { get; private set; }

        public AddOrUpdateManufacturerCommand Command { get; private set; }

        public bool IsManufacturerFound { get { return this.Manufacturer.Exists(); } }

        public bool IsAddNew { get; private set; }

        public ManufacturerDetailsViewModel Initialize(int? id = 0, bool isAddNew = false)
        {
            this.Manufacturer = this.app
                .PerformQuery(new SearchManufacturersQuery { ManufacturerId = id })
                .FirstOrDefault();

            if (this.Manufacturer.Exists())
            {
                this.Command.ManufacturerId = this.Manufacturer.Id.GetValueOrDefault();
                this.Command.Shortname = this.Manufacturer.Shortname;
                this.Command.ExternalCode = this.Manufacturer.ExternalCode;
                this.Command.Description = this.Manufacturer.Description;
            }

            this.IsAddNew = isAddNew;

            return this;
        }
    }
}