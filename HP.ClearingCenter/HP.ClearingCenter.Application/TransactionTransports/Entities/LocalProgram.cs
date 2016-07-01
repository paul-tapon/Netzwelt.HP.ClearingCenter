using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Entities
{
    [Table("T_LocalProgram", Schema = Schemas.TransactionTransports)]
    public class LocalProgram
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("MarketingProgram")]
        public int MarketingProgramId { get; set; }

        [Key]
        [Required]
        [StringLength(3)]
        [Column(Order = 2)]
        public string CountryIsoCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }
        
        public virtual ClearingCenter ClearingCenter { get; set; }
                
        public virtual MarketingProgram MarketingProgram { get; set; }

        public DateTime TermStartDate { get; set; }

        public DateTime TermEndDate { get; set; }
    }
}
