using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Musbooking.Domain.Core.Exceptions;
using Musbooking.Domain.Extensions;
using Musbooking.Domain.Http;
using Musbooking.Domain.Http.Responses;

namespace Musbooking.Host.Filters;

/// <inheritdoc />
public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task OnExceptionAsync(ExceptionContext exceptionContext)
    {
        var context = exceptionContext.HttpContext;
        var ex = exceptionContext.Exception;

        if (await ProcessException(ex, context))
            exceptionContext.ExceptionHandled = true;
    }

    private async Task<bool> ProcessException(System.Exception exception, HttpContext context)
    {
        switch (exception)
        {
            case HttpExceptionWithLog exceptionWithLog:
            {
                if (!string.IsNullOrEmpty(exceptionWithLog.LoggingMessage))
                    _logger.LogHttpException(exceptionWithLog);

                await context.WriteResponseToContextAsync(new ErrorResponse(exceptionWithLog),
                    exceptionWithLog.StatusCode!.Value);

                return true;
            }
            case HttpRequestException badRequestException:
                await context.WriteResponseToContextAsync(new ErrorResponse(badRequestException),
                    badRequestException.StatusCode);

                return true;

            default:
                return false;
        }
    }
}