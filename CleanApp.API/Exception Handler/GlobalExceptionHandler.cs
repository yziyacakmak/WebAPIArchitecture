using App.Application;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CleanApp.API.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var errorAsDto=ServiceResult.Fail(exception.Message,System.Net.HttpStatusCode.InternalServerError);

        httpContext.Response.StatusCode = (int)errorAsDto.Status;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken: cancellationToken);

        return true;
    }
}

