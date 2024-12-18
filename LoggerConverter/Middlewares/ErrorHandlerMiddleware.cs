using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System;
using LoggerConverter.Dtos.Exceptions;
using LoggerConverter.Dtos.Exceptions.Enums;
using Newtonsoft.Json;

namespace LoggerConverter.Middlewares
{

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var response = httpContext.Response;

            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                var responseBody = new ErrorResponse(CommonMessageEnum.GENERIC_MESSAGE);

                switch (error)
                {
                    case ValidationException e:
                        var messageString = e.Message;
                        messageString = messageString.Replace(" Severity: Error", "");
                        messageString = messageString.Split("-- ")[1];
                        responseBody.Message = messageString.Split(":")[1].Trim();
                        break;
                    default:
                        responseBody.Details = error.Message.Trim();
                        break;
                }

                await response.WriteAsync(JsonConvert.SerializeObject(responseBody));
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}