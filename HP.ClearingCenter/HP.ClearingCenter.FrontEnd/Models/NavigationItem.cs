using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HP.ClearingCenter.FrontEnd.Models
{
    public class NavigationItem
    {
        public string DisplayText { get; set; }
        public string LinkUrl { get; set; }
        public bool IsActive { get; set; }
    }
}