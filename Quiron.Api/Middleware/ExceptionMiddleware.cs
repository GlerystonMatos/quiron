using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Quiron.Domain.Exception;
using System.Net;

namespace Quiron.Api.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void UseExceptionHandlerCuston(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is QuironException)
                        {
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ExceptionMessage(
                                contextFeature.Error.Message)));
                        }
                        else
                        {
                            if (contextFeature.Error.InnerException.Message.Contains("violates foreign key constraint"))
                            {
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ExceptionMessage(
                                    "Não foi possível finalizar a operação pois este registro possui dependências.")));
                            }
                            else
                            {
                                context.Response.ContentType = "application/json";
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ExceptionMessage(
                                    "Erro interno no servidor ao processar a solicitação." + contextFeature.Error.Message +
                                    contextFeature.Error.InnerException.Message)));
                            }
                        }
                    }
                });
            });
        }
    }
}