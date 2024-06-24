using System.Net;

namespace Musbooking.Domain.Exceptions;

public class UnkownExcpetionWithLog : HttpExceptionWithLog
{
    public UnkownExcpetionWithLog(string reasonPhrase, string loggingMessage, HttpStatusCode statusCode,
        params object[]? loggingParameters) : base(reasonPhrase, reasonPhrase, loggingMessage, statusCode, loggingParameters)
    {
    }
}