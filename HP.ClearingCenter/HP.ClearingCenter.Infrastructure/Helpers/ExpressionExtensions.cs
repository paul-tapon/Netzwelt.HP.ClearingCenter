using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace HP.ClearingCenter.Infrastructure.Helpers
{
    public static class ExpressionExtensions
    {
        public static MemberExpression MemberExpression<TModel, TValue>(this Expression<Func<TModel, TValue>> expression)
        {
            return expression.Body as MemberExpression;
        }
    }
}
