using System.Net;
using Musbooking.Domain.Core.Exceptions;

namespace Musbooking.Domain.Exceptions;

public class CantValidateRequestExceptionWithLog : HttpExceptionWithLog
{
    public CantValidateRequestExceptionWithLog(string message, string reasonPhrase, string loggingMessage, HttpStatusCode statusCode, params object[]? loggingParameters) : base(message, reasonPhrase, loggingMessage, statusCode, loggingParameters)
    {
    }
}
