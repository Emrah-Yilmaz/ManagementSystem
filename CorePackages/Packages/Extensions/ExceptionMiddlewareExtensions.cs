using Microsoft.AspNetCore.Builder;
using Packages.Exceptions.Middlewares;

namespace Packages.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomerExceptionMiddleware(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}
