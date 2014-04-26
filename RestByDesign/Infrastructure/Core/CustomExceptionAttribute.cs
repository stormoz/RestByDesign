using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using PersonalBanking.Domain.Model.Exceptions;
using RestByDesign.Infrastructure.JSend;

namespace RestByDesign.Infrastructure.Core
{
    public class CustomExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var domainException = context.Exception as PersonalBankingException;

            if (domainException != null)
            {
                var payload = new JSendPayload { Data = null, Message = string.Join(Environment.NewLine, domainException.ErrorMessages), Status = JSendStatus.Fail };
                context.ActionContext.Response = context.ActionContext.Request.CreateResponse(HttpStatusCode.BadRequest, payload);

                return;
            }

            context.ActionContext.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, context.Exception);
        }
    }
}