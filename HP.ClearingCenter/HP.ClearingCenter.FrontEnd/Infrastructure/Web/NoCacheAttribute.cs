using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Infrastructure.Web
{
    public class NoCacheAttribute : OutputCacheAttribute
    {
        public NoCacheAttribute()
        {
            this.Duration = 0;
            this.NoStore = true;
        }
    }
}