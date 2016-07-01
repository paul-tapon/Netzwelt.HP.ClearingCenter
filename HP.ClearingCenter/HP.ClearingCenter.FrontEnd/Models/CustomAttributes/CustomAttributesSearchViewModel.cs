using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models.CustomAttributes
{
    public class CustomAttributesSearchViewModel
    {
        private IApplicationContext app;

        public CustomAttributesSearchViewModel(IApplicationContext app)
        {
            this.app = app;
        }

        public IEnumerable<CustomAttributeData> Results { get; private set; }

        public CustomAttributesSearchViewModel Initialize(string term,int? page)
        {
            Page = page ?? 1;

            this.Results = this.app
                .PerformQuery(new GetCustomAttributeQuery { ExternalCode = term,Page = Page,MaxRowCount = 5});

            return this;
        }

        public int Page { get; set; }

    }
}