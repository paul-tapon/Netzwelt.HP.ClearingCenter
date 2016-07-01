using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models.ProductGroups
{
    public class ProductGroupsSearchViewModel
    {
        private IApplicationContext app;

        public ProductGroupsSearchViewModel(IApplicationContext app)
        {
            this.app = app;
        }

        public IEnumerable<ProductGroupData> Results { get; private set; }

        public ProductGroupsSearchViewModel Initialize(string term, int? page)
        {
            Page = page ?? 1;

            this.Results = this.app.PerformQuery(new ProductGroupSearchQuery { Term = term,Page = this.Page, MaxRowCount = 1});
            return this;
        }

        public int Page { get; set; }
    }
}