using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    [ComplexType]
    [Serializable]
    public class Address
    {
        [StringLength(128)]
        public string StreetAddress01 { get; set; }

        [StringLength(128)]
        public string StreetAddress02 { get; set; }

        [StringLength(128)]
        public string City { get; set; }

        [StringLength(48)]
        public string PostalCode { get; set; }

        [StringLength(128)]
        public string StateOrProvince { get; set; }

        [StringLength(128)]
        public string Phone { get; set; }

        [StringLength(128)]
        public string Fax { get; set; }

        [StringLength(128)]
        public string EmailAddress { get; set; }
    }
}
