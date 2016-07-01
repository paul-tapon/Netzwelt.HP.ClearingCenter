using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models.Products
{
    public class ProductsSearchViewModel
    {
        private IApplicationContext app;

        public ProductsSearchViewModel(IApplicationContext app)
        {
            this.app = app;
        }

        public IEnumerable<ProductData> Results { get; private set; }

        public ProductsSearchViewModel Initialize(string term, int? page)
        {
            Page = page ?? 1;

            this.Results = this.app
                .PerformQuery(new SimpleSearchProductsQuery { Term = term, MaxItemCount = 5,Page = this.Page });

            return this;
        }

        public int Page { get; set; }
    }
}