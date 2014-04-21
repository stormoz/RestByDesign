using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using RestByDesign.Controllers;

namespace RestByDesign.Infrastructure.JSend
{
    public class JSendMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(t =>
                {
                    /*
                    if (request.Headers.Accept.All(a => a.MediaType != "application/json"))
                        return t.Result;

                    if (t.Result.Content != null && t.Result.Content.Headers.ContentType.MediaType != "application/json")
                        return t.Result;
                    */

                    object responseObject;
                    t.Result.TryGetContentValue(out responseObject);

                    if (t.Exception != null)
                    {
                        return request.CreateResponse(new JSendPayload
                        {
                            Status = JSendStatus.Error,
                            Message = t.Exception.Message,
                            Data = responseObject ?? t.Exception
                        });
                    }

                    if (t.IsCanceled)
                    {
                        return request.CreateResponse(new JSendPayload
                        {
                            Status = JSendStatus.Fail,
                            Message = "Operation Cancelled",
                            Data = responseObject ?? t.Exception
                        });
                    }

                    if (responseObject is JSendPayload)
                    {
                        return request.CreateResponse(responseObject);
                    }

                    var errorContent = t.Result.Content as ObjectContent<HttpError>;
                    if (errorContent != null)
                    {
                        return request.CreateResponse(new JSendPayload
                        {
                            Status = JSendStatus.Error,
                            Message = t.Result.ReasonPhrase,
                            Code = (int)t.Result.StatusCode,
                            Data = responseObject
                        });
                    }

                    return request.CreateResponse(new SuccessJSendPayload
                    {
                        Status = JSendStatus.Success,
                        Data = responseObject
                    });

                }, cancellationToken);
        }
    }
}