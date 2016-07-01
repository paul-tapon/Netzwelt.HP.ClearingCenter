using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Queries
{
    public class GetReturnObjectsByProductGroupCodeQuery : IQuery<IEnumerable<ReturnObjectData>>
    {
        public int MarketingProgramId { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryIsoCode { get; set; }
        
        [Required]
        [StringLength(128)]
        public string ProductGroupExternalCode { get; set; }

        public int? ProductId { get; set; }
    }
}
