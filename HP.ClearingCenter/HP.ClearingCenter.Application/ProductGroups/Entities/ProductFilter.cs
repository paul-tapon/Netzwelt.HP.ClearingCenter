using HP.ClearingCenter.Application.Data;
using HP.ClearingCenter.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Entities
{
    [Table("T_ProductFilter", Schema = Schemas.ProductGroups)]
    public class ProductFilter
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ProductGroupCategory ProductGroupCategory { get; set; }
                
        public int ProductFilterOperatorId { get; set; }
                
        [Required]
        [ForeignKey("ProductFilterOperatorId")]
        public ProductFilterOperator ProductFilterOperator { get; set; }

        [Required]
        [StringLength(128)]
        public string CustomAttributeExternalCode { get; set; }
        
        [StringLength(512)]
        public string CriteriaText { get; set; }

        [NotMapped]
        public FilterOperator FilterOperator { get { return (FilterOperator)this.ProductFilterOperatorId; } }

        [NotMapped]
        public string[] CriteriaValues
        {
            get
            {
                return string.IsNullOrEmpty(CriteriaText) ?
                    new string[0] :
                    this.CriteriaText.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); 
            }
        }

        public object[] GetCriteriaValues(CustomAttributeType customAttributeType)
        {
            var results = new List<object>();

            foreach (var val in this.CriteriaValues)
            {
                switch (customAttributeType)
                {
                    case CustomAttributeType.String:
                        results.Add(val);
                        break;
                    case CustomAttributeType.Boolean:
                        results.Add(bool.Parse(val));
                        break;
                    case CustomAttributeType.Integer:
                        results.Add(int.Parse(val));
                        break;
                    case CustomAttributeType.Decimal:
                        var formatter = CultureInfo.GetCultureInfo("en-US");
                        results.Add(decimal.Parse(val, formatter));
                        break;
                }
            }

            return results.ToArray();
        }
    }
}
