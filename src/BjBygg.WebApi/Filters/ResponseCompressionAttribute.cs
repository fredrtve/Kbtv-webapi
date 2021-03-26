using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.IO.Compression;

namespace BjBygg.WebApi.Filters
{
    public class ResponseCompressionAttribute : ActionFilterAttribute
    {
        private Stream _originStream = null;
        private MemoryStream _ms = null;
        private CompressionLevel _level;
        public ResponseCompressionAttribute(CompressionLevel level = CompressionLevel.Fastest) { _level = level;  }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            HttpRequest request = context.HttpContext.Request;
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding)) return;
            acceptEncoding = acceptEncoding.ToUpperInvariant();
            HttpResponse response = context.HttpContext.Response;
            if (acceptEncoding.Contains("BR", StringComparison.OrdinalIgnoreCase))//Brotli 
            {
                if (!(response.Body is BrotliStream))// avoid twice compression.
                {
                    _originStream = response.Body;
                    _ms = new MemoryStream();
                    response.Headers.Add("Content-encoding", "br");
                    response.Body = new BrotliStream(_ms, _level);
                }
            }
            else if (acceptEncoding.Contains("GZIP", StringComparison.OrdinalIgnoreCase))
            {
                if (!(response.Body is GZipStream))
                {
                    _originStream = response.Body;
                    _ms = new MemoryStream();
                    response.Headers.Add("Content-Encoding", "gzip");
                    response.Body = new GZipStream(_ms, _level);
                }
            }
            else if (acceptEncoding.Contains("DEFLATE", StringComparison.OrdinalIgnoreCase))
            {
                if (!(response.Body is DeflateStream))
                {
                    _originStream = response.Body;
                    _ms = new MemoryStream();
                    response.Headers.Add("Content-encoding", "deflate");
                    response.Body = new DeflateStream(_ms, _level);
                }
            }
            base.OnActionExecuting(context);
        }

        public override async void OnResultExecuted(ResultExecutedContext context)
        {
            if ((_originStream != null) && (_ms != null))
            {
                HttpResponse response = context.HttpContext.Response;
                await response.Body.FlushAsync();
                _ms.Seek(0, SeekOrigin.Begin);
                response.Headers.ContentLength = _ms.Length;
                await _ms.CopyToAsync(_originStream);
                response.Body.Dispose();
                _ms.Dispose();
                response.Body = _originStream;
            }
            base.OnResultExecuted(context);
        }
    }
}
