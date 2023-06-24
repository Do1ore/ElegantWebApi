using System.Net;

namespace ElegantWebApi.Api.Exceptions;

public sealed class ValidErrorCode
{
    public static HttpStatusCode GetErrorCode(Exception exception)
    {
        switch (exception)
        {
            case UnauthorizedAccessException _:
                return HttpStatusCode.Unauthorized;
            default: return HttpStatusCode.InternalServerError;
        }
    }
}