﻿using System.Web;
using System.Web.Mvc;

namespace Jobs_Ooffers_Websit
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
