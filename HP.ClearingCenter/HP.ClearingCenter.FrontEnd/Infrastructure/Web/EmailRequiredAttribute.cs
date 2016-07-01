using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    public class EmailRequiredAttribute : RegularExpressionAttribute
    {
        public const string EMAIL_EXPRESSION = @"^[^@]+@[^@]+\.[^@]+$";
        public EmailRequiredAttribute() : base(EMAIL_EXPRESSION) { }
    }
}