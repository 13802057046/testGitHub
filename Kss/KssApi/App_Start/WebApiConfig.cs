using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KssApi.Common.Validation;
using WebApiThrottle;

namespace KssApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //register the custom action filter
            config.Filters.Add(new WebApiExceptionFilterAttribute());

            //Register throttling handler
            config.MessageHandlers.Add(new MyThrottlingHandler()
            {
                //Policy = new ThrottlePolicy(perSecond: 1, perMinute: 20, perHour: 200, perDay: 1500, perWeek: 3000)
                Policy = new ThrottlePolicy(perMinute: 3000)
                {
                    IpThrottling = true
                },
                Repository = new MemoryCacheRepository()
            });



        }
    }
}
