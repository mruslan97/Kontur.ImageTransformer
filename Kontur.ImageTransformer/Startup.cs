using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using WebApiThrottle;

namespace Kontur.ImageTransformer
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new ThrottlingHandler
            {
                Policy = new ThrottlePolicy(60)
                {
                    ClientThrottling = true,
                },
                Repository = new MemoryCacheRepository(),
                QuotaExceededMessage = "Try again later"
            });
            appBuilder.UseWebApi(config);

        }
    }
}
