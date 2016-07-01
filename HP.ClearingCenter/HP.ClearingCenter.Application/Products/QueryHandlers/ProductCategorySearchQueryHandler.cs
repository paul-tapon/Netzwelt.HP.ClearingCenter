using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Entities;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;

namespace HP.ClearingCenter.Application.Products.QueryHandlers
{
    public class ProductCategorySearchQueryHandler : IQueryHandler<ProductCategorySearchQuery, IEnumerable<CategoryData>>
    {
        public IEnumerable<CategoryData> Retrieve(ProductCategorySearchQuery query)
        {
            using (var db = new ClearingCenterDataContext())
            {
                IQueryable<Category> resultSet = db.Categories.Include(x => x.ParentCategory);

                if (query.CategoryId.HasValue)
                {
                    resultSet = resultSet.Where(x => x.Id == query.CategoryId);
                }
                else
                {
                    string term = (query.Term ?? string.Empty).Trim().ToUpperInvariant();

                    if (!string.IsNullOrEmpty(query.Term))
                    {
                        resultSet = resultSet.Where(x => x.ExternalCode.ToUpper().StartsWith(term));
                    }

                    if (query.ExcludedCategoryIds.Any())
                    {
                        resultSet = resultSet.Where(x => !query.ExcludedCategoryIds.Contains(x.Id));
                    }
                }


                return this.FinalizeResults(resultSet.OrderBy(x => new { x.NavigationPath, x.ShortName }), query.Page, query.MaxResults);                    
            }
        }

        private IEnumerable<CategoryData> FinalizeResults(IQueryable<Category> resultSet,int page,int maxRows)
        {
            var categories = resultSet.Select(x => new CategoryData
            {
                Id = x.Id,
                ParentId = x.ParentCategory != null ? x.ParentCategory.Id : new Nullable<int>(),
                ParentCategoryName = x.ParentCategory != null ? x.ParentCategory.ShortName : string.Empty,
                NavigationPath = x.NavigationPath,
                ExternalCode = x.ExternalCode,
                ShortName = x.ShortName,
                Description = x.Description
            });

            return categories.ToPagedList(page, maxRows).AsEnumerable();
        }
    }
}
