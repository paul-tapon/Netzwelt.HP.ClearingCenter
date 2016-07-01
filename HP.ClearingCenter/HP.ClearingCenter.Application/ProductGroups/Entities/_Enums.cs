using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.ProductGroups.Entities
{
    public enum FilterOperator
    {
        IsEqualTo = 1,
        IsNotEqualTo = 2,        
        StartsWith = 3,
        EndsWith = 4,
        SatisfiesExpression = 5,
        Between = 6,
        GreaterThan = 7,
        GreaterThanOrEqualTo = 8,
        LessThan = 9,
        LessThanOrEqualTo = 10,
    }
}
