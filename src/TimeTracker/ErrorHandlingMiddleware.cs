using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimeTracker
{
    public class ErrorHandelingMiddleware
    {

        private RequestDelegate _next;
        private ILogger<ErrorHandelingMiddleware> _logger;

        public static object HttpStatysCode { get; private set; }

        public ErrorHandelingMiddleware(RequestDelegate next, ILogger<ErrorHandelingMiddleware> logger)
        {

            _next = next;
            _logger = logger;


        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExeptionAsync(context, ex);

                
            }


        }

        private static Task HandleExeptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            // TODO: Change code base on exeption


            var problem = new ProblemDetails
            {
                Type = "https://www.etf.edu/server-error",
                Title = "Internal server error",
                Detail = ex.Message,
                Instance = "",
                Status = (int) code

            };

            var result = JsonSerializer.ToString(problem);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);


        }


    }
}
