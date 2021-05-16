using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using System;
using System.IO.Compression;
using System.Threading.Tasks;

namespace BjBygg.WebApi.Filters
{
    public class ResponseCompressionAttribute : ResultFilterAttribute
    {
        private class ResponseCompressionOptionsProvider : IOptions<ResponseCompressionOptions>
        {
            public ResponseCompressionOptionsProvider(CompressionLevel level)
            {
                this.Value = new ResponseCompressionOptions()
                {
                    EnableForHttps = true        
                };
                this.Value.Providers.Add(new BrotliCompressionProvider(new BrotliCompressionProviderOptions() { Level = level }));
                this.Value.Providers.Add(new GzipCompressionProvider(new GzipCompressionProviderOptions() { Level = level }));
            }
            public ResponseCompressionOptions Value { get; private set; }
        }

        public CompressionLevel CompressionLevel { get; private set; }
        public bool BodyContainsSecret { get; private set; }
        public bool BodyContainsFormInput { get; private set; }

        public ResponseCompressionAttribute(CompressionLevel compressionLevel)
        {
            CompressionLevel = compressionLevel;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext executingContext, ResultExecutionDelegate next)
        {
            await new ResponseCompressionMiddleware(
                    (context) => { return next(); },
                    new ResponseCompressionProvider(
                        executingContext.HttpContext.RequestServices,
                        new ResponseCompressionOptionsProvider(CompressionLevel)
                    )
                ).Invoke(executingContext.HttpContext);
        }
    }
}