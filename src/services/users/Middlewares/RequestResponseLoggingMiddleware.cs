using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System;

namespace core_sockets.Middlewares
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseTimeMiddleware> _logger;

        public ResponseTimeMiddleware(RequestDelegate next, ILogger<ResponseTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var timer = new Stopwatch();
            timer.Start();
            await _next(context);
            timer.Stop();

            _logger.LogWarning("The request took '{0}' ms", timer.ElapsedMilliseconds);

        }
    }
}