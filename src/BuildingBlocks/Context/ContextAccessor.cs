﻿
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Context;
internal class ContextAccessor : IContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid CorrelationId
    {
        get
        {
            if(HttpContextRequestIsAvailable
                && _httpContextAccessor.HttpContext.Request.Headers.Any(h => h.Key == Constants.CorrelationId))
            {
                return Guid.Parse(_httpContextAccessor.HttpContext.Request.Headers[Constants.CorrelationId]);
            }

            throw new ApplicationException($"{Constants.CorrelationId} header is not available!");
        }
    }

    public string TraceId
    {
        get
        {
            if(HttpContextIsAvailable && !string.IsNullOrWhiteSpace(_httpContextAccessor.HttpContext.TraceIdentifier))
            {
                return _httpContextAccessor.HttpContext.TraceIdentifier;
            }

            throw new ApplicationException("TraceIdentifier is not available!");
        }
    }

    private bool HttpContextRequestIsAvailable => _httpContextAccessor?.HttpContext?.Request != null;
    private bool HttpContextIsAvailable => _httpContextAccessor?.HttpContext != null;
}
