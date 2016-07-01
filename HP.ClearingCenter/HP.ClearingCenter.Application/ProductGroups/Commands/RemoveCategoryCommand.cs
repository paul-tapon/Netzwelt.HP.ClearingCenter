using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Commands
{
    public class RemoveCategoryCommand : ICommand
    {
        public int ProductGroupCategoryId { get; set; }
        public int ProductGroupId { get; set; }
        public int CategoryId { get; set; }
    }
}
