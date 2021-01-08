using AlbedoTeam.Sdk.ExceptionHandler.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace AlbedoTeam.Sdk.ExceptionHandler
{
    public static class Setup
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(
            this IApplicationBuilder app,
            ILoggerFactory loggerFactory)
        {
            app.UseApiExceptionHandler(loggerFactory);
            return app;
        }
    }
}