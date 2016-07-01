using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Api.Data
{
    [Serializable]
    public class ContactAddress
    {
        [MaxLength(256)]
        public string CustomerName { get; set; }

        [MaxLength(256)]
        public string CompanyName { get; set; }

        [MaxLength(256)]
        public string StreetAddress01 { get; set; }

        [MaxLength(256)]
        public string StreetAddress02 { get; set; }

        [MaxLength(128)]
        public string City { get; set; }

        [MaxLength(64)]
        public string PostalCode { get; set; }

        [MaxLength(128)]
        public string StateOrProvince { get; set; }

        [MaxLength(48)]
        public string Phone { get; set; }

        [MaxLength(48)]
        public string Fax { get; set; }

        [MaxLength(512)]
        public string EmailAddress { get; set; }
    }
}
