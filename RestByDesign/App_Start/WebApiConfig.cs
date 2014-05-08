using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
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

            // Content-negotiation and formatter config
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().Single();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Converters.Add(new JavaScriptDateTimeConverter());

#if (DEBUG)
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
#endif

            config.MessageHandlers.Add(new OptionsHandler());

            // Etag-cache config for Get methods
            config.MessageHandlers.Add(new CachingHandler(config));

            // HEAD verb support
            config.MessageHandlers.Add(new HeadHandler());

            // Jsend
            config.MessageHandlers.Add(new JSendMessageHandler());

            // Exception handler attribute
            config.Filters.Add(new CustomExceptionAttribute());

            // CORS
            //config.EnableCors();
            var attr = new EnableCorsAttribute("*", "*", "*");// CORS support for all controllers
            config.EnableCors(attr);
        }
    }
}
