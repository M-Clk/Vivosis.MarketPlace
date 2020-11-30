using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Vivosis.MarketPlace.API.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var failure = context.Features.Get<IExceptionHandlerFeature>();
                if(failure!=null)
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new Error
                    {
                        ErrorMessage = failure.Error.Message,
                        StatusCode = context.Response.StatusCode
                    }));
                }
            }));
        }
    }
}
