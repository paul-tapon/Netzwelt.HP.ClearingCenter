using HP.ClearingCenter.Application.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.TransactionTransports.Entities
{
    [Table("T_MarketingProgram", Schema = Schemas.TransactionTransports)]
    public class MarketingProgram
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("MarketingProgramType")]
        public int MarketingProgramTypeId { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        [StringLength(512)]
        public string Description { get; set; }
                
        public bool IsForwardingEnabled { get; set; }
                
        public virtual MarketingProgramType MarketingProgramType { get; set; }
                
        [Required]
        public ClearingProcessType ClearingProcessType { get; set; }

        public ForwardingInstruction ForwardingInstruction { get; set; }

        [NotMapped]
        public ProgramType ProgramType { get { return (ProgramType)this.MarketingProgramTypeId; } }
    }
}
