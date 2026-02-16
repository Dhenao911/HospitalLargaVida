using HospitalLargaVida.Backend.Exceptions;
using System.Globalization;
using System.Net;
using System.Text.Json;

namespace HospitalLargaVida.Backend.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                    _logger.LogError(ex, "Ocurrio una excepcion no controllada: {Message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Declaramos el estado de la respuesta, por defecto es InternalServerError
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            //mensaje de error por defecto
            string defaultMessage = "A ocurrido un error inesperado.";

            switch (exception)
            {
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    defaultMessage = exception.Message;
                    break;

                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    defaultMessage = exception.Message;
                    break;

                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    defaultMessage = exception.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new
            {
                success = false,
                status = (int)statusCode,
                message = defaultMessage,
                traceId = context.TraceIdentifier
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    }
}