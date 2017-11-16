using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Apollo.ASP.Models;


namespace Apollo.ASP
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes
    .Add(new MediaTypeHeaderValue("text/html"));
//            config.Formatters.Add(new BrowserJsonFormatter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
