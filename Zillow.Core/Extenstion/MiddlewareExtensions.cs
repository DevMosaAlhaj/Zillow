using Microsoft.AspNetCore.Builder;
using Zillow.Core.Middleware;

namespace Zillow.Core.Extenstion
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAppMiddlewares(this IApplicationBuilder builder)
        {

            builder.UseMiddleware<ExceptionHandlerMiddleware>();
            
            return builder;
        }
    }
}