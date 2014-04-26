﻿using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using CacheCow.Server;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestByDesign.Infrastructure.Core;
using RestByDesign.Infrastructure.JSend;

namespace RestByDesign
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Content-negotiation
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().Single();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Converters.Add(new JavaScriptDateTimeConverter());
#if (DEBUG)
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
#endif
            // Etag-cache config for Get methods
            config.MessageHandlers.Add(new CachingHandler(config));

            // HEAD verb support
            config.MessageHandlers.Add(new CachingHandler(config));

            // Jsend
            config.MessageHandlers.Add(new JSendMessageHandler());

            // CORS
            config.EnableCors();

            // Exception handler attribute
            config.Filters.Add(new CustomExceptionAttribute());
        }
    }
}
