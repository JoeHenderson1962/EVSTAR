using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ERPS
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Off;
            //routes.EnableFriendlyUrls(settings);
        }

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "getapi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "getapicode",
                routeTemplate: "api/{controller}/{clientcode}",
                defaults: new { clientcode = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "reachapi",
               routeTemplate: "api/reach/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}