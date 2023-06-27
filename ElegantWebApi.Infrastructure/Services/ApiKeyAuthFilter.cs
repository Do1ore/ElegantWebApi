using ElegantWebApi.Domain.Constants;
using ElegantWebApi.Domain.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace ElegantWebApi.Infrastructure.Services;

public class ApiKeyAuthFilter : IAsyncAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new ErrorModel
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "Api key not provided."
            });
            return Task.CompletedTask;
        }

        var apikey = _configuration.GetSection(AuthConstants.PathToKey).Value;
        if  (!apikey!.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new ErrorModel
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "Api key invalid."
            });
        }

        return Task.CompletedTask;
    }
}