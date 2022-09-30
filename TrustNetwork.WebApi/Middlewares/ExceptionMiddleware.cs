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
                if (handler is null) throw;
                handler(context);
            }
        }

        private static Action<HttpContext>? GetHandler(Exception ex)
        {
            var errBody = JsonSerializer.Serialize(new { error = ex.Message });
            Action<HttpContext>? res = null;
            switch (ex)
            {
                case BadRequestException:
                    res = async (HttpContext context) =>
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(errBody);
                    };
                    break;
                case NotFoundException:
                    res = async (HttpContext context) =>
                    {
                        context.Response.StatusCode = 404;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(errBody);
                    };
                    break;
            };
            return res;
        }
    }
}
