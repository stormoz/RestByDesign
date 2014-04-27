using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

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
                        return request.CreateResponse(new JSendPayload<object>
                        {
                            Status = JSendStatus.Error,
                            Message = t.Exception.Message,
                            Data = responseObject ?? t.Exception
                        });
                    }

                    if (t.IsCanceled)
                    {
                        return request.CreateResponse(new JSendPayload<object>
                        {
                            Status = JSendStatus.Fail,
                            Message = "Operation Cancelled",
                            Data = responseObject ?? t.Exception
                        });
                    }

                    var jsendResponse = responseObject as JSendPayload<object>;
                    if (jsendResponse != null)
                    {
                        return jsendResponse.Code != null ?
                            request.CreateResponse((HttpStatusCode)jsendResponse.Code, responseObject) :
                            request.CreateResponse(responseObject);
                    }

                    if ((int)t.Result.StatusCode >= 400 || t.Result.Content is ObjectContent<HttpError>)
                    {
                        return request.CreateResponse(
                            t.Result.StatusCode,
                            new JSendPayload<object>
                            {
                                Status = JSendStatus.Error,
                                Message = t.Result.ReasonPhrase,
                                Code = (int)t.Result.StatusCode,
                                Data = responseObject
                            });
                    }

                    return request.CreateResponse(new JSendPayloadSuccess<object>
                    {
                        Status = JSendStatus.Success,
                        Data = responseObject
                    });

                }, cancellationToken);
        }
    }
}