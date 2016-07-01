using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.ProductGroups.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Entities
{
    [Table("T_CustomAttribute")]
    public class CustomAttribute
    {
        public CustomAttribute()
        {   
            this.OptionListItems = new List<OptionListItem>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]        
        public string ExternalCode { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortName { get; set; }

        public bool IsStrictToOptions { get; set; }

        public bool IsOptionListItemsEnabled { get; set; }

        [StringLength(128)]
        public string UnitText { get; set; }

        [StringLength(256)]
        public string TranslatorShortcut { get; set; }
        
        [StringLength(256)]
        public string Remarks { get; set; }

        [Required]
        public virtual CustomAttributeDataType CustomAttributeDataType { get; set; }

        public virtual IList<OptionListItem> OptionListItems { get; private set; }

        [NotMapped]
        public CustomAttributeType CustomAttributeType
        {
            get
            {   
                return this.CustomAttributeDataType != null ?
                    (CustomAttributeType)this.CustomAttributeDataType.Id :
                    CustomAttributeType.String;
            }
        }

        [NotMapped]
        public string ValueContainer
        {
            get
            {
                switch (this.CustomAttributeType)
                {
                    case Entities.CustomAttributeType.Boolean:
                        return "BooleanValue";
                    case Entities.CustomAttributeType.Decimal:
                        return "DecimalValue";
                    case Entities.CustomAttributeType.Integer:
                        return "IntegerValue";
                    default:
                        return "StringValue";
                }
            }
        }

        [NotMapped]
        public IEnumerable<FilterOperatorData> ValidOperators
        {
            get
            {
                var operators = new List<FilterOperatorData>();
                operators.Add(new FilterOperatorData(FilterOperator.IsEqualTo));
                operators.Add(new FilterOperatorData(FilterOperator.IsNotEqualTo));

                if (this.OptionListItems != null && !this.OptionListItems.Any())
                {
                    switch (this.CustomAttributeType)
                    {
                        case Data.Entities.CustomAttributeType.String:
                            operators.Add(new FilterOperatorData(FilterOperator.StartsWith));
                            operators.Add(new FilterOperatorData(FilterOperator.EndsWith));
                            break;
                        case Data.Entities.CustomAttributeType.Integer:
                        case Data.Entities.CustomAttributeType.Decimal:
                            operators.Add(new FilterOperatorData(FilterOperator.GreaterThan));
                            operators.Add(new FilterOperatorData(FilterOperator.GreaterThanOrEqualTo));
                            operators.Add(new FilterOperatorData(FilterOperator.LessThan));
                            operators.Add(new FilterOperatorData(FilterOperator.LessThanOrEqualTo));
                            operators.Add(new FilterOperatorData(FilterOperator.Between));
                            break;
                    }
                }

                return operators;
            }
        }
    }
}
