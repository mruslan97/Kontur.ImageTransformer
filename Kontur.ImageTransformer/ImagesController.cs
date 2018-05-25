using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ImageProcessor;
using Kontur.ImageTransformer.Filters;

namespace Kontur.ImageTransformer
{
    [SizeFilter]
    [ExceptionFilter]
    [RoutePrefix("process")]
    public class ImagesController : ApiController
    {
        [HttpPost]
        [Route("rotate-{parameter}/{x:int},{y:int},{w:int},{h:int}")]
        public async Task<HttpResponseMessage> Rotate(int x, int y, int w, int h, string parameter)
        {
            int degree;
            switch (parameter)
            {
                case "cw":
                    degree = 90;
                    break;
                case "ccw":
                    degree = -90;
                    break;
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var photoBytes = await Request.Content.ReadAsByteArrayAsync();
            var rectangle = new Rectangle(x, y, w, h);
            using (var inStream = new MemoryStream(photoBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var imageFactory = new ImageFactory(true))
                    {
                        imageFactory.Load(inStream)
                            .Rotate(degree)
                            .Crop(rectangle)
                            .Save(outStream);                            
                    }

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(outStream.ToArray())
                    {
                        Headers = { ContentType = new MediaTypeHeaderValue("image/png") }
                    };
                    return response;
                }
            }
        }

        [HttpPost]
        [Route("flip-{parameter}/{x:int},{y:int},{w:int},{h:int}")]
        public async Task<HttpResponseMessage> Flip(int x, int y, int w, int h, string parameter)
        {
            bool flipParameter;
            switch (parameter)
            {
                case "h":
                    flipParameter = false;
                    break;
                case "v":
                    flipParameter = true;
                    break;
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var photoBytes = await Request.Content.ReadAsByteArrayAsync();
            var rectangle = new Rectangle(x, y, w, h);
            using (var inStream = new MemoryStream(photoBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var imageFactory = new ImageFactory(true))
                    {
                        imageFactory.Load(inStream)
                            .Flip(flipParameter)
                            .Crop(rectangle)
                            .Save(outStream);
                    }

                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new ByteArrayContent(outStream.ToArray())
                    {
                        Headers = { ContentType = new MediaTypeHeaderValue("image/png") }
                    };
                    return response;
                }
            }
        }
    }
}
