using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using ImageProcessor.Common.Exceptions;

namespace Kontur.ImageTransformer.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public Type ExceptionType { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            switch (actionExecutedContext.Exception)
            {
                case ArgumentOutOfRangeException _:
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, new HttpError());
                    break;
                case ImageProcessingException _:
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                        HttpStatusCode.NoContent, new HttpError());
                    break;
                default:
                    actionExecutedContext.Response =
                        actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            new HttpError($"Unexpected exception: {actionExecutedContext.Exception.Message}"));
                    break;
            }

            return Task.FromResult<object>(null);
        }
    }
}
