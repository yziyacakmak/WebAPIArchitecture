using System.Diagnostics;
using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CleanApp.API.ExceptionHandlers;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not CriticalException)
        {
            Debug.WriteLine("Error Action");
        }
        return ValueTask.FromResult(false);
    }
}

