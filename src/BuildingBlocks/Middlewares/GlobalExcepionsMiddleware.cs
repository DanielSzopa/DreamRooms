using BuildingBlocks.Exceptions;
using BuildingBlocks.Middlewares.ProblemDetails;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mime;
using System.Text.Encodings.Web;
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
        GlobalExceptionHandlerParametersDto dto = default;
        switch (exception)
        {
            case ValidationException:
                dto = HandleValidationException(httpContext.Request, (ValidationException)exception);
                break;
            case DreamRoomsException:
                dto = HandleDreamRoomsException(httpContext?.TraceIdentifier, (DreamRoomsException)exception);
                break;
            default:
                dto = HandleInternalServerError(httpContext);
                break;
        }

        var jsonResponse = JsonSerializer.Serialize(dto.ProblemDetails, new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
        httpContext.Response.StatusCode = dto.StatusCode;
        httpContext.Response.ContentType = dto.ContentType;
        await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

        return true;
    }

    private GlobalExceptionHandlerParametersDto HandleValidationException(HttpRequest httpRequest, ValidationException ex)
    {
        var validationProblemDetails = new ValidationProblemDetails();
        validationProblemDetails.Extend(httpRequest?.Path, ex.Errors?.ToList());
        return new(validationProblemDetails, MediaTypeNames.Application.ProblemJson, (int)HttpStatusCode.BadRequest);
    }

    private GlobalExceptionHandlerParametersDto HandleDreamRoomsException(string traceId, DreamRoomsException ex)
    {
        var dreamRoomsExProblemDetails = new DreamRoomsExProblemDetails(traceId, ex.Message);
        return new(dreamRoomsExProblemDetails, MediaTypeNames.Application.Json, (int)ex.HttpStatusCode);
    }

    private GlobalExceptionHandlerParametersDto HandleInternalServerError(HttpContext httpContext)
    {
        var internalServerError = new InternalServerErrorProblemDetails(httpContext?.TraceIdentifier, httpContext.Request?.Path);
        return new(internalServerError, MediaTypeNames.Application.ProblemJson, (int)HttpStatusCode.InternalServerError);
    }
}

public record GlobalExceptionHandlerParametersDto(object ProblemDetails, string ContentType, int StatusCode);
