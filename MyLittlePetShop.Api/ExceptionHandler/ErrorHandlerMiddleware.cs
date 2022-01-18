using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyLittlePetShop.Api.Models.error;

namespace MyLittlePetShop.Api.ExceptionHandler
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                var message = new ErrorFormat();

                response.ContentType = "application/json";

                switch (error)
                {
                    case DataException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        message.Status = (int)HttpStatusCode.BadRequest;
                        message.Error = "BAD REQUEST";
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        message.Status = (int)HttpStatusCode.NotFound;
                        message.Error = "NOT FOUND";
                        break;
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        message.Status = (int)HttpStatusCode.Unauthorized;
                        message.Error = "UNAUTHORIZED";
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message.Status = (int)HttpStatusCode.InternalServerError;
                        message.Error = "INTERNAL SERVER ERROR";
                        break;
                }

                message.Message = error?.Message;
                await response.WriteAsync(JsonSerializer.Serialize(message));
            }
        }
    }
}
