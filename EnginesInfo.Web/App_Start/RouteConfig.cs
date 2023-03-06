using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnginesInfo.Web
{
    public class RouteConfig
    {
        public const string ALL_VALUES = "...";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Engines", action = "ObjectsInfo", id = UrlParameter.Optional }
            );
        }
    }
}
