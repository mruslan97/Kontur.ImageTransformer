using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Kontur.ImageTransformer.Filters
{
    public class SizeFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var contentLength = actionContext.Request.Content.Headers.ContentLength;
                if (contentLength.HasValue && contentLength.Value > 100 * 1024)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest);
            }, cancellationToken);
        }
    }
}
