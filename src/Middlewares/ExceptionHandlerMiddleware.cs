using System;
using System.Net;
using AlbedoTeam.Sdk.ExceptionHandler.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AlbedoTeam.Sdk.ExceptionHandler.Middlewares
{
    internal static class ExceptionHandlerMiddleware
    {
        internal static void UseApiExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    //if any exception then report it and log it
                    if (contextFeature != null)
                    {
                        //Technical Exception for troubleshooting
                        var logger = loggerFactory.CreateLogger("GlobalException");
                        var errorCorrelatorId = Guid.NewGuid();
                        logger.LogError($"{errorCorrelatorId} | Algo errado aconteceu {contextFeature.Error}");

                        //Business exception - exit gracefully
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message =
                                $"Algo errado aconteceu. Tente novamente mais tarde ou verifique os logs com esta chave: {errorCorrelatorId}"
                        }.ToString());
                    }
                });
            });
        }
    }
}