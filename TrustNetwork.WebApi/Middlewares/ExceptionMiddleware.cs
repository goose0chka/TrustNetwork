using System.Text.Json;
using TrustNetwork.BL.Exceptions;

namespace TrustNetwork.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try { await _next(context); }
            catch (Exception ex)
            {
                var handler = GetHandler(ex);
                handler(context);
            }
        }

        private static Action<HttpContext> GetHandler(Exception ex)
        {
            var errBody = JsonSerializer.Serialize(new { error = ex.Message });
            return ex switch
            {
                BadRequestException => async (HttpContext context) =>
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errBody);
                },
                NotFoundException => async (HttpContext context) =>
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(errBody);
                }
                ,
                _ => async (HttpContext context) => { await context.Response.WriteAsync("Unexpected error occured"); }
            };
        }
    }
}
