using Microsoft.AspNetCore.Http;
using Packages.Exceptions.Handlers;

namespace Packages.Exceptions.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExpcetionHandler _httpExceptionHandler;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _httpExceptionHandler = new HttpExpcetionHandler();
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExpcetionAsync(context.Response, exception);
            }
        }

        private Task HandleExpcetionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }
    }
}
