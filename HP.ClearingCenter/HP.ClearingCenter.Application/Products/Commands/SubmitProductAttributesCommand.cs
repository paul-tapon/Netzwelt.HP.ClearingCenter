using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Products.Commands
{
    public class SubmitProductAttributesCommand : ICommand
    {
        public SubmitProductAttributesCommand()
        {
            this.Values = new List<ProductCustomAttributeValueData>();
        }
        
        public int ProductId { get; set; }

        public List<ProductCustomAttributeValueData> Values { get; set; }
    }
}
