using BuildingBlocks.Exceptions;
using BuildingBlocks.Middlewares.ProblemDetails;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace BuildingBlocks.Middlewares;
public class GlobalExcepionsMiddleware : IExceptionHandler
{
    private readonly ILogger<GlobalExcepionsMiddleware> _logger;

    public GlobalExcepionsMiddleware(ILogger<GlobalExcepionsMiddleware> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        switch(exception)
        {
            case ValidationException:
                var validationProblemDetails = new ValidationProblemDetails();
                var validationEx = (ValidationException)exception;
                validationProblemDetails.Extend(httpContext.Request?.Path, validationEx.Errors?.ToList());
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;
                var response = JsonSerializer.Serialize(validationProblemDetails);
                await httpContext.Response.WriteAsync(response, cancellationToken);
                break;
            case DreamRoomsException:
                var ex = (DreamRoomsException)exception;
                var problemDetails = new DreamRoomsExProblemDetails(httpContext.TraceIdentifier, ex.Message);
                httpContext.Response.StatusCode = (int)ex.HttpStatusCode;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                break;
            default:
                var internalServerError = new InternalServerErrorProblemDetails(httpContext?.TraceIdentifier, httpContext.Request?.Path);
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;
                var json = JsonSerializer.Serialize(internalServerError);
                await httpContext.Response.WriteAsync(json, cancellationToken);
                break;
        }

        return true;
    }
}
