using MembershipsManagement.Exceptions;
using System.Net;
using System.Text.Json;

namespace MembershipsManagement.Extensions
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;
            string specialMessage = "";

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(EntityNotFoundException))
            {
                message = exception.Message;
                statusCode = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                statusCode = HttpStatusCode.NotImplemented;
                message = exception.Message;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "Dogodila se greška. Molimo kontaktirajte podršku";
                specialMessage = exception.Message;
            }

            var exceptionResult = JsonSerializer.Serialize(new { message, statusCode, specialMessage});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
