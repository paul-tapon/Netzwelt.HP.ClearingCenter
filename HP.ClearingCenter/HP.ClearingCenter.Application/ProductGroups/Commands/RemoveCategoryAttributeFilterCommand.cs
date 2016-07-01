using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Commands
{
    public class RemoveCategoryAttributeFilterCommand : ICommand
    {
        public int ProductGroupId { get; set; }
        public string ProductGroupExternalCode { get; set; }
        public Guid ProductFilterId { get; set; }
    }
}
