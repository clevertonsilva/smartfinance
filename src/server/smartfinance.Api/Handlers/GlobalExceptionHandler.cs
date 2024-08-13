using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using smartfinance.Domain.Common;
using System.Net;

namespace smartfinance.Api.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        //private static IDictionary<Type, HttpStatusCode> Mappings => new Dictionary<Type, HttpStatusCode>
        //{
        //    {typeof (ArgumentNullException), HttpStatusCode.BadRequest},
        //    {typeof (ArgumentException), HttpStatusCode.BadRequest},
        //    {typeof (ArgumentOutOfRangeException), HttpStatusCode.BadRequest},
        //    {typeof (InvalidOperationException), HttpStatusCode.BadRequest},
        //    {typeof (JsonReaderException), HttpStatusCode.BadRequest },
        //    {typeof (KeyNotFoundException), HttpStatusCode.BadRequest },
        //    {typeof (WebException), HttpStatusCode.BadRequest }
        //};

        public async ValueTask<bool> TryHandleAsync(
              HttpContext httpContext,
              Exception exception,
              CancellationToken cancellationToken)
        {
            Log.Error(exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Detail = exception.Message,
            };

            //var result = 

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        //public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        //{
        //    try
        //    {
        //        await next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, ex.Message);
        //        await HandleExceptionAsync(context, ex);
        //    }
        //}

        //private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    var code = Mappings.ContainsKey(exception.GetType()) ? Mappings[exception.GetType()] : HttpStatusCode.InternalServerError;
        //    await WriteExceptionAsync(context, exception, code);
        //}

        //private static async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        //{
        //    var response = context.Response;
        //    response.StatusCode = (int)code;
        //    response.ContentType = "application/json";

        //    await response
        //        .WriteAsync(JsonConvert.SerializeObject(OperationResult<string>.Failed(exception.Message)));
        //}


    }
}
