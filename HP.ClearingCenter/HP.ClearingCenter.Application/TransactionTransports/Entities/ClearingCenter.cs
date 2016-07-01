using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Entities
{
    [Table("T_ClearingCenter", Schema = Schemas.TransactionTransports)]
    public class ClearingCenter
    {
        public ClearingCenter()
        {
            this.Contact = new Address();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(128)]
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryIsoCode { get; set; }

        [Required]
        [StringLength(3)]
        public string LanguageIsoCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }
                
        public Address Contact { get; set; }
    }
}
