using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models.Manufacturers
{
    public class ManufacturersSearchViewModel
    {
        private IApplicationContext app;

        public ManufacturersSearchViewModel(IApplicationContext app)
        {
            this.app = app;
            this.Results = Enumerable.Empty<ManufacturerData>();
        }

        public IEnumerable<ManufacturerData> Results { get; private set; }

        public ManufacturersSearchViewModel Initialize(string term, int? page)
        {
            Page = page ?? 1;
            this.Results = this.app.PerformQuery(new SearchManufacturersQuery { Term = term,Page = this.Page,MaxRows = 5});
            return this;
        }

        public int Page { get; set; }
    }
}