using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace RestByDesign.Infrastructure.JSend
{
    public class JSendMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(t =>
                {
                    //if (request.Headers.Accept.All(a => a.MediaType != "application/json"))
                    //    return t.Result;

                    if (t.Result.Content != null && t.Result.Content.Headers.ContentType.MediaType != "application/json")
                        return t.Result;

                    HttpResponseMessage response;

                    object responseObject;
                    t.Result.TryGetContentValue(out responseObject);

                    if (t.Exception != null)
                    {
                        response = request.CreateResponse(new JSendPayload<object>
                        {
                            Status = JSendStatus.Error,
                            Message = t.Exception.Message,
                            Data = responseObject ?? t.Exception
                        });
                    }
                    else if (t.IsCanceled)
                    {
                        response = request.CreateResponse(new JSendPayload<object>
                        {
                            Status = JSendStatus.Fail,
                            Message = "Operation Cancelled",
                            Data = responseObject ?? t.Exception
                        });
                    }
                    else if(responseObject is JSendPayload<object>)
                    {
                        var jsendResponse = responseObject as JSendPayload<object>;
                        response = jsendResponse.Code != null ?
                            request.CreateResponse((HttpStatusCode)jsendResponse.Code, responseObject) :
                            request.CreateResponse(responseObject);
                    }
                    else if ((int)t.Result.StatusCode >= 400 || t.Result.Content is ObjectContent<HttpError>)
                    {
                        var data = responseObject;

                        var errorContent = t.Result.Content as ObjectContent;
                        if (errorContent != null)
                        {
                            var error = errorContent.Value as HttpError;

                            if (error != null)
                                data = error;
                        }

                        response = request.CreateResponse(
                            t.Result.StatusCode,
                            new JSendPayload<object>
                            {
                                Status = JSendStatus.Error,
                                Message = t.Result.ReasonPhrase,
                                Code = (int)t.Result.StatusCode,
                                Data = data
                            });
                    }
                    else
                    {
                        response = request.CreateResponse(new JSendPayloadSuccess<object>
                        {
                            Status = JSendStatus.Success,
                            Data = responseObject
                        });
                    }

                    t.Result.Headers.ToArray().ForEach(h=>response.Headers.Add(h.Key, h.Value));

                    return response;

                }, cancellationToken);
        }
    }
}