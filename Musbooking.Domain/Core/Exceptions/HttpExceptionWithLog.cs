using System.Net;

namespace Musbooking.Domain.Core.Exceptions;

public abstract class HttpExceptionWithLog : HttpRequestException
{
    public readonly string ReasonPhrase;
    public readonly string LoggingMessage;
    public readonly object[]? LoggingParameters;

    protected HttpExceptionWithLog(string message, string reasonPhrase, string loggingMessage,
        HttpStatusCode statusCode,
        params object[]? loggingParameters) : base(message, null, statusCode)
    {
        ReasonPhrase = reasonPhrase;
        LoggingMessage = loggingMessage;
        LoggingParameters = loggingParameters;
    }
}