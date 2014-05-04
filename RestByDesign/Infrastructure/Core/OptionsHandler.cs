using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using RestByDesign.Infrastructure.Core.Extensions;

namespace RestByDesign.Infrastructure.Core
{
    public class OptionsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method != HttpMethod.Options)
                return base.SendAsync(request, cancellationToken);

            var config = request.GetConfiguration();
            var apiExplorer = config.Services.GetApiExplorer();
            var controllerRequested = request.GetControllerName();

            var controllerDescriptor = apiExplorer.ApiDescriptions
                .Where(d => d.ActionDescriptor.ControllerDescriptor.ControllerName.EqualsIc(controllerRequested));
            
            var supportedMethods = controllerDescriptor.Select(d => d.HttpMethod.Method)
                .Distinct().ToList();

            if (!supportedMethods.Any())
                return Task.Factory.StartNew(() => request.CreateResponse(HttpStatusCode.NotFound), cancellationToken);

            if (supportedMethods.Contains("GET") && !supportedMethods.Contains("HEAD"))
                supportedMethods.Add("HEAD");

            return Task.Factory.StartNew(() =>
            {
                var resp = new HttpResponseMessage(HttpStatusCode.OK);
                resp.Headers.Add("Access-Control-Allow-Methods", String.Join(",", supportedMethods));
                return resp;
            }, cancellationToken);
        }
    }
}