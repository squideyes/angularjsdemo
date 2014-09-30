﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AngularJSDemo.Models;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace AngularJSDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<LogItem>("LogItems");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "odata",
                model: builder.GetEdmModel());

        }
    }
}
