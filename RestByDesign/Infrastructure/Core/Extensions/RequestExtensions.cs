using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace RestByDesign.Infrastructure.Core.Extensions
{
    public static class RequestExtensions
    {
        public static string GetControllerName(this HttpRequestMessage request)
        {
            var config = request.GetConfiguration();
            var routeData = config.Routes.GetRouteData(request);

            if (routeData != null && routeData.Values != null && routeData.Values.ContainsKey("controller"))
                return routeData.Values["controller"] as string;

            // for attributeroutes
            var actionDescription = routeData.GetSubRoutes().First().Route.DataTokens["actions"] as HttpActionDescriptor[];
            return actionDescription[0].ControllerDescriptor.ControllerName;
        }
    }
}