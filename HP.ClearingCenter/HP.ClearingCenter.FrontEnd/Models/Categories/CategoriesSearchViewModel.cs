using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models.Categories
{
    public class CategoriesSearchViewModel
    {
        private IApplicationContext app;

        public CategoriesSearchViewModel(IApplicationContext app)
        {
            this.app = app;
        }

        public IEnumerable<CategoryData> Results { get; private set; }

        public CategoriesSearchViewModel Initialize(string term,int? page)
        {
            Page = page ?? 1;

            this.Results = this.app.PerformQuery(new ProductCategorySearchQuery { Term = term,Page=this.Page  });
            return this;
        }

        public int Page { get; set; }
    }
}