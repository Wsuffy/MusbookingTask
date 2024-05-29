using Microsoft.Extensions.Logging;
using Musbooking.Domain.Core.Exceptions;

namespace Musbooking.Domain.Extensions;

public static partial class LoggerExtensions
{
    public static void LogHttpException(this ILogger logger, HttpExceptionWithLog exceptionWithLog) =>
        logger.LogError(exceptionWithLog.LoggingMessage, exceptionWithLog.LoggingParameters!);
}