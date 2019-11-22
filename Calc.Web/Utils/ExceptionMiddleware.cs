using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Calc.Web.Utils
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApplicationException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text";

                await context.Response.WriteAsync(ex.Message);
                return;
            }
            catch (Exception ex) {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started");
                    throw;
                }

                context.Response.Clear();
                await context.Response.WriteAsync(ex.Message);
                return;
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
