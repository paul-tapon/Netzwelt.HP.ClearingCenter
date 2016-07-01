using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Commands
{
    public class AddCategoryAttributeFilterCommand : ICommand
    {
        public AddCategoryAttributeFilterCommand()
        {
            this.InputValues = new string[0];
        }

        public int ProductGroupCategoryId { get; set; }

        public int ProductGroupId { get; set; }

        [Required]
        public string ProductGroupExternalCode { get; set; }

        public int CategoryId { get; set; }

        public int CustomAttributeId { get; set; }

        public int FilterOperatorId { get; set; }

        public string[] InputValues { get; set; }
    }
}
