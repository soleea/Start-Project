using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace MiniEcommerceWebApi.Filters
{
    public class GlobalExceptionFilters : IExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment env;

        public GlobalExceptionFilters(ILogger<GlobalExceptionFilters> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            this.env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var tracedId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;

            if (!context.ExceptionHandled)
            {
                var exception = context.Exception;
                int statusCode;

                switch (true)
                {
                    case bool _ when exception is UnauthorizedAccessException:
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case bool _ when exception is NullReferenceException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case bool _ when exception is InvalidOperationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError($"GlobalExceptionFilter: Error in {context.ActionDescriptor.DisplayName}. {exception.Message}. Stack Trace: {exception.StackTrace}");

                if (env.EnvironmentName.Equals("Production"))
                {
                    var json = new
                    {
                        tracedId,
                        message = "An error occurred"
                    };

                    context.Result = new ObjectResult(exception.Message) { StatusCode = statusCode };
                    context.ExceptionHandled = true;
                }
                else
                {
                    context.Result = new ObjectResult(exception.Message) { StatusCode = statusCode };
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}