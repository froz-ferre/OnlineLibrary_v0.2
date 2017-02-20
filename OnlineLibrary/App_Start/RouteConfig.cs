using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineLibrary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null,
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                null,
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    null,
            //    "",
            //    new { controller = "Home", action = "Index", able = (short?)null, page = 1 }
            //    );

            //routes.MapRoute(
            //    name: null,
            //    url: "Page{page}",
            //    defaults: new {controller = "Home", action = "Index", able = (short?)null},
            //    constraints: new { page = @"\d+" }
            //    );

            //routes.MapRoute(
            //    null,
            //    "{able}",
            //    new { controller = "Home", action = "Index", page = 1 }
            //    );

            //routes.MapRoute(
            //    null,
            //    "{able}/Page{page}",
            //    new { controller = "Home", action = "Index" },
            //    new { page = @"\d+"}
            //    );

            //routes.MapRoute(
            //    null,
            //    "{controller}/{action}"
            //    );


        }
    }
}
