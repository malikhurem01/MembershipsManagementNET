using MembershipsManagement.Entities;
using MembershipsManagement.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using NLog.Fluent;
using System.Text;

namespace MembershipsManagement.Extensions
{
    public static class RegisterExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    StringBuilder message = new StringBuilder();
                    if (contextFeature?.Error is Exception)
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = "application/json";
                        message.Append(contextFeature.Error.Message);
                    }
                    else if (contextFeature?.Error is BadRequestException)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = "application/json";
                        message.Append("Bad Request. Parameters not valid.");
                    }
                    else if (contextFeature?.Error is EntityNotFoundException)
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        context.Response.ContentType = "application/json";
                        message.Append("The requested resource was not found.");
                    }
                    Log.Error($"Something Went Wrong in the {contextFeature.Error}");
                    await context.Response.WriteAsync(new ErrorModel
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = message.ToString()
                    }.ToString());
                });
            });
        }
    }
}
