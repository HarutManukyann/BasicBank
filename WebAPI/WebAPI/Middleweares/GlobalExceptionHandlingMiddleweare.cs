using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace WebAPI.Middleweares
{
    public class GlobalExceptionHandlingMiddleweare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlingMiddleweare> logger;
        public GlobalExceptionHandlingMiddleweare(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleweare> logger)
        {
            this.next = next;
            this.logger = logger;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
                

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            logger.LogError(ex.Message.ToString());
            string message;
            var code = ConfigurateExceptionTypes(ex, out message);
            ErrorModel model = new ErrorModel { StatusCode = (int)code, Message = message };
            var result = JsonSerializer.Serialize(model);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);

        }

        private static int ConfigurateExceptionTypes(Exception ex, out string message)
        {
            int httpStatusCode;
            var type = ex.GetType();
            message = "";
            switch (type)
            {
                case var _ when type == typeof(System.ServiceModel.FaultException):
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    message = "Bad Request";
                    break;
                case var _ when type == typeof(System.ServiceModel.CommunicationException):
                case var _ when type == typeof(System.NullReferenceException):
                    httpStatusCode = (int)HttpStatusCode.NotFound;
                    message = "Not found";
                    break;
                default:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }
            return httpStatusCode;
        }
    }
}
