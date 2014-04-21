using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestByDesign.Infrastructure.JSend
{
    public class FailResult : IHttpActionResult
    {
        public FailResult(HttpStatusCode code, string message, object data, ApiController controller)
        {
            Code = code;
            Message = message;
            Data = data;
            Controller = controller;
        }

        public HttpStatusCode Code { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public ApiController Controller { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                var payload = new JSendPayload { Data = Data, Message = Message, Status = JSendStatus.Fail };
                return Controller.Request.CreateResponse(Code, payload);
            }, cancellationToken);
        }
    }
}