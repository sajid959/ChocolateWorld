using Microsoft.AspNetCore.Mvc;  // this contains ProblemDetails, RFC 7807 standard object for API errors
using System.Text.Json;     // this is the default serializer used by ASP.NET Core, faster than Newtonsoft.Json
namespace ChocolateWorldApp.Api.Middleware
{
    public class ExcptionHandlingMiddleware
    {
        // private fields to hold the next middleware in the pipeline and a logger
        private readonly RequestDelegate _next; 
        private readonly ILogger<ExcptionHandlingMiddleware> _logger;

        // constructor to inject the next middleware in the pipeline and a logger and
        // this is the method that will be called for each request

        public ExcptionHandlingMiddleware(RequestDelegate next, ILogger<ExcptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async  Task InvokeAsync (HttpContext context)
        {
            try
            {
                await _next(context);  // call the next middleware in the pipeline
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occured on request {Method} {Path}",
                    context.Request.Method,context.Request.Path);

                await HandleExceptionAsync(context, ex);  // handle the exception and return a response
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception) 
        {
            // use pattern matching to determine the status code and title based on the exception type
            var (statusCode, title) = exception switch   
            {
                ArgumentNullException => (StatusCodes.Status400BadRequest, "Bad Request"),
                ArgumentException => (StatusCodes.Status400BadRequest, "Bad Request"),
                KeyNotFoundException => (StatusCodes.Status404NotFound, " Not Found"),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
                 _ => (StatusCodes.Status500InternalServerError, "Internal Server Error") // default case for unhandled exceptions
            };

            // create a ProblemDetails object to return a standardized error response
            var problemdetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            // set the response status code and content type, and write the ProblemDetails object as JSON to the response body
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            // serialize the ProblemDetails object to JSON using System.Text.Json with camelCase naming policy
            var json = JsonSerializer.Serialize(problemdetails, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            await context.Response.WriteAsync(json);
        }
    }
}
