// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Builder;
// using System.Threading.Tasks;

// namespace core_sockets.Middlewares
// {
//     public class RequestResponseLoggingMiddleware
//     {
//         private readonly RequestDelegate _next;
//         private readonly ILogger _logger;

//         public RequestResponseLoggingMiddleware(RequestDelegate next,
//                                                 ILoggerFactory loggerFactory)
//         {
//             _next = next;
//             _logger = loggerFactory
//                       .CreateLogger<RequestResponseLoggingMiddleware>();
//         }

//         public async Task Invoke(HttpContext context)
//         {
//             _logger.LogInformation(await FormatRequest(context.Request));

//             var originalBodyStream = context.Response.Body;

//             using (var responseBody = new MemoryStream())
//             {
//                 context.Response.Body = responseBody;

//                 await _next(context);

//                 _logger.LogInformation(await FormatResponse(context.Response));
//                 await responseBody.CopyToAsync(originalBodyStream);
//             }
//         }

//         private static async Task<string> FormatRequest(HttpRequest request)
//         {
//             var body = request.Body;
//             request.EnableRewind();
//             var buffer = new byte[Convert.ToInt32(request.ContentLength)];
//             await request.Body.ReadAsync(buffer, 0, buffer.Length);
//             var bodyAsText = Encoding.UTF8.GetString(buffer);
//             request.Body = body;

//             var messageObjToLog = new { scheme = request.Scheme, host = request.Host, path = request.Path, queryString = request.Query, requestBody = bodyAsText };

//             return JsonConvert.SerializeObject(messageObjToLog);
//         }
        
//     }

//     public static class RequestResponseLoggingMiddlewareExtensions
//     {
//         public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
//         {
//             return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
//         }
//     }
// }