using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Commands
{
    public class AddProductGroupFilterCategoryCommand : ICommand
    {
        public int ProductGroupId { get; set; }
                
        public int CategoryId { get; set; }
    }
}
