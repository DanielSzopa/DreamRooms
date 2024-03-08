using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BuildingBlocks.Middlewares.ProblemDetails;
internal static class ValidationProblemDetailsExtensions
{
    internal static ValidationProblemDetails Extend(this ValidationProblemDetails problemDetails, string instance, List<ValidationFailure> errors)
    {
        if(errors != null)
        {
            var errorsDictionary = new Dictionary<string, List<string>>();
            foreach(var error in errors)
            {
                if (!errorsDictionary.ContainsKey(error.PropertyName))
                    errorsDictionary[error.PropertyName] = new List<string>();

                errorsDictionary[error.PropertyName].Add(error.ErrorMessage);
            }

            problemDetails.Errors = errorsDictionary.ToDictionary(k => k.Key, v => v.Value.ToArray());
        }

        problemDetails.Status = (int)HttpStatusCode.BadRequest;
        problemDetails.Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/400";
        problemDetails.Instance = instance;

        return problemDetails;
    }
}